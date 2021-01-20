using UnityEngine;

namespace Scenes.Game.Blocks
{
    public class BlockView : MonoBehaviour
    {
        public Vector3 Size
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
    }
}