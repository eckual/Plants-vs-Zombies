using UnityEngine;

namespace Tools
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        private void OnDestroy() => ReleaseReferences();
        protected abstract void ReleaseReferences();
    
    }
}