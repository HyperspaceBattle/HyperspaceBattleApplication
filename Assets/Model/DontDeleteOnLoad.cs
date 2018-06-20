using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDeleteOnLoad : MonoBehaviour
{
    private bool isCreated = false;

    void Awake()
    {
        if(!isCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            isCreated = true;
        }
    }
}
