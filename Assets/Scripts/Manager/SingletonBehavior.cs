using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_inst;
    public static T Inst
    {
        get
        {
            if (s_inst)
            {
                return s_inst;
            }
            s_inst = FindObjectOfType<T>();
            if (!s_inst)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).FullName + "_Singleton";
                DontDestroyOnLoad(obj);
                s_inst = obj.AddComponent<T>();
            }
            return s_inst;
        }
    }
}