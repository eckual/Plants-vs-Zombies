using Systems.Pooling;
using ScenesManagers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameObjects
{
    public class SunController : GameObjectsController
    {
        [SerializeField] private Transform parent;
        private SunObject _sun;
        private bool _canCreateSun;
        protected override void ReleaseReferences()
        {
            parent = null;
            _sun = null;
        }

        private void Update()
        {
            if (InGameSceneManager.Instance.collectedSuns < value)
            {
                button.interactable = false;
                _canCreateSun = false;
                return;
            }

            _canCreateSun = true;
            button.interactable = true;
        }

        // drag & drop  system
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if(!_canCreateSun) return;
            _sun = PoolingSystem.Instance.Spawn<SunObject>(ObjectToPoolType.ObjectSun);
            _sun.transform.SetParent(parent.transform);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if(!_canCreateSun) return;
            _sun.transform.position = eventData.position;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if(!_canCreateSun) return;
            if (_sun.hasTrigger)
            {
                var dragOptionPosition = _sun.dropOption.transform;
                _sun.transform.SetParent(dragOptionPosition);
                _sun.transform.position = dragOptionPosition.position;
                StartCoroutine(_sun.PlaceSunGenerator());
                InGameSceneManager.Instance.collectedSuns -= value;
                
                InGameSceneManager.Instance.UpdateCollectedSunsText();
                return;
            }
            
            PoolingSystem.Instance.DeSpawn(_sun);
        }
    }
}
