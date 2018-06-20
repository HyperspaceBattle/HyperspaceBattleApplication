using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private bool canHyperspeed = true;
    private bool isPaused = false;     //pause state
    private bool canPause = true;

    private Player player; // The Rewired Player
    private PlayerCharacter character;
    
    
    private CharacterController charController;
    private float hyperSpeed;
    private float speed;
    private float gameTime;
    private float lastPressed;
    private bool hasInitialized = false;

    public void Init(GameObject parent)
    {
        this.character = parent.GetComponent<PlayerCharacter>();
        this.speed = this.character.Model.SpeedNormal;
        this.hyperSpeed = this.character.Model.HyperSpeedMax;

        //rewire start settings
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        this.player = ReInput.players.GetPlayer(this.character.Model.PlayerID);
        this.player.AddInputEventDelegate(OnPausePress, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        this.player.AddInputEventDelegate(OnHyperSpeedPress, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "HyperSpeed");
        this.player.AddInputEventDelegate(OnHyperSpeedRelease, UpdateLoopType.Update, InputActionEventType.ButtonUnpressed, "HyperSpeed");
        this.player.AddInputEventDelegate(OnFirePress, UpdateLoopType.Update, InputActionEventType.AxisActive, "RS Fire Horizontal");
        this.player.AddInputEventDelegate(OnFirePress, UpdateLoopType.Update, InputActionEventType.AxisActive, "RS Fire Vertical");

        // Get the character controller
        this.charController = this.character.gameObject.GetComponent<CharacterController>();
        
        this.gameTime = 0f;
        this.lastPressed = 0f;
        this.hasInitialized = true;
    }


    void FixedUpdate()
    {        
        try
        {
            if (!this.isPaused && this.hasInitialized)
            {
                this.gameTime += Time.deltaTime * 1000;

                if (this.hyperSpeed >= this.character.Model.HyperSpeedMax)
                {
                    this.hyperSpeed = this.character.Model.HyperSpeedMax * .999F;
                }

                if (this.hyperSpeed <= 0)
                {
                    this.hyperSpeed = 0;
                }
            
                Vector3 rotation = new Vector3(player.GetAxis(this.character.Model.HorizontalRotation), 0, player.GetAxis(this.character.Model.VerticalRotation)) + this.character.View.gameObject.transform.position;
                this.character.View.gameObject.transform.LookAt(rotation, Vector3.up);
            
                //Axis Movement, uses controller.move for precise collision using character controller
                float horizontalInput = player.GetAxis("LS Move Horizontal");            // get input by name or action id
                float verticalInput = player.GetAxis("LS Move Vertical");
                if (horizontalInput != 0.0f || verticalInput != 0.0f)
                {
                    Vector3 direction = new Vector3(horizontalInput * Time.deltaTime, 0, verticalInput * Time.deltaTime).normalized * this.speed;
                    this.charController.Move(direction);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in FixedUpdate: " + ex.Message.ToString());
        }
    }

    void OnFirePress(InputActionEventData data)
    {
        try
        {
            if (!this.isPaused)
            {
                if ((this.gameTime - this.lastPressed) > (1000 / this.character.Model.BulletDelayTime))
                {
                    if (!player.GetButton("Hyperspeed"))
                    {
                        foreach (GameObject gun in this.character.Model.Guns)
                        {
                            GameObject bullet = (GameObject)Resources.Load(this.character.Model.Bullet);
                            this.character.View.SetBulletColor(bullet);
                            Rigidbody newBullet = Instantiate(bullet.GetComponent<Rigidbody>(), gun.transform.position, gun.transform.rotation) as Rigidbody;
                            newBullet.AddForce(-gun.transform.forward * this.character.Model.BulletVelocity, ForceMode.VelocityChange);
                            gun.GetComponent<AudioSource>().Play();
                            this.lastPressed = this.gameTime;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in OnFirePress: " + ex.Message.ToString());
        }
    }
    void OnHyperSpeedRelease(InputActionEventData data)
    {        
        try
        {
            if (!this.isPaused)
            {
                this.hyperSpeed += this.character.Model.HyperRestoreRate * Time.deltaTime;
                this.speed = this.character.Model.SpeedNormal;
                this.character.View.ChargingFX.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in OnHyperSpeedRelease: " + ex.Message.ToString());
        }
    }

    void OnHyperSpeedPress(InputActionEventData data)
    {        
        try
        {
            if (!isPaused)
            {
                //but you have less than enough energy to start
                if (this.hyperSpeed <= 0)
                {
                    canHyperspeed = false;
                }
                // you have enough to start
                if (this.hyperSpeed >= this.character.Model.HyperSpeedMax * .25f)
                {
                    canHyperspeed = true;
                }
                //the actual line to speed you up and restore passively
                if (canHyperspeed)
                {
                    this.speed = this.character.Model.SpeedMax;
                    this.hyperSpeed -= this.character.Model.HyperExhaustRate * Time.deltaTime;
                    this.character.View.ChargingFX.SetActive(true);
                }
                else
                {
                    this.speed = this.character.Model.SpeedNormal;
                    //hyperEnergy += hyperRestoreRate * Time.deltaTime;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in OnHyperSpeedPress: " + ex.Message.ToString());
        }
    }

    void OnPausePress(InputActionEventData data)
    {        
        try
        {
            if (isPaused)
            {
                //Debug.Log("Game Continued");
                if (canPause)
                    Time.timeScale = 1;
                //pausePanel.SetActive(false);
                //this.character.gameObject.SetActive(true);
                //enable the scripts again
                this.isPaused = false;
            }
            else
            {
                //Debug.Log("Game Pause");
                if (canPause)
                    Time.timeScale = 0;
                //pausePanel.SetActive(true);
                //Disable scripts that still work while timescale is set to 0
                //this.character.gameObject.SetActive(false);
                this.isPaused = true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error in OnPausePress: " + ex.Message.ToString());
        }
    }

    public void Explode(GameObject explosion)
    {
        Time.timeScale = .1f;
        //spawn explosion
        Instantiate(explosion, this.character.View.transform.position, this.character.View.transform.rotation);
        this.character.gameObject.SetActive(false);
        Time.timeScale = 1f;

        player.RemoveInputEventDelegate(OnPausePress);
        player.RemoveInputEventDelegate(OnHyperSpeedPress);
        player.RemoveInputEventDelegate(OnHyperSpeedRelease);
        player.RemoveInputEventDelegate(OnFirePress);

        Destroy(this.character);
    }


}
