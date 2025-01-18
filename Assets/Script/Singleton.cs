using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 静态实例
    private static T _instance;

    // 公共静态属性用于访问实例
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 尝试在场景中查找现有的实例
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // 如果找不到，创建一个新的 GameObject 并添加组件
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();

                    // 确保在场景切换时不会销毁这个单例对象
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    // 私有构造函数，防止外部实例化
    protected Singleton() { }
}
