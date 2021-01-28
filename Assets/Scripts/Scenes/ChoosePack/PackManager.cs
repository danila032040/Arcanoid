using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Context;
using DG.Tweening;
using EnergySystem;
using SaveLoadSystem;
using SaveLoadSystem.Data;
using SaveLoadSystem.Interfaces;
using SaveLoadSystem.Interfaces.SaveLoaders;
using SceneLoader;
using Scenes.ChoosePack.Packs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ChoosePack
{
    public class PackManager : MonoBehaviour
    {
        [SerializeField] private Pack _packPrefab;
        [SerializeField] private Transform _packsParent;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TextMeshProUGUI _energyPointsCountText;
        [SerializeField] private Button _buttonReturn;


        private IPackProvider _packProvider;
        private IPlayerInfoSaveLoader _playerInfoSaveLoader;
        private DataProviderBetweenScenes _dataProvider;


        public void Init(IPackProvider packProvider, IPlayerInfoSaveLoader playerInfoSaveLoader,
            DataProviderBetweenScenes dataProvider)
        {
            _packProvider = packProvider;
            _playerInfoSaveLoader = playerInfoSaveLoader;
            _dataProvider = dataProvider;
        }

        private void Start()
        {
            Init(ProjectContext.Instance.GetPackProvider(), new InfoSaveLoader(), DataProviderBetweenScenes.Instance);
            SpawnPacks();
            ScrollToLastOpenedPack();
            
            _energyPointsCountText.text = $"{EnergyManager.Instance.GetEnergyPointsCount()}/" +
                                          $"{ProjectContext.Instance.GetEnergyConfig().GetInitialEnergyPoints()}";
            _buttonReturn.onClick.AddListener(() =>
            {
                SceneLoaderController.Instance.LoadScene(LoadingScene.StartScene);
            });
            
        }
        
        private Pack[] _packs;

        public void SpawnPacks()
        {
            PackInfo[] packInfos = _packProvider.GetPackInfos();
            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();
            int n = packInfos.Length;
            _packs = new Pack[n];


            if (info == null) info = PlayerInfo.GetDefault(n);

            _playerInfoSaveLoader.SavePlayerInfo(info);


            for (int i = 0; i < n; ++i)
            {
                _packs[i] = Instantiate(_packPrefab, _packsParent);
                _packs[i].transform.SetAsFirstSibling();
                _packs[i].Init(packInfos[i], _playerInfoSaveLoader.LoadPlayerInfo(), _packProvider);

                _packs[i].Clicked += PackClicked;

                if (info.GetOpenedPacks()[i])
                {
                    _packs[i].GetPackView().Show();
                }
                else _packs[i].GetPackView().Hide();
            }
        }

        private void ScrollToLastOpenedPack()
        {
            Pack lastOpenedPack = _packs[0];

            PlayerInfo info = _playerInfoSaveLoader.LoadPlayerInfo();

            for (int i = 0; i < info.GetOpenedPacks().Length; ++i)
                if (info.GetOpenedPacks()[i])
                {
                    lastOpenedPack = _packs[i];
                }

            Vector2 position =
                _scrollRect.GetSnapToPositionToBringChildIntoView(lastOpenedPack.GetComponent<RectTransform>());

            if (position.y < 0) position.y = 0;

            position = Vector2.ClampMagnitude(position, _scrollRect.content.rect.height - _scrollRect.viewport.rect.height);
            
            if (position.y < 0) position.y = 0;
            
            _scrollRect.content.transform.DOLocalMove(position, 1f);
        }

        private void PackClicked(PackInfo packInfo)
        {
            int packNumber = _packProvider.GetPackNumber(packInfo);

            PlayerInfo playerInfo = _playerInfoSaveLoader.LoadPlayerInfo();
            int levelNumber = playerInfo.GetLastPlayedLevels()[packNumber];

            if (levelNumber == packInfo.GetLevelsCount()) levelNumber = 0;

            _dataProvider.SetCurrentLevelNumber(levelNumber);
            _dataProvider.SetCurrentPackNumber(packNumber);

            SceneLoaderController.Instance.LoadScene(LoadingScene.GameScene);
        }
    }
}