using UnityEngine;

public class Player : Unit
{
    [HideInInspector]
    public PlayerTrigger m_trigger;

    public override void OnInit()
    {
        m_trigger = GetComponent<PlayerTrigger>();
        base.OnInit();
    }
}