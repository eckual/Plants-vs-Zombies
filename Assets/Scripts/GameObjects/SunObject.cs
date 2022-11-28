using System.Collections;
using Systems.Pooling;
using DG.Tweening;
using UnityEngine;

namespace GameObjects
{
    public class SunObject : PoolableObject
    {
        [HideInInspector] public DropOption dropOption;
        private CollectableSun _collectableSun;
        private WaitForSeconds _waitForSeconds;
        
        public bool hasTrigger;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(15f);
        }

        protected override void ReleaseReferences()
        {
            base.ReleaseReferences();
            dropOption = null;
            _collectableSun = null;
            _waitForSeconds = null;
        }

        public IEnumerator PlaceSunGenerator()
        {
            yield return _waitForSeconds;
            _collectableSun = PoolingSystem.Instance.Spawn<CollectableSun>(ObjectToPoolType.CollectableSun);
            _collectableSun.transform.SetParent(transform);
            _collectableSun.transform.position = 
                new Vector2(transform.position.x + 35, transform.position.y - 15);
            
            _collectableSun.transform.
                DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).Play().
                OnComplete(() => _collectableSun.transform.DOScale(Vector3.one, 0.5f).Play());
            
            yield return PlaceSunGenerator();
        }
        
        // trigger system
        private void OnTriggerEnter2D(Collider2D other)
        {
            hasTrigger = true;
            other.TryGetComponent(out dropOption);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            hasTrigger = false;
            other.TryGetComponent(out dropOption);
        }
        
    }
}
