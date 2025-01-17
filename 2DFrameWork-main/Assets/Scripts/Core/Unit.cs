using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Unit : MonoBehaviour
{
    [HideInInspector]
    public UnitController m_controller;

    private List<UnitComponent> m_components;
    private List<UnitComponent> m_nonLogicComponents;
    
    [HideInInspector]
    public Transform m_transform;
    [HideInInspector]
    public Rigidbody2D m_rigidbody;

    private bool m_isInit;
    private void Awake()
    {
        OnInit();
    }
    private void OnDestroy()
    {
        OnRelease();
    }
    public virtual void OnInit()
    {
        if (m_isInit) return;
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_controller = GetComponent<UnitController>();
        m_components = new List<UnitComponent>();
        m_nonLogicComponents = new List<UnitComponent>();
        GetComponentsInChildren(m_components);
        m_components.Sort(UnitComponentComparison);
        for (var i = 0; i < m_components.Count; i++)
        {
            var component = m_components[i];
            component.unit = this;
            if (component is INotLogicComponent)
            {
                m_nonLogicComponents.Add(component);
            }
        }
        for (var i = 0; i < m_components.Count; i++)
        {
            m_components[i].OnInit();
        }
        m_isInit = true;
    }
    public virtual void OnRelease()
    {
        if (!m_isInit) return;
        for (var i = m_components.Count - 1; i >= 0; i--)
        {
            m_components[i].OnRelease();
        }
        m_isInit = false;
    }
    private void Update()
    {
        var deltaTime = Time.deltaTime;
        if (GameManager.IsPause)
        {
            //照常更新
            for (var i = 0; i < m_nonLogicComponents.Count; i++)
            {
                m_nonLogicComponents[i].OnUpdate(deltaTime);
            }
            return;
        }
        for (var i = m_components.Count - 1; i >= 0; i--)
        {
            m_components[i].OnUpdate(deltaTime);
        }
    }
    private void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;
        if (GameManager.IsPause)
        {
            //照常更新
            for (var i = 0; i < m_nonLogicComponents.Count; i++)
            {
                m_nonLogicComponents[i].OnFixedUpdate(deltaTime);
            }
            return;
        }
        for (var i = m_components.Count - 1; i >= 0; i--)
        {
            m_components[i].OnFixedUpdate(deltaTime);
        }
    }
    
    //排序
    private static int UnitComponentComparison(UnitComponent a, UnitComponent b)
    {
        return a.scriptOrder - b.scriptOrder;
    }
}