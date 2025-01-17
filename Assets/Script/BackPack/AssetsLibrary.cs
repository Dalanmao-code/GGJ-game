using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class AssetsLibrary
    {
        //资源加载
        private static Dictionary<string, Object> _RESOURCES = new Dictionary<string, Object>();
        
        /*资源加载相关*/
        public static T LoadResource<T>(string resourcePath) where T : Object
        {
            if (_RESOURCES.ContainsKey(resourcePath))
            {
                return _RESOURCES[resourcePath] as T;
            }
            else
            {
                T resource = Resources.Load<T>(resourcePath);
                if (resource != null)
                {
                    _RESOURCES[resourcePath] = resource;
                }
                return resource;
            }
        }
        
        public static T[] LoadResources<T>(string resourcePath) where T : Object
        {
            T[]  resource = Resources.LoadAll<T>(resourcePath);
            return resource;
        }
        
        public static void UnloadResource(string resourcePath)
        {
            if (_RESOURCES.ContainsKey(resourcePath))
            {
                Object resource = _RESOURCES[resourcePath];
                _RESOURCES.Remove(resourcePath);
                Resources.UnloadAsset(resource);
            }
        }
        public static void ClearResources()
        {
            foreach (var resource in _RESOURCES.Values)
            {
                Resources.UnloadAsset(resource);
            }
            _RESOURCES.Clear();
        }
        public static GameObject BattleDamageNumber;
    }

}
