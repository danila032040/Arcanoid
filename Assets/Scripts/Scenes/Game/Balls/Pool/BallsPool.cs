using Pool.Abstracts;
using Pool.Factories;
using Scenes.Game.Balls.Base;
using UnityEngine;

namespace Scenes.Game.Balls.Pool
{
    public class BallsPool : Pool<Ball>
    {
        [SerializeField] private Ball _prefab;
        
        private void Awake()
        {
            Init(new PrefabPoolFactory<Ball>(_prefab));
        }
        protected override void OnPoolEnter(Ball obj)
        {
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        protected override void OnPoolExit(Ball obj)
        {
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
        }
    }
}