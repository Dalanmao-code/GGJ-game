using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UI;
using UnityEngine;

namespace Scenario
{
    public enum EDrawingDirection
    {
        Left = 0, Center = 1, Right = 2
    }
    //立绘类
    public class DrawingData
    {
        public bool black;                  //是否黑化
        public Sprite icon;                 //角色立绘
        public EDrawingDirection direction; //角色站位
        
        public static DrawingData[] Build(JsonData iconData, JsonData data)
        {
            var count = Mathf.Min(data.Count, iconData.Count);
            var arr = new DrawingData[count];
            for (var i = 0; i < count; i++)
            {
                var nestingData = data[i];
                var drawingData = new DrawingData
                {
                    black = nestingData[0].ToString()[0] == '1',
                    direction = (EDrawingDirection) int.Parse(nestingData[1].ToString()),
                };
                var iconName = iconData[i].ToString().Trim();
                if (!string.IsNullOrEmpty(iconName))
                {
                    drawingData.icon = Atlas.DrawingIcon.GetSprite(iconName);
                }
                arr[i] = drawingData;
            }
            return arr;
        }
    }
    public class ReplyData
    {
        public string message;
        
        public string jumpScenario;    //跳转对话
        public int jumpTo;             //跳转id

        public static ReplyData[] Build(JsonData contentData, JsonData jumpData)
        {
            var count = contentData.Count;
            var arr = new ReplyData[count];
            for (var i = 0; i < count; i++)
            {
                var jumpPair = jumpData[i].ToString().Split(',');
                var replyData = new ReplyData
                {
                    message = contentData[i].ToString(),
                    
                    jumpScenario = jumpPair[0].Trim(),
                    jumpTo = int.Parse(jumpPair[1]),
                };
                arr[i] = replyData;
            }
            return arr;
        }
    }
    //对话类
    public class DialogueData
    {
        public int id;
        public string name;                  //显示名称
        public string content;               //对话内容
        public float showTime;               //展示时间
        public bool canSkip;                 //可否快进
        public bool shake;                   //是否抖动
        public DrawingData[] drawingDataArr; //立绘列表
        public ReplyData[] replyDataArr;     //回复列表
        
        public string jumpScenario;          //跳转对话
        public int jumpTo;                   //跳转id
        
        public bool end;                     //结束标志

        public static DialogueData Build(JsonData data)
        {
            var dialogueData = new DialogueData
            {
                id = int.Parse(data["i_id"].ToString()),
                name = data["s_name"].ToString(),
                content = data["s_content"].ToString(),
                showTime = float.Parse(data["f_show_time"].ToString()),
                canSkip = data["b_can_skip"].ToString()[0] == '1',
                shake = data["b_shake"].ToString()[0] == '1',
                drawingDataArr = DrawingData.Build(data["sl_drawing_icon"], data["ill_drawing_data_arr"]),
                replyDataArr = ReplyData.Build(data["sl_relay_content_arr"], data["sl_relay_jump_arr"]),
                jumpScenario = data["s_jump_scenario"].ToString().Trim(),
                jumpTo = int.Parse(data["i_jump_to"].ToString()),
                end = data["b_end"].ToString()[0] == '1',
            };
            return dialogueData;
        }
    }
    //剧本类
    public class ScenarioData
    {
        /// <summary>
        /// 获取第index段的对话
        /// </summary>
        public DialogueData this[int index] => m_dialogueDataArr[index];
        /// <summary>
        /// 获取对话数
        /// </summary>
        public int Length => m_dialogueDataArr.Length;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name => m_name;

        private readonly string m_name;
        
        private readonly DialogueData[] m_dialogueDataArr;
        private readonly Dictionary<int, int> m_dialogueDataMap;
        public ScenarioData(string name, DialogueData[] arr)
        {
            m_name = name;
            m_dialogueDataArr = arr;
            m_dialogueDataMap = new Dictionary<int, int>(arr.Length);
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                var data = arr[i];
                if (data.drawingDataArr == null)
                {
                    data.drawingDataArr = new DrawingData[0];
                }
                if (data.replyDataArr == null)
                {
                    data.replyDataArr = new ReplyData[0];
                }
                if (data.id >= 0)
                {
                    m_dialogueDataMap.Add(data.id, i);
                }
            }
        }

        public int GetIndexById(int id)
        {
            return m_dialogueDataMap[id];
        }
    }
}