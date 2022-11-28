using Tools;
using UnityEngine;

namespace Systems.Pooling
{
    public class PoolableObject : BaseMonoBehaviour
    {
        [SerializeField] protected ObjectToPoolType type;
        public ObjectToPoolType Type => type;

        private Transform _t;

        public Transform Transform
        {
            get
            {
                if (_t == null) _t = transform;
                return _t;
            }
        }

        protected override void ReleaseReferences()
        {
            _t = null;
        }
    }
}