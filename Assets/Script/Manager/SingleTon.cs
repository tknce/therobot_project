using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance = null;

    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                // ���� �� ��ȯ
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject); // ����Ƽ �� ��ȯ �ÿ��� �����˴ϴ�.
            Debug.Log(gameObject.name + "���� ����");
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �̹� �ٸ� �ν��Ͻ��� �ִ� ��� �� �ν��Ͻ��� �ı��մϴ�.
        }
        
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            Destroy(this);
        }
    }
}
