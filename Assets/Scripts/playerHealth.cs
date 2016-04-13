﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
    public float health = 100f;                         // How much health the player has left.

    public float AfterDeathTime = 5f;              // How much time from the player dying to the level reseting.
   // public AudioClip deathClip;         ----->                The sound effect of the player dying.

	private PlayerController playerMovement;              // Reference to the player movement script.
    private SceneFadeInOut sceneFadeInOut;              // Reference to the SceneFadeInOut script.
    private LastPlayerSighting lastPlayerSighting;      // Reference to the LastPlayerSighting script.
    private float timer;                                // A timer for counting to the reset of the level once the player is dead.
    private bool deadYesOrNo;                            // A bool to show if the player is dead or not.

	#region : life bar attributs
	[SerializeField]
	float MaxHealth = 100f;

	Rect healthRec;
	Texture2D healthTexture;

	#endregion

    void Awake()
    {
       
		playerMovement = GetComponent<PlayerController>();
       
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }


    void Update()
    {
        
        if (health <= 0f)
        {
            if (deadYesOrNo)
            {
                PlayerDead();
                LevelReset();
            }
        }
    }

    void PlayerDead()
    {

        // Disable the movement.
        playerMovement.enabled = false;
        
        deadYesOrNo = true;
        
        lastPlayerSighting.position = lastPlayerSighting.resetPosition;
        
    }



    /* void PlayerDying()
    {


        // Play the dying sound effect at the player's location.
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    */
    
    void LevelReset()
    {
        timer += Time.deltaTime;

        //If the timer is greater than or equal to the time before the level resets...
        if (timer >= AfterDeathTime)
            // ... reset the level.
            sceneFadeInOut.EndScene();
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
    }

	#region : life bar function
	void OnGUI()
	{
		//Compute the ratio
		float healthRatio = health / MaxHealth;

		//the bar progression by the rect size
		float RectWidth = healthRatio * Screen.width * 1 / 3;
		healthRec.width = RectWidth;

		//Draw the bar
		GUI.DrawTexture(healthRec, healthTexture);

	}
	#endregion
}
