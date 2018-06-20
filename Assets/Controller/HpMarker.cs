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
    [SerializeField] private Material healthBarColor;
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
	}

    public void Init(int healthBarNum, int playerID)
    {
        int index = 0;
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        while(hpMarkers.Count > 0)
        {
            GameObject hpMarker = hpMarkers.Dequeue();
            if (index < healthBarNum)
            {
                healthBarColor.SetColor("_TintColor", AppManager.GetPlayerColor(playerID));
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
        return hpMarkers.Count > 0;
    }
	
	
}
