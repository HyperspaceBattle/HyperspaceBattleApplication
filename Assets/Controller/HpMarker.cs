using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpMarker : MonoBehaviour
{
    public GameObject HpMarker1;
    public GameObject HpMarker2;
    public GameObject HpMarker3;
    public GameObject HpMarker4;
    public GameObject HpMarker5;
    public GameObject HpMarker6;
    public GameObject HpMarker7;
    public GameObject HpMarker8;
    public GameObject HpMarker9;
    public GameObject HpMarker10;
    private Material healthBarColor;
    private Queue<GameObject> hpMarkers;
    // Use this for initialization
    void Awake ()
    {
        hpMarkers = new Queue<GameObject>();
        hpMarkers.Enqueue(HpMarker1);
        hpMarkers.Enqueue(HpMarker2);
        hpMarkers.Enqueue(HpMarker3);
        hpMarkers.Enqueue(HpMarker4);
        hpMarkers.Enqueue(HpMarker5);
        hpMarkers.Enqueue(HpMarker6);
        hpMarkers.Enqueue(HpMarker7);
        hpMarkers.Enqueue(HpMarker8);
        hpMarkers.Enqueue(HpMarker9);
        hpMarkers.Enqueue(HpMarker10);
    }

    public void Init(int playerNumber, int healthBarNum, Color color)
    {
        this.healthBarColor = (Material)Resources.Load("Prefab/Materials Shared/PlayerColorEmissive" + playerNumber);
        if (this.healthBarColor == null)
            Debug.Log("Material is null. Player " + playerNumber);

        int index = 0;
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        while(hpMarkers.Count > 0)
        {
            GameObject hpMarker = hpMarkers.Dequeue();
            if (index < healthBarNum)
            {
                healthBarColor.SetColor("_TintColor", color);
                hpMarker.GetComponent<MeshRenderer>().sharedMaterial = healthBarColor;
                tempQueue.Enqueue(hpMarker);
            }
            else
                hpMarker.SetActive(false);
            index++;
        }
        hpMarkers = tempQueue;
    }

    public bool Damage()
    {
        GameObject hpMarker = hpMarkers.Dequeue();
        hpMarker.SetActive(false);
        return hpMarkers.Count < 1;
    }
	
	
}
