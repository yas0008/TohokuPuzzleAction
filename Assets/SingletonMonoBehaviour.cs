using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    instance = (Instantiate(Resources.Load("Prefab/Singleton" + typeof(T).Name)) as GameObject).GetComponent<T>();
                    if(instance == null)
                    {
                        Debug.Log(typeof(T) + "has no prefab to instantiate");
                    }
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}