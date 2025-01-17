using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Rendering;

[DefaultExecutionOrder(-900)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_instance;
    private static GameManager s_instance;
    
    public static bool IsPause => m_pauseMask > 0;
    private static int m_pauseMask;
    public static void SetPause(bool pause)
    {
        m_pauseMask += pause ? 1 : -1;
        if (m_pauseMask < 0)
        {
            Debug.LogError("暂停操作不对劲!");
            m_pauseMask = 0;
        }
    }

    [Serializable]
    private class ScenePrefab
    {
        public string name;
        public GameObject prefab;
    }
    [SerializeField]
    private List<ScenePrefab> m_scenePrefabList;
    private Dictionary<string, GameObject> m_scenePrefabDict;
    
    public static Player Player => s_player;
    private static Player s_player;

    private SceneInfo m_sceneInfo;
    private string m_curSceneName;
    
    private TimerController m_timerController;
    private UIController m_uiController;
    private void Awake()
    {
        s_instance = this;
        m_scenePrefabDict = new Dictionary<string, GameObject>();
        for (var i = 0; i < m_scenePrefabList.Count; i++)
        {
            var scenePrefab = m_scenePrefabList[i];
            m_scenePrefabDict[scenePrefab.name] = scenePrefab.prefab;
        }
        
        //初始化
        m_uiController = UIController.CreateInstance();
        m_timerController = TimerController.CreateInstance();
    }
    private void Start()
    {
        m_uiController.Show<UI_MainTitlePanel>();
        Debug.Log("1");
    }
    private void OnDestroy()
    {
        if (m_sceneInfo != null)
        {
            GameObject.Destroy(m_sceneInfo);
            m_sceneInfo = null;
        }
        
        m_uiController.ReleaseAll();
    }
    private void Update()
    {
        var deltaTime = Time.deltaTime;
        m_uiController.OnUpdate(deltaTime);
        m_timerController.OnUpdate(deltaTime);
    }
    
    public void LoadScene(string name)
    {
        //删除旧的
        if (m_sceneInfo != null)
        {
            if (s_player != null)
            {
                s_player.OnRelease();
                GameObject.Destroy(s_player.gameObject);
            }
            GameObject.Destroy(m_sceneInfo.gameObject);
        }
        
        //初始化场景
        m_sceneInfo = Instantiate(m_scenePrefabDict[name]).GetComponent<SceneInfo>();
        //开始剧本
        m_uiController.Show<UI_ScenarioPanel>().BeginScenario(m_sceneInfo.m_startScenarioName, 0);

        m_curSceneName = name;
    }

    public void InitPlayer(Player player)
    {
        s_player = player;
    }
    public void InitSceneObj(Transform root)
    {
        root.SetParent(m_sceneInfo.transform, true);
    }
}