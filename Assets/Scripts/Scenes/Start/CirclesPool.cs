
namespace Scripts.Scenes.Start
{
    using Scripts.Pool.Abstracts;
    using Scripts.Pool.Factories;
    using UnityEngine;

    public class CirclesPool : Pool<PoolableCircle>
    {
        [SerializeField] private PoolableCircle _prefab;

        public void Start()
        {
            base.Init(new PrefabFactory<PoolableCircle>(_prefab));
        }

        protected override void OnPoolEnter(PoolableCircle obj)
        {
            obj.transform.SetParent(this.transform);
            obj.gameObject.SetActive(false);
        }

        protected override void OnPoolExit(PoolableCircle obj)
        {
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
        }
    }
}
