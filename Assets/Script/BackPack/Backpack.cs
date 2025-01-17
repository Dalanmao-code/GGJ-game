using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Core;

//[System.Serializable]
//public class Item
//{
//    public int id;           // 物品的唯一标识符
//    public string itemName;  // 物品名称
//    public int quantity;     // 物品数量
//    public GameObject prefab; //物品对应实例化的格子

//    // 构造函数
//    public Item(int id, string name, int quantity, GameObject prefab)
//    {
//        this.id = id;
//        this.itemName = name;
//        this.quantity = quantity;
//        this.prefab = prefab;
//    }
//}

/// <summary>
/// 道具实体类
/// </summary>
public class Item
{
    public int unitID; // 唯一标识
    public ItemData itemData; // 道具数据
    public int count; // 道具数量

    public Item(int unitID, ItemData itemData, int count)
    {
        this.unitID = unitID;
        this.itemData = itemData;
        this.count = count;
    }
}

/// <summary>
/// 道具数据类
/// </summary>
[Serializable]
public class ItemData
{
    public int id;//道具Id
    public string text;//线索
    public string description; // 线索描述
    public string iconFileName; // 图标文件名
    public bool IsImportant;//是否为重要
    public int pairing_id; //配对Id
    public int target_id; //目标Id

    public  ItemData(int id, string text, string description, string iconFileName,bool IsImportant,int pairing_id,int target_id)
    {
        this.id = id;
        this.text = text;
        this.description = description;
        this.iconFileName = iconFileName;
        this.IsImportant = IsImportant;
        this.pairing_id = pairing_id;
        this.target_id = target_id;

    }

    public ItemData() { }
}

public class Backpack : MonoBehaviour
{
    // 背包中的物品列表
    public static List<Item> ITEMS = new List<Item>();

    // 增加物品到背包
    public static void AddItem(Item newItem)
    {
        Item existingItem = GetItemByID(newItem.unitID);
        if (existingItem != null)
        {
            // 如果物品已经存在，增加数量
            existingItem.count += newItem.count;
        }
        else
        {
            // 如果物品不存在，则添加到列表
            ITEMS.Add(newItem);
        }
    }

    // 删除物品
    public static void RemoveItem(int id)
    {
        Item item = GetItemByID(id);
        if (item != null)
        {
            ITEMS.Remove(item);
        }
    }

    // 更新物品数量
    public static void UpdateItem(int id, int newCount)
    {
        Item item = GetItemByID(id);
        if (item != null)
        {
            item.count = newCount;
        }
    }

    // 查找物品
    public static Item GetItemByID(int id)
    {
        return ITEMS.Find(item => item.unitID == id);
    }

    // 获取背包中的所有物品
    public static List<Item> GetItems()
    {
        return ITEMS;
    }
}
