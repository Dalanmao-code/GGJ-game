using System;
using System.Collections.Generic;
using LitJson;
using Scenario;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ScenarioManager : MonoBehaviour
{
    private static ScenarioManager _instance;

    private Dictionary<string, ScenarioData> m_dict = new Dictionary<string, ScenarioData>();

    private void Awake()
    {
        _instance = this;
        var assets = Resources.LoadAll<TextAsset>("Configs/Scenarios");
        for (int i = assets.Length - 1; i >= 0; i--)
        {
            var asset = assets[i];
            var jsonData = JsonMapper.ToObject(asset.text);
            var arr = new DialogueData[jsonData.Count];
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j] = DialogueData.Build(jsonData[j]);
            }
            m_dict[asset.name] = new ScenarioData(asset.name, arr);
        }
    }

    public static ScenarioData Find(string scenarioName)
    {
        return _instance.m_dict[scenarioName];
    }
}