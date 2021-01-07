using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallAttachment : MonoBehaviour
    {
        [SerializeField] private Vector3 _attachOffset;

        private void Update()
        {
            if (_isAttaching && (object)_attachingGameObject != null)
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
    }
}
