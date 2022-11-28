using System;
using Systems.Pooling;
using ScenesManagers;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class CollectableSun : PoolableObject
    {
        private event Action<int> OnCollectSun; 
        [SerializeField] private Button button;
        protected override void ReleaseReferences()
        {
            if(OnCollectSun !=null) OnCollectSun -= CollectSun;
            button = null;
        }

        private void Awake()
        {
            OnCollectSun += CollectSun;
            button.onClick.AddListener(()=> OnCollectSun?.Invoke(50));
        }

        private void CollectSun(int gainedAmount)
        {
            Destroy(this.gameObject);
            InGameSceneManager.Instance.collectedSuns += gainedAmount;
            InGameSceneManager.Instance.UpdateCollectedSunsText();
        }
    }
}
