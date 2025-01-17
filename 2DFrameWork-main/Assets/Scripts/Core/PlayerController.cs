using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitController
{
    public override void OnUpdate(float deltaTime)
    {
        //移动
        m_moveInput = Input.GetAxisRaw("Horizontal");
    }
}
