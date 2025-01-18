using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    [Header("储存类")]
    [SerializeField]public GameObject[] LivingRoomScenarios;
    [SerializeField] public GameObject[] ExhibitionScenarios;
    [SerializeField] public GameObject[] WorkScenarios;
    [SerializeField] public GameObject[] SickRoomScenarios;
    [SerializeField] public Animator[] animators;
    private int local_scene = 0;

    //当前的
    private GameObject[] scenarios;
    // Start is called before the first frame update
    void Start()
    {
        scenarios = WorkScenarios;

        //Backpack.AddItem(new Item(0, DataCenter.GetItemDataByID(0), 1));
        //Backpack.AddItem(new Item(1, DataCenter.GetItemDataByID(1), 1));
        //Backpack.AddItem(new Item(3, DataCenter.GetItemDataByID(3), 1));
        //BackpackUIController.notifyBackpackUpdated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 供BackGroundMgr调用
    /// </summary>
    /// <param name="bt"></param>
    public void ChangeBackGround(BackGroundType bt)
    {
        switch (bt)
        {
            case BackGroundType.living:
                scenarios = LivingRoomScenarios;
                break;
            case BackGroundType.exhibt:
                scenarios = ExhibitionScenarios;
                break;
            case BackGroundType.sick:
                scenarios = SickRoomScenarios;
                break;
            case BackGroundType.work:
                scenarios = WorkScenarios;
                break;
        }

        local_scene = 0;
        Update_scene();
    }

    public void Left_Button()
    {
        local_scene--;
        Update_scene();
    }
    public void Right_Button()
    {
        local_scene++;
        Update_scene();
    }

    public void Update_scene()
    {
        local_scene = Mathf.Abs(local_scene) % scenarios.Length;
        foreach (var scenario in scenarios)
        {
            scenario.SetActive(false);
        }
        scenarios[local_scene].SetActive(true);
    }
    // 鼠标进入按钮时的响应
    public void OnPointerEnter_Left()
    {
        animators[0].SetBool("button", true);
    }

    // 鼠标离开按钮时的响应
    public void OnPointerExit_Left()
    {
        animators[0].SetBool("button", false);
    }

    // 鼠标进入按钮时的响应
    public void OnPointerEnter_Right()
    {
        animators[1].SetBool("button", true);
    }

    // 鼠标离开按钮时的响应
    public void OnPointerExit_Right()
    {
        animators[1].SetBool("button", false);
    }
}
