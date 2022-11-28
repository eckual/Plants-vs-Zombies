using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class EntryPoint : BaseMonoBehaviour
    {
        [SerializeField] private List<EntryPointSystemBase> entryPoints;
        protected override void ReleaseReferences()
        {
            entryPoints = null;
        }

        private void Awake()
        {
            for (var i = 0; i < entryPoints.Count; i++)
                entryPoints[i].Begin();
        }
    }
}
