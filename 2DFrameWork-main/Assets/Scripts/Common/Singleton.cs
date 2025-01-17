public class Singleton<T> where T : new ()
{
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                return s_instance = new T();
            }
            return s_instance;
        }
    }
    private static T s_instance;

    public static bool HasInstance() => s_instance != null;
    public static T GetInstance() => Instance;
    public static T CreateInstance() => Instance;
}