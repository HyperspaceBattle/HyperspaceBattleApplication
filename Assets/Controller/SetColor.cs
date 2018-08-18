using System;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public Material matParent;
    public Material[] matChildren;
    void Awake()
    {
        // Grabs the main material of the bullet.
        matParent = this.GetComponent<MeshRenderer>().material;

        // Grabs all children materials under the bullet if they exist.
        int childCount = this.transform.childCount;
        matChildren = new Material[childCount];
        for (int index = 0; index < childCount; index++)
            matChildren[index] = this.transform.GetChild(index).GetComponent<MeshRenderer>().material;
    }
    public void ColorSet(Color color)
    {
        try
        {
            matParent.SetColor("_TintColor", color);
            foreach(Material child in matChildren)
                child.SetColor("_TintColor", color);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ColorSet: " + ex.Message.ToString());
        }
    }
}
