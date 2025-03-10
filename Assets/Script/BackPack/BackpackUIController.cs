﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;
using Unity.VisualScripting;

public class BackpackUIController : MonoBehaviour
{
    // 事件通知背包更新
    public static event Action ONBACKPACK_UPDATED; 
    // 用于显示背包物品的UI元素
    public Transform backpack_uicontent;
    //格子预制体
    //public GameObject lattice_Prefabs;
    private BackpackController _backpackController;
    //存储背包格子
    private GameObject[] lattices = new GameObject[100];
    //最大格子数
    public static int MAXLATTICE = 8;
    public GameObject linshi;

    private bool IsOpen = false; 
    void Start()
    {
        Backpack.AddItem(new Item(0, DataCenter.GetItemDataByID(0), 1));
        Backpack.AddItem(new Item(1, DataCenter.GetItemDataByID(1), 1));
        Backpack.AddItem(new Item(2, DataCenter.GetItemDataByID(2), 1));
        Backpack.AddItem(new Item(3, DataCenter.GetItemDataByID(3), 1));
        Backpack.AddItem(new Item(4, DataCenter.GetItemDataByID(4), 1));
        Backpack.AddItem(new Item(5, DataCenter.GetItemDataByID(5), 1));

        //Backpack.AddItem(new Item(100, DataCenter.GetItemDataByID(100), 1));
        //Backpack.AddItem(new Item(4, DataCenter.GetItemDataByID(4), 1));
        //Backpack.AddItem(new Item(21, DataCenter.GetItemDataByID(21), 1));
        //Backpack.AddItem(new Item(23, DataCenter.GetItemDataByID(23), 1));
        //Backpack.AddItem(new Item(25, DataCenter.GetItemDataByID(25), 1));
        //Backpack.AddItem(new Item(44, DataCenter.GetItemDataByID(44), 1));
        BackpackUIController.notifyBackpackUpdated();
        initialize(this.gameObject.GetComponent<BackpackController>());
    }

    // 初始化方法
    public void initialize(BackpackController controller)
    {
        _backpackController = controller;

        // 注册事件监听器，当背包内容更新时，调用刷新方法
        ONBACKPACK_UPDATED += refreshUI;

        // 初始化UI
        refreshUI();
    }

    private void Update()
    {
        if (IsOpen&&Input.GetKeyDown(KeyCode.Escape))
        {
            Open_Close_Back();
        }
    }
    // 刷新UI，更新显示的物品格子
    public void refreshUI()
    {
        int i=0,j=0;
        // 清除旧的UI格子
        foreach (Transform child in backpack_uicontent)
        {
            lattices[i] = child.gameObject;
            child.gameObject.name = "Objects_" + i++;
            child.GetComponentInChildren<TextMeshProUGUI>().text =null;
            child.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);//清空颜色
            child.GetComponent<Follow_>().enabled = false;
            child.GetComponent<BoxCollider2D>().enabled = false;
        }

        // 获取当前背包中的物品
        var items = Backpack.GetItems();
            
        // 为每个物品生成一个UI格子
        foreach (var item in items)                        
        {
            if (item.count <= 0)
            {
                Backpack.RemoveItem(item.unitID);
                refreshUI();
            }
            else
            {
                lattices[j].GetComponent<Follow_>().enabled = true;
                lattices[j].GetComponent<BoxCollider2D>().enabled = true;
                lattices[j].GetComponentInChildren<TextMeshProUGUI>().text = DataCenter.GetItemDataByID(item.itemData.id).text;
                lattices[j].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);//清空颜色
                Follow_ follow_ = lattices[j].GetComponent<Follow_>();
                follow_.pairing_id = DataCenter.GetItemDataByID(item.itemData.id).pairing_id;
                follow_.target_id = DataCenter.GetItemDataByID(item.itemData.id).target_id;
                follow_.local_id = DataCenter.GetItemDataByID(item.itemData.id).id;
                j++;
            }
        }

        
    }

    // 处理右键点击物品的逻辑
    private void _onItemRightClicked(Item item)
    {
        Debug.Log($"Right-clicked on {item.itemData.id}");

        // 可扩展为显示详细信息或提供操作菜单
    }

    // 通知背包内容更新
    public static void notifyBackpackUpdated()
    {
        ONBACKPACK_UPDATED?.Invoke();
    }

    public void Open_Close_Back()
    {
        if (IsOpen)
        {
            backpack_uicontent.gameObject.SetActive(false);
            linshi.SetActive(false);
            IsOpen = !IsOpen;
        }
        else
        {
            backpack_uicontent.gameObject.SetActive(true);
            linshi.SetActive(true);
            IsOpen = !IsOpen;
        }
    }
}
