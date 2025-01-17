using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    [InspectorName("金色硬币")]
    GoldCoin = 1,
}
public class Inventory : Singleton<Inventory>
{
    public List<EItemType> list = new List<EItemType>();

    public void AddItem(EItemType type)
    {
        if (list.Contains(type)) return;
        list.Add(type);
    }
    public void UseItem(EItemType type)
    {
        if (!list.Contains(type)) return;
        list.Remove(type);
    }
    public bool Contains(EItemType type)
    {
        return list.Contains(type);
    }

    public void Load()
    {
        
    }
    public void Clear()
    {
        list.Clear();
    }
}