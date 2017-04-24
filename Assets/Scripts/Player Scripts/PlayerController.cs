using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Controller script for the Player. This script contains logic to handle
 * user input, the health of the Player, and
 * the UI text related to the state of the Player. The script also contains
 * logic to generate 'bullets' that the player character can shoot at
 * enemies.
 */
public class PlayerController : MonoBehaviour {
    // Reference to the bullet GameObject
    public GameObject bullet;
    private GameObject pauseMenu;
	private GameObject gameOverMenu;
	private GameObject bossKeyDisplay;

    // Reference to the clones created each time a player shoots a bullet
	private GameObject bulletClone;
    // Variable to represent the health of the player. Max/Initial value is 4.
    private int health;
    // A coroutine that controls the scale of the Player
    private IEnumerator coroutine;
    // A boolean to keep track of whether or not the heart should pulse
    private bool pulsed;
	// Keeps track of the horizontal movement based on user input
	private float horizontalMovement;
	// Keeps track of the vertical movement based on user input
	private float verticalMovement;
	// Reference to the RigidBody2D component on the player
	private Rigidbody2D rigidBody;
	// Fire rate for the Player's weapo
	private float fireRate;
	// Time stamp for the last time a player shot a bullet. Controls rate of fire
	private float lastShot;
    private int keyCount;
    private GameObject healthBar;
	public bool hasBossKey;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
        // Ensure that the game runs properly when resetting after winning or dying
        Time.timeScale = 1;
        health = 4;
        pulsed = false;
        coroutine = HeartBeat(1.0f);
        StartCoroutine(coroutine);
		fireRate = 0.5f;
		lastShot = 0.0f;
        keyCount = 0;

        healthBar = GameObject.Find("/Canvas/Health Bar");
        healthBar.GetComponent<HealthDisplayController>().DisplayHealth();
        pauseMenu = GameObject.Find("/Canvas/Pause Menu/");
        gameOverMenu = GameObject.Find("/Canvas/Game Over Menu");
		bossKeyDisplay = GameObject.Find ("/Canvas/Boss Key Display");
		bossKeyDisplay.GetComponent<BossKeyDisplayController> ().SetMarkSprite ();
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

		hasBossKey = false;
	}
	
    /**
     * Update contains the logic for reading user input and checking if
     * the Player has won the game or died. Each of these features have
     * been separated into their own functions.
     */
	void Update () {
        CheckState();
	}

    /**
     * OnTriggerEnter checks for a collision with an enemy bullet. If this
     * collision occurs, the bullet is destroyed and the player's health is
     * decremented by 1.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
		switch (other.gameObject.tag)
		{
		    case "Enemy Bullet":
			    Destroy (other.gameObject);
			    health = health - 1;
			    healthBar.GetComponent<HealthDisplayController> ().DisplayHealth ();
		    	break;
		    case "Wall":
			    rigidBody.velocity = Vector2.zero;
			    break;
		    case "Door":
			    other.gameObject.GetComponentInParent<RoomController> ().setAsCurrentRoom ();
			    break;
		    case "Room Key":
			    Destroy (other.gameObject);
			    AddKey ();
			    break;
		    case "Heart Container":
			    Destroy (other.gameObject);
			    IncrementHealth ();
			    healthBar.GetComponent<HealthDisplayController> ().DisplayHealth ();
			    break;
		    case "Boss Key":
			    Destroy (other.gameObject);
			    hasBossKey = true;
			    bossKeyDisplay.GetComponent<BossKeyDisplayController> ().SetMarkSprite ();
			    break;
		}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.CompareTag ("Locked Door"))
		{
			if (keyCount > 0)
			{
				Destroy (other.gameObject);
				RemoveKey ();
			}
		}
		else if (other.gameObject.CompareTag ("Boss Door"))
		{
			if (hasBossKey)
			{
				Destroy (other.gameObject);
				hasBossKey = false;
				bossKeyDisplay.GetComponent<BossKeyDisplayController> ().SetMarkSprite ();
			}
		}
    }

    /**
     * Handles user input. WASD and R are mapped to control this game and they
     * represent directions in which a user can shoot a bullet. W = up,
     * S = down, A = left, and D = right. R is used to restart the game after
     * a player has either won or died. Any other input is ignored.
     */
    void ReadUserInput()
    {
        /**
         * The input for shooting bullets will only be read when a user
         * hasn't died or won the game.
         */
        if (health > 0)
        {
			Vector3 position = this.transform.position;
			if (Input.GetKey(KeyCode.RightArrow))
            {
                CreateBullet(new Vector2(5, 0));
            }
			else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CreateBullet(new Vector2(-5, 0));
            }
			else if (Input.GetKey(KeyCode.UpArrow))
            {
                CreateBullet(new Vector2(0, 5));
            }
			else if (Input.GetKey(KeyCode.DownArrow))
            {
                CreateBullet(new Vector2(0, -5));
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

	void FixedUpdate()
	{
        ReadUserInput();
		float x = 0, y = 0;

		if (Input.GetKey (KeyCode.W))
		{
			y = 4;
		}
		if (Input.GetKey (KeyCode.A))
		{
			x = -4;
		}
		if (Input.GetKey (KeyCode.S))
		{
			y = -4;
		}
		if (Input.GetKey (KeyCode.D))
		{
			x = 4;
		}
        if(Input.GetKey (KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

		rigidBody.velocity = new Vector2 (x, y);
	}

    /**
     * Given a position and velocity, creates a new Player bullet used to
     * attack an enemy.
     * 
     * position - a given starting position for the new bullet
     * velocity - a given velocity for the new bullet
     */
    void CreateBullet(Vector2 velocity)
    {
		if (Time.time > fireRate + lastShot)
		{
			bulletClone = Instantiate (bullet, this.transform.position, Quaternion.identity);
			bulletClone.GetComponent<Rigidbody2D> ().velocity = velocity;
			bulletClone.GetComponent<SpriteRenderer> ().enabled = true;
			bulletClone.GetComponent<CircleCollider2D> ().enabled = true;
			lastShot = Time.time;
		}
    }

    /**
     * Checks the state of the game. The two conditions being checked for
     * are a death state or a win state. If health = 0, the player is in
     * the death state.
     */
    void CheckState()
    {
        if (health <= 0)
        {
    		gameOverMenu.SetActive (true);
            Time.timeScale = 0;
        }
    }

    /**
     */
    IEnumerator HeartBeat(float waitTime)
    {
        while(true)
        {
            pulsed = !pulsed;
            yield return new WaitForSeconds(waitTime);
            if (pulsed)
            {
                this.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            }
            else
            {
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    void AddKey()
    {
        keyCount += 1;
    }

    void RemoveKey()
    {
        keyCount -= 1;
    }

    // **** PUBLIC FUNCTIONS ****

    /**
     * Returns the health for this Player. This is called in the
     * SpriteController script to help determine which character sprite should
     * be rendered at each frame. The character sprites represent the health of
     * the Player.
     */
    public int GetHealth()
    {
        return health;
    }

    public int GetKeyCount()
    {
        return keyCount;
    }

	public void IncrementHealth()
	{
		health += 1;
	}
}
