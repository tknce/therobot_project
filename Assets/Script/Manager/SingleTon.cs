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
                // 생성 후 반환
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
            DontDestroyOnLoad(gameObject); // 유니티 씬 전환 시에도 유지됩니다.
            Debug.Log(gameObject.name + "생성 성공");
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 이미 다른 인스턴스가 있는 경우 이 인스턴스를 파괴합니다.
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
