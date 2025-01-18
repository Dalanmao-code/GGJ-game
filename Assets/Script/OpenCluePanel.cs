using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

public class OpenCluePanel : MonoBehaviour,Iinteract
{
    public string path;
    public void InteractEvent()
    {
        FindCluePanel.Instance.ShowMeNew(path);
    }
}
