using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public void ColorSet(Color color)
    {
        try
        {
            Material parent = this.GetComponent<MeshRenderer>().sharedMaterial;
            if (parent != null)
            {
                Debug.Log("parent is not null");
                parent.SetColor("_TintColor", color);
            }

            int childCount = this.gameObject.transform.childCount;
            for (int index = 0; index < childCount; index++)
            {
                Material child = this.gameObject.transform.GetChild(index).GetComponent<MeshRenderer>().sharedMaterial;
                if (child != null)
                {
                    child.SetColor("_TintColor", color);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ColorSet: " + ex.Message.ToString());
        }
    }
}
