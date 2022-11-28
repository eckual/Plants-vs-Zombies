using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace Systems.Pooling
{
    public enum ObjectToPoolType
    {
        CollectableSun, ObjectSun
    }
    public class PoolingSystem : EntryPointSystemBase
    {
        public static PoolingSystem Instance;
        [Serializable] private struct PoolObject
        {
            public ObjectToPoolType type;
            public PoolableObject objectReference;
            public int amount;
        }
        
        [SerializeField] private List<PoolObject> poolObjects;
        [SerializeField] private List<PoolableObject> currentPoolObjects;
        
        protected override void ReleaseReferences()
        {
            currentPoolObjects = null;
            poolObjects = null;
            Instance = null;
        }

        public override void Begin()
        {
            if (Instance != null) return;
            Instance = this;
            InitPool();
        }
        
        private void InitPool()
        {
            for (var index = 0; index < poolObjects.Count; index++)
            {
                var obj = poolObjects[index];
                for (var i = 0; i < obj.amount; i++)
                {
                    var newObj = Instantiate(obj.objectReference, transform, true);
                    newObj.gameObject.SetActive(false);
                    currentPoolObjects.Add(newObj);
                }
            }
        }
        public T Spawn<T>(ObjectToPoolType type) where T : PoolableObject
        {
            var poolItem = currentPoolObjects.FirstOrDefault(t => t.Type == type);
            if (poolItem != null)
            {
                var obj = poolItem.GetComponent<T>();
                currentPoolObjects.Remove(poolItem);
                obj.gameObject.SetActive(true);
                return obj;
            }
            GenerateExtraElement(type);
            return Spawn<T>(type);
        }
        
        public void DeSpawn(PoolableObject objectToDeSpawn)
        {
            objectToDeSpawn.transform.SetParent(transform);
            objectToDeSpawn.gameObject.SetActive(false);
            currentPoolObjects.Add(objectToDeSpawn);
        }
    
        //create an extra element if the pool is empty
        private void GenerateExtraElement(ObjectToPoolType type) 
        {
            var poolItem = poolObjects.FirstOrDefault(poolObject => poolObject.type == type);
            var item = Instantiate(poolItem.objectReference, transform);
            item.gameObject.SetActive(false);
            currentPoolObjects.Add(item);
        }
        
    }
}