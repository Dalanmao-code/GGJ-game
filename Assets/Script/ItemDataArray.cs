using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "ItemDataArray", menuName = "ScriptableObjects/ItemDataArray", order = 1)]
    [System.Serializable]
    public class ItemDataArray : ScriptableObject
    {
        [Header("道具数组")]
        public List<ItemData> itemDataList;
    }
}
