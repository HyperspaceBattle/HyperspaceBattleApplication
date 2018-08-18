using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private bool canHyperspeed = true;  //pause state
    private Ship ship;
    private CharacterController charController;
    private float hyperSpeed;
    private float speed;
    private float gameTime;
    private float lastPressed;
    private bool hasInitialized = false;
    private const int FIRERATE = 1000;

    public void Init(GameObject parent)
    {
        this.ship = parent.GetComponent<Ship>();
        this.speed = this.ship.Model.SpeedNormal;
        this.hyperSpeed = this.ship.Model.HyperSpeedMax;

        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        this.ship.Model.Player = ReInput.players.GetPlayer(this.ship.Model.PlayerID);
        this.ship.Model.Player.AddInputEventDelegate(OnPausePress, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        this.ship.Model.Player.AddInputEventDelegate(OnSpecialPress, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Select");
        //this.ship.Model.Player.AddInputEventDelegate(OnSpecialPress, UpdateLoopType.Update, InputActionEventType.ButtonPressedForTime, "Select", new object[]{ 5.0f });
        this.ship.Model.Player.AddInputEventDelegate(OnHyperSpeedPress, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "HyperSpeed");
        this.ship.Model.Player.AddInputEventDelegate(OnHyperSpeedRelease, UpdateLoopType.Update, InputActionEventType.ButtonUnpressed, "HyperSpeed");
        this.ship.Model.Player.AddInputEventDelegate(OnFirePress, UpdateLoopType.Update, InputActionEventType.AxisActive, "RS Fire Horizontal");
        this.ship.Model.Player.AddInputEventDelegate(OnFirePress, UpdateLoopType.Update, InputActionEventType.AxisActive, "RS Fire Vertical");

        // Get the character controller
        this.charController = this.ship.gameObject.GetComponent<CharacterController>();
        
        this.gameTime = 0f;
        this.lastPressed = 0f;
        this.hasInitialized = true;
    }


    void FixedUpdate()
    {        
        try
        {
            if (AppManager.IsUnpaused && this.hasInitialized)
            {
                this.gameTime += Time.deltaTime * 1000;

                if (this.hyperSpeed >= this.ship.Model.HyperSpeedMax)
                {
                    this.hyperSpeed = this.ship.Model.HyperSpeedMax * .999F;
                }

                if (this.hyperSpeed <= 0)
                {
                    this.hyperSpeed = 0;
                }
            
                Vector3 rotation = new Vector3(this.ship.Model.Player.GetAxis(this.ship.Model.HorizontalRotation), 0, this.ship.Model.Player.GetAxis(this.ship.Model.VerticalRotation)) + this.ship.View.gameObject.transform.position;
                this.ship.View.gameObject.transform.LookAt(rotation, Vector3.up);
            
                //Axis Movement, uses controller.move for precise collision using character controller
                float horizontalInput = this.ship.Model.Player.GetAxis("LS Move Horizontal");            // get input by name or action id
                float verticalInput = this.ship.Model.Player.GetAxis("LS Move Vertical");
                if (horizontalInput != 0.0f || verticalInput != 0.0f)
                {
                    Vector3 direction = new Vector3(horizontalInput * Time.deltaTime, 0, verticalInput * Time.deltaTime).normalized * this.speed;
                    this.charController.Move(direction);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PlayerCharacterController's FixedUpdate: " + ex.Message.ToString());
        }
    }

    void OnFirePress(InputActionEventData data)
    {
        try
        {
            if (AppManager.IsUnpaused)
            {
                if ((this.gameTime - this.lastPressed) > (FIRERATE / this.ship.Model.BulletDelayTime))
                {
                    if (!this.ship.Model.Player.GetButton("Hyperspeed"))
                    {
                        foreach (GameObject gun in this.ship.Model.Guns)
                        {
                            GameObject goBullet = Instantiate(this.ship.Model.Bullet, gun.transform.position, gun.transform.rotation);
                            Bullet bullet = goBullet.GetComponent<Bullet>();
                            bullet.Init(this.ship.Model.PlayerNumber, gun.transform, this.ship.Model.BulletVelocity, this.ship.Model.ShipColor);
                            gun.GetComponent<AudioSource>().Play();
                            this.lastPressed = this.gameTime;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in ShipController's OnFirePress: " + ex.Message.ToString());
        }
    }
    void OnHyperSpeedRelease(InputActionEventData data)
    {        
        try
        {
            if (AppManager.IsUnpaused)
            {
                this.hyperSpeed += this.ship.Model.HyperRestoreRate * Time.deltaTime;
                this.speed = this.ship.Model.SpeedNormal;
                this.ship.View.ChargingFX.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PlayerCharacterController's OnHyperSpeedRelease: " + ex.Message.ToString());
        }
    }

    void OnHyperSpeedPress(InputActionEventData data)
    {        
        try
        {            
            if (AppManager.IsUnpaused)
            {
                //but you have less than enough energy to start
                if (this.hyperSpeed <= 0)
                {
                    canHyperspeed = false;
                }
                // you have enough to start
                if (this.hyperSpeed >= this.ship.Model.HyperSpeedMax * .25f)
                {
                    canHyperspeed = true;
                }
                //the actual line to speed you up and restore passively
                if (canHyperspeed)
                {
                    this.speed = this.ship.Model.SpeedMax;
                    this.hyperSpeed -= this.ship.Model.HyperExhaustRate * Time.deltaTime;
                    this.ship.View.ChargingFX.SetActive(true);
                }
                else
                {
                    this.speed = this.ship.Model.SpeedNormal;
                    //hyperEnergy += hyperRestoreRate * Time.deltaTime;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PlayerCharacterController's OnHyperSpeedPress: " + ex.Message.ToString());
        }
    }

    void OnPausePress(InputActionEventData data)
    {        
        try
        {
            AppManager.Pause();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in PlayerCharacterController ( " + this.ship.Model.PlayerNumber +") OnPausePress: " + ex.Message.ToString());
        }
    }

    void OnSpecialPress(InputActionEventData data)
    {
        if (AppManager.IsUnpaused)
        {
            Debug.Log("It Works");
        }
    }
 }
