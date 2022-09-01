using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Libs.Logic.Containers
{
    public class RunTimeObjectsContainer
    {
        private Dictionary<int, GameObject> _sceneObjects = new Dictionary<int, GameObject>();

        public void AddNew(int instanceId, GameObject gameObject)
        {
            if (_sceneObjects.ContainsKey(instanceId))
            {
#if UNITY_EDITOR
                Debug.LogError($"Object exist! Id: {instanceId}");
#endif
                return;
            }

            _sceneObjects.Add(instanceId, gameObject);
        }

        [CanBeNull]
        public GameObject Get(int instanceId)
        {
            return _sceneObjects.ContainsKey(instanceId) ? _sceneObjects[instanceId] : null;
        }
    }
}
