using System.Collections;
using System.Collections.Generic;
using UI.ExamplePanel;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OpenCluePanel : MonoBehaviour,Iinteract
{
    public string path;
    [Header("�Ŵ�����")]
    [SerializeField]public int enlarge_id;
    [Header("׷������")]
    [SerializeField] public int Tracing_id;
    [Header("׷������")]
    [SerializeField] public int main_Tracing_id;
    public void InteractEvent()
    {
        FindCluePanel.enlarge_id = enlarge_id;
        FindCluePanel.Tracing_id = Tracing_id;
        FindCluePanel.main_Tracing_id = main_Tracing_id;
        FindCluePanel.openCluePanel = this;
        FindCluePanel.Instance.ShowMeNew(path);

    }
}
