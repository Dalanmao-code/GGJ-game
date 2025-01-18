using System;

public class Singleton<T> where T : class, new()
{
    // 静态实例
    private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

    // 公共静态属性用于访问实例
    public static T Instance => _instance.Value;

    // 私有构造函数，防止外部实例化
    protected Singleton() { }
}
