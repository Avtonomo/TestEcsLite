using UnityEngine;

namespace Libs.Logic.Loaders
{
    public class ResourceLoadService
    {
        public GameObject Load(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}
