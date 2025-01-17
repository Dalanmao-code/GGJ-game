using System;
using System.Collections.Generic;
using UnityEngine;

public enum ETimerType
{
    Game, UI, _Count
}
/// <summary>
/// 计时器系统
/// </summary>
public class TimerController : Singleton<TimerController>
{
    private class Timer
    {
        private int m_finalId;
        private struct Data
        {
            public int m_id;
            public object m_notary;
            public Action m_callback;
            public float m_remaining;
        }
        private readonly List<Data> m_list = new List<Data>();
        
        /// <summary>
        /// 更新计时器
        /// </summary>
        public void Update(float deltaTime)
        {
            for (var i = 0; i < m_list.Count; i++)
            {
                var data = m_list[i];
                data.m_remaining -= deltaTime;
                //到期
                if (data.m_remaining <= 0)
                {
                    data.m_callback?.Invoke();
                    m_list.RemoveAt(i--);
                    continue;
                }
                m_list[i] = data;
            }
        }
        
        /// <summary>
        /// 添加一个任务
        /// </summary>
        public int AddTask(float time, Action callback, object notary)
        {
            m_list.Add(new Data
            {
                m_id = m_finalId,
                m_notary = notary,
                m_callback = callback,
                m_remaining = time
            });
            return m_finalId++;
        }

        /// <summary>
        /// 根据id关闭一个任务
        /// </summary>
        public void RemoveTask(int id, bool callback)
        {
            for (var i = m_list.Count - 1; i >= 0; i--)
            {
                var data = m_list[i];
                if (data.m_id == id)
                {
                    if (callback) data.m_callback?.Invoke();
                    m_list.RemoveAt(i);
                    return;
                }
            }
        }
        /// <summary>
        /// 根据法人关闭所有任务
        /// </summary>
        public void RemoveTask(object notary, bool callback)
        {
            for (var i = m_list.Count - 1; i >= 0; i--)
            {
                var data = m_list[i];
                if (data.m_notary == notary)
                {
                    if (callback) data.m_callback?.Invoke();
                    m_list.RemoveAt(i);
                    return;
                }
            }
        }
        /// <summary>
        /// 清空所有任务
        /// </summary>
        public void Clear()
        {
            m_list.Clear();
        }
    }

    private const int COUNT = (int) ETimerType._Count;
    
    private readonly Timer[] m_timers;
    private readonly float[] m_speeds;

    public TimerController()
    {
        m_timers = new Timer[COUNT];
        m_speeds = new float[COUNT];
        for (var i = 0; i < COUNT; i++)
        {
            m_timers[i] = new Timer();
            m_speeds[i] = 1f;
        }
    }

    /// <summary>
    /// 运行计时器
    /// </summary>
    public void OnUpdate(float deltaTime)
    {
        for (var i = 0; i < COUNT; i++)
        {
            m_timers[i].Update(deltaTime * m_speeds[i]);
        }
    }

    /// <summary>
    /// 设置所有计时器的运行速率
    /// </summary>
    public void SetSpeed(float speed)
    {
        speed = Math.Max(0, speed);
        for (var i = 0; i < COUNT; i++)
        {
            m_speeds[i] = speed;
        }
    }
    /// <summary>
    /// 设置某个计时器的运行速率
    /// </summary>
    public void SetSpeed(ETimerType type, float speed)
    {
        speed = Math.Max(0, speed);
        m_speeds[(int) type] = speed;
    }

    /// <summary>
    /// 重置所有计时器的运行速率
    /// </summary>
    public void ResetSpeed()
    {
        for (var i = 0; i < COUNT; i++)
        {
            m_speeds[i] = 1f;
        }
    }
    /// <summary>
    /// 重置某个计时器的运行速率
    /// </summary>
    public void ResetSpeed(ETimerType type)
    {
        m_speeds[(int) type] = 1f;
    }

    /// <summary>
    /// 给一个计时器添加定时任务 (并设置一个公证人)
    /// </summary>
    public int AddTask(ETimerType type, float time, Action callback, object notary = null)
    {
        return m_timers[(int) type].AddTask(time, callback, notary);
    }
    /// <summary>
    /// 根据id移除一个计时器的任务
    /// </summary>
    public void RemoveTask(ETimerType type, int id, bool callback = false)
    {
        m_timers[(int) type].RemoveTask(id, callback);
    }
    /// <summary>
    /// 根据公证人移除一个计时器的任务
    /// </summary>
    public void RemoveTask(ETimerType type, object notary, bool callback = false)
    {
        m_timers[(int) type].RemoveTask(notary, callback);
    }
    
    /// <summary>
    /// 清空所有计时器的任务
    /// </summary>
    public void ClearTask()
    {
        for (int i = 0; i < COUNT; i++)
        {
            m_timers[i].Clear();
        }
    }
    /// <summary>
    /// 清空某个计时器下的所有任务
    /// </summary>
    public void ClearTask(ETimerType type)
    {
        m_timers[(int) type].Clear();
    }
}