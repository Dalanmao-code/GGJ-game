using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : UnitComponent
{
    public override int scriptOrder => 0;
    
    public float m_walkSpeed;

    [Space]
    public bool m_moving;
    public float m_moveVelotiy;
    public bool m_rotateToRight;
    public Collider2D Collider => m_collider;

    public float m_moveInput;

    protected BoxCollider2D m_collider;
    protected Rigidbody2D m_rigidbody;
    public override void OnInit()
    {
        base.OnInit();
        m_collider = GetComponent<BoxCollider2D>();
        m_rigidbody = unit.m_rigidbody;
    }
    public override void OnFixedUpdate(float deltaTime)
    {
        //移动
        var value = m_moveInput;
        if (value != 0)
        {
            m_moveVelotiy = Mathf.Sign(value) * m_walkSpeed;
            m_rigidbody.position += new Vector2(m_moveVelotiy * deltaTime, 0);
                    
            m_moving = true;
            SetRotate(value > 0);
        }
        else
        {
            m_moving = false;
            m_moveVelotiy = 0;
        }
    }
    
    /// <summary>
    /// 设置朝向
    /// </summary>
    public void SetRotate(bool toRight)
    {
        m_rotateToRight = toRight;
    }
}
