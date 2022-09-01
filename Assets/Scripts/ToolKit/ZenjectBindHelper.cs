using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Studio3.Toolkit
{
    public static class ZenjectBindHelper 
    {
        public static T GetGameObjectOnSceneByName<T>(string objectName) where T : Component
        {
            return Object.FindObjectsOfType<T>(true)
                .FirstOrDefault(obj => obj.name == objectName)
                .GetOrThrow();
        }
    }
}