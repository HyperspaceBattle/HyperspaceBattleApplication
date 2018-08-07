using UnityEngine;
using System;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public int parentNum;
    private List<string> destroyableTags = new List<string>();
    [SerializeField] private string[] tags = {"Enemy", "Bullet" };
    [SerializeField] private float timer = 1.5f;    

    public void Init(int playerNum, Transform gun, float speed)
    {
        this.parentNum = playerNum;
        Rigidbody rigidbody = Instantiate(this.gameObject.GetComponent<Rigidbody>(), gun.transform.position, gun.transform.rotation) as Rigidbody;
        rigidbody.AddForce(-gun.forward * speed, ForceMode.VelocityChange);
    }

    void Awake()
    {
        Destroy(this.gameObject, timer);
        foreach (string tag in tags)
            this.destroyableTags.Add(tag);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collide dammit");
        try
        {
            if (this.destroyableTags.Contains(other.gameObject.tag))
            {
                //Bullet bullet = other.gameObject.GetComponent<Bullet>();
                //if (bullet.ParentNum == this.parentNum)
                //    return;
                // Destroys GameObjects that are deemed destroyable.
                Destroy(other.gameObject);
            }
            else
            {
                // Handles damaging a ship.
                ShipView oppShipView = other.gameObject.GetComponent<ShipView>();
                if (oppShipView != null)
                    oppShipView.Damage(this.parentNum);
            }
            Destroy(this.gameObject);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in Bullet's OnCollisionEnter: " + ex.Message.ToString());
        }
    }

}