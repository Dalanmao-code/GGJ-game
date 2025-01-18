using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Obj: MonoBehaviour
{
    [Header("ÊýÖµÀà")]
    [SerializeField] public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get_ToId()
    {
        Backpack.AddItem(new Item(id, DataCenter.GetItemDataByID(id), 1));
        BackpackUIController.notifyBackpackUpdated();
        Destroy(this.gameObject);
    }
}
