using UnityEngine;

namespace Tanks
{
    internal static class ResourcesLoader
    {

        public static T LoadPrefab<T>(ResourcePath path) where T : Object =>
            Resources.Load<T>(path.PathResource);

        public static T InstantiateObject<T>(T prefab, Transform parent, bool worldPositionStays) where T : Object
        {
            return Object.Instantiate(prefab, parent, worldPositionStays);
        }
        public static T LoadAndInstantiateObject<T>(ResourcePath path, Transform parent, bool
        worldPositionStays) where T : Object
        {
            var prefab = LoadPrefab<T>(path);
            return InstantiateObject(prefab, parent, worldPositionStays);
        }

    }
}
