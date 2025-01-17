using UnityEngine;

public class BackpackController : MonoBehaviour
{
    // BackpackController 作为中间层与 Backpack 和 UI 交互
    private Backpack _backpack;

    // 初始化时获取背包组件
    private void Start()
    {
        _backpack = GetComponent<Backpack>();
    }

    // 暂时作为空脚本，后续可根据项目需求添加具体逻辑
}
