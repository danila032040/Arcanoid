using UnityEngine;

namespace Scenes.Game.Balls
{
    public class BallAttachment : MonoBehaviour
    {
        [SerializeField] private Vector3 _attachOffset;
        [SerializeField] private Rigidbody2D _rb;

        public void AttachTo(Transform attachingTransform)
        {
            _rb.isKinematic = true;

            Transform currTransform = transform;
            
            currTransform.SetParent(attachingTransform);
            currTransform.localPosition = _attachOffset;
        }
        public void Detach()
        {
            _rb.isKinematic = false;
            
            transform.SetParent(null);
        }
    }
}
