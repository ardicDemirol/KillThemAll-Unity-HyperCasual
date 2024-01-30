using UnityEngine;

namespace Modifiers
{
    public abstract class ModifierBase : MonoBehaviour
    {
        public abstract void Modify(PlayerController playerController);
    }
}