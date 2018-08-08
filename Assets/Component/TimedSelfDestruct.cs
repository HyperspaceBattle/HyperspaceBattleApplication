using UnityEngine;
using System.Collections;

public class TimedSelfDestruct : MonoBehaviour
{
    private float gameTime = 0f;
	public float timeTilDestruct = 5.0f; 

    void FixedUpdate()
    {
        if (AppManager.IsUnpaused)
        {
            this.gameTime += Time.deltaTime;
            if(this.gameTime > this.timeTilDestruct)
                Destroy(this.gameObject);
        }
    }
}
