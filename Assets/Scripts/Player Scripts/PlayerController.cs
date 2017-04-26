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
public class PlayerController : MonoBehaviour
{
    //  **** UI Variables ****
	private GameObject bossKeyDisplay;
	private GameObject gameOverMenu;
    private GameObject healthBar;
    private GameObject pauseMenu;
	private GameObject powerUpDisplay;

	// **** Player State Variables ****
    private int health; // Health of player
    private int keyCount; // number of room keys a player has
	private string powerUpName; // name of power up if player has one
	private int shieldHealth; // shield health if player has powerup

	public bool hasBossKey; // true if player has a boss key, false otherwise

	// **** Shooting Variables ****
	private GameObject bullet; // player bullet prefab
	private float fireRate; // how often a player can shoot
	private float lastShot; // timestamp of the player's last shot
	// private GameObject bulletClone;


	// **** Movement Variables ****
	private float horizontalMovement;
	private Rigidbody2D rigidBody;
	private float verticalMovement;

	// **** Pulse Animation variables ****
    private IEnumerator coroutine; // changes the scale for the pulse animation
    private bool pulsed; // used to alternate the player's scale for the pulse

	void Start ()
	{
        Time.timeScale = 1;

		bossKeyDisplay = GameObject.Find ("/Canvas/Boss Key Display");
		bossKeyDisplay.GetComponent<BossKeyDisplayController> ().SetMarkSprite ();
        gameOverMenu = GameObject.Find("/Canvas/Game Over Menu");
        gameOverMenu.SetActive(false);
        healthBar = GameObject.Find("/Canvas/Health Bar");
        healthBar.GetComponent<HealthDisplayController>().DisplayHealth();
        pauseMenu = GameObject.Find("/Canvas/Pause Menu/");
		powerUpDisplay = GameObject.Find ("/Canvas/Power Up Display");
        pauseMenu.SetActive(false);

		hasBossKey = false;
        health = 4;
        keyCount = 0;
		powerUpName = null;
		shieldHealth = 0;

		bullet = transform.Find ("Bullet").gameObject;
		fireRate = 0.5f;
		lastShot = 0.0f;

		rigidBody = GetComponent<Rigidbody2D> ();

        pulsed = false;
        coroutine = HeartBeat(1.0f);
        StartCoroutine(coroutine);
	}


    private void CheckState()
    {
        if (health <= 0)
        {
    		gameOverMenu.SetActive (true);
            Time.timeScale = 0;
        }
    }

	void Update ()
	{
        CheckState();
	}

	void HandleHit(GameObject enemyBullet)
	{
		if (powerUpName == "Cheerio")
		{
			if (shieldHealth > 1)
			{
				shieldHealth -= 1;
			}
			else
			{
				shieldHealth = 0;
			    powerUpDisplay.GetComponent<PowerUpDisplayController> ().RenderPowerUpIcon (null);
				powerUpName = null;
			}
		}
		else
		{
			health = health - 1;
			healthBar.GetComponent<HealthDisplayController> ().DisplayHealth ();
		}

		Destroy (enemyBullet);
	}

    private void AddKey()
    {
        keyCount += 1;
    }

    private void RemoveKey()
    {
        keyCount -= 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		switch (other.gameObject.tag)
		{
		    case "Enemy Bullet":
			    HandleHit (other.gameObject);
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
		    case "Cheerio":
			    Sprite powerUpSprite = other.gameObject.GetComponent<SpriteRenderer> ().sprite;
			    powerUpDisplay.GetComponent<PowerUpDisplayController> ().RenderPowerUpIcon (powerUpSprite);
			    Destroy (other.gameObject);
			    shieldHealth = 2;
			    powerUpName = "Cheerio";
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

    private void CreateBullet(Vector2 velocity)
    {
		if (Time.time > fireRate + lastShot)
		{
			GameObject bulletClone = Instantiate (bullet, this.transform.position, Quaternion.identity);
			bulletClone.GetComponent<Rigidbody2D> ().velocity = velocity;
			bulletClone.GetComponent<SpriteRenderer> ().enabled = true;
			bulletClone.GetComponent<CircleCollider2D> ().enabled = true;
			lastShot = Time.time;
		}
    }

    private void ReadShootingInput()
    {
        if (health > 0)
        {
			Vector3 position = transform.position;

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
    }

	private void ReadMovementInput()
	{
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

		rigidBody.velocity = new Vector2 (x, y);
	}

	void FixedUpdate()
	{
        ReadShootingInput ();
		ReadMovementInput ();

		// Open pause menu
        if(Input.GetKey (KeyCode.Escape))
        {
            pauseMenu.SetActive (true);
            Time.timeScale = 0;
        }
	}

    private IEnumerator HeartBeat(float waitTime)
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

    // **** PUBLIC FUNCTIONS ****
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
