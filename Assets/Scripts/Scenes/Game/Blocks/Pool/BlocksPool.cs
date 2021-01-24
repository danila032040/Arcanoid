using System;
using Pool.Abstracts;
using Pool.Factories;
using Scenes.Game.Blocks.Base;
using UnityEngine;

namespace Scenes.Game.Blocks.Pool
{
    public class BlocksPool : Pool<Block>
    {
        [SerializeField] private Block _prefab;

        private void Awake()
        {
            base.Init(new PrefabPoolFactory<Block>(_prefab));
        }

        protected override void OnPoolEnter(Block obj)
        {
            obj.gameObject.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        protected override void OnPoolExit(Block obj)
        {
            obj.gameObject.transform.SetParent(null);
            obj.gameObject.SetActive(true);
        }

        public BlockType GetBlockType() => _prefab.GetBlockType();
    }
}