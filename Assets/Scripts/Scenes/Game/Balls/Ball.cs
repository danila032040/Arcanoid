namespace Scripts.Scenes.Game.Balls
{
    using UnityEngine;
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector3 _attachOffset;

        [SerializeField] private BallMovement _ballMovement;

        private void Update()
        {
            if (_isAttaching && _attachingGameObject != null)
            {
                Attach();
            }
        }

        private void Attach()
        {
            this.transform.position = _attachingGameObject.transform.position + _attachOffset;
        }

        private bool _isAttaching;
        private GameObject _attachingGameObject;
        public void AttachTo(GameObject obj)
        {
            _isAttaching = true;
            _attachingGameObject = obj;
        }
        public void Detach()
        {
            _isAttaching = false;
            _attachingGameObject = null;
        }
        public void StartMoving()
        {
            _ballMovement.StartMoving();
        }
    }
}
