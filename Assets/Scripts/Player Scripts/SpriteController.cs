using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script to control the sprite being rendered for the player. In this game,
 * the health of the player is not represented as a number, but as a facial
 * expression on the player character. This script will change the facial
 * expression on the player to represent how much health the have left.
 */
public class SpriteController : MonoBehaviour {
    // Representations for the different sprites a player can be.
    public Sprite fullHealthSprite; // 4 health
    public Sprite mediumHealthSprite; // 3-2 health
    public Sprite lowHealthSprite; // 1 health
    // Reference to Player GameObject
    public GameObject player;

    // Reference to the spriteRenderer component of this GameObject.
    private SpriteRenderer spriteRenderer;

    /**
     * Initializes the a reference to this object's SpriteRenderer.
     */
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
    /**
     * Checks the health of the player at each frame. Renders an appropriate
     * sprite based on the health of the player.
     */
	void Update ()
    {
        int health = player.GetComponent<PlayerController>().GetHealth();
        if (health >= 4)
        {
            spriteRenderer.sprite = fullHealthSprite;
        }
        else if (health > 1 && health < 4)
        {
            spriteRenderer.sprite = mediumHealthSprite;
        }
        else
        {
            spriteRenderer.sprite = lowHealthSprite;
        }
		
	}
}
