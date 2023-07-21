using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeCycle : MonoBehaviour
{
    public virtual void Initialize()
    {

    }

    private void Awake()
    {
        Initialize();
    }
}