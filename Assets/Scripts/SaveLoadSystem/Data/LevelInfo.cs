using System.Collections.Generic;
using SaveLoadSystem.Interfaces.Infos;
using Scenes.Game.Bricks;
using UnityEngine;

namespace SaveLoadSystem.Data
{
    [System.Serializable]
    public class LevelInfo : IBrickLevelInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private string _fileName;
        [SerializeField] private int _n;
        [SerializeField] private int _m;
        [SerializeField] private List<BrickType> _bricks;
        [SerializeField] private float _brickHeight;
        [SerializeField] private float _leftOffset;
        [SerializeField] private float _rightOffset;
        [SerializeField] private float _offsetBetweenRows;
        [SerializeField] private float _offsetBetweenCols;

        public string Name => _name;
        public string FileName => _fileName;
        public BrickType?[,] Map
        {
            get
            {
                BrickType?[,] res = new BrickType?[_n, _m];

                int currBrick = 0;
                for (int i = 0; i < _n; ++i)
                    for (int j = 0; j < _m; ++j)
                    {
                        if (_bricks[currBrick] != BrickType.None) res[i, j] = _bricks[currBrick];
                        else res[i, j] = null;
                        ++currBrick;
                    }

                return res;
            }
        }
        public float BrickHeight => _brickHeight;
        public float LeftOffset => _leftOffset;
        public float RightOffset => _rightOffset;
        public float OffsetBetweenRows => _offsetBetweenRows;
        public float OffsetBetweenCols => _offsetBetweenCols;

        public LevelInfo() { }

        public LevelInfo(string name, string fileName, List<BrickType> bricks, int n, int m, float brickHeight, float leftOffset, float rightOffset, float offsetBetweenRows, float offsetBetweenCols)
        {
            _name = name;
            _fileName = fileName;
            _bricks = bricks;
            _n = n;
            _m = m;
            _brickHeight = brickHeight;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
            _offsetBetweenRows = offsetBetweenRows;
            _offsetBetweenCols = offsetBetweenCols;
        }
    }
}