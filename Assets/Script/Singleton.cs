using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // ��̬ʵ��
    private static T _instance;

    // ������̬�������ڷ���ʵ��
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // �����ڳ����в������е�ʵ��
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // ����Ҳ���������һ���µ� GameObject ��������
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();

                    // ȷ���ڳ����л�ʱ�������������������
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    // ˽�й��캯������ֹ�ⲿʵ����
    protected Singleton() { }
}
