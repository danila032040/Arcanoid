using Pool.Abstracts;
using Pool.Factories;
using UnityEngine;

namespace Scenes.Game.Player.Pools
{
    public class OneHpViewPool : Pool<OneHpView>
    {
        [SerializeField] private OneHpView _prefab;

        public void Awake()
        { 
            Init(new PrefabPoolFactory<OneHpView>(_prefab));
        }

        protected override void OnPoolEnter(OneHpView view)
        {
            view.gameObject.SetActive(false);
            
            Transform viewTransform = view.transform;
            viewTransform.SetParent(transform);
            viewTransform.localScale = new Vector3(0, 0, 0);
        }

        protected override void OnPoolExit(OneHpView view)
        {
            view.gameObject.SetActive(true);
            
            Transform viewTranform = view.transform;
            viewTranform.SetParent(transform);
            viewTranform.localScale = new Vector3(0, 0, 0);
        }
    }
}