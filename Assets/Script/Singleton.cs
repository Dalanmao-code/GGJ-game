using System;

public class Singleton<T> where T : class, new()
{
    // ��̬ʵ��
    private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

    // ������̬�������ڷ���ʵ��
    public static T Instance => _instance.Value;

    // ˽�й��캯������ֹ�ⲿʵ����
    protected Singleton() { }
}
