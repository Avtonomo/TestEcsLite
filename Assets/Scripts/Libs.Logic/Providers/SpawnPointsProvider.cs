using UnityEngine;

namespace Libs.Logic.Providers
{
    //TODO: добавить спавн точки на уровне и возращать их координаты
    public class SpawnPointsProvider
    {
        public Vector3 GetHeroSpawnPoint()
        {
            return new Vector3(5,0,5);
        }
    }
}
