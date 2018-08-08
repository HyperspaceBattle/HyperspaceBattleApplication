using UnityEngine;
using System;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public int parentNum;    
    //[SerializeField] private string[] tags = {"Enemy", "Bullet" };
    [SerializeField] private float timer = 1.5f;    
    //private List<string> destroyableTags = new List<string>();
    public Vector3 gunForward;
    public float bulletSpeed = -1f;
    private float gameTime = 0f;

    public void Init(int playerNum, Transform gun, float speed)
    {
        this.parentNum = playerNum;
        this.gunForward = -gun.forward;
        this.bulletSpeed = speed;
        Rigidbody rigidbody = Instantiate(this.gameObject.GetComponent<Rigidbody>(), gun.transform.position, gun.transform.rotation) as Rigidbody;
    }

    void Awake()
    {
        //foreach (string tag in tags)
        //    this.destroyableTags.Add(tag);
    }

    void FixedUpdate()
    {
        try
        {
            if (AppManager.IsUnpaused)
            {
                // Moves the bullet in the direction the ship is facing.
                if (this.bulletSpeed > 0)
                    transform.position += this.gunForward * Time.deltaTime * this.bulletSpeed;
                // Destroys the bullet after a specific amount of time.
                this.gameTime += Time.deltaTime;
                if (this.gameTime > this.timer)
                    Destroy(this.gameObject);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Bullet's FixedUpdate: " + ex.Message.ToString());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colliding with " + other.gameObject.name);
        try
        {            
            // Handles damaging a ship.
            ShipView oppShipView = other.gameObject.GetComponent<ShipView>();
            if (oppShipView != null)
                oppShipView.Damage(this.parentNum);
            Destroy(this.gameObject);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Bullet's OnCollisionEnter: " + ex.Message.ToString());
        }
    }

}