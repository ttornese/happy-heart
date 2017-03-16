
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Controller script for the Player. This script contains logic to handle
 * user input, the health of the Player, the Player's current score, and
 * the UI text related to the state of the Player. The script also contains
 * logic to generate 'bullets' that the player character can shoot at
 * enemies.
 */
public class PlayerController : MonoBehaviour {
    // Reference to the bullet GameObject
    public GameObject bullet;
    // Reference to the score text
    public Text scoreText;
    // Reference to the text for a win state
    public Text winText;
    // Reference to the text for a death state
    public Text deathText;
    public GameObject healthUI;
    public GameObject pauseMenu;


    // Reference to the clones created each time a player shoots a bullet
	private GameObject bulletClone;
    // Variable to represent the health of the player. Max/Initial value is 4.
    private int health;
    // Variable to represent the score of the player. Max value is 10.
    private int score;
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

    /**
     * Sets up the initial state of the Player. This includes setting health to
     * 4, the score to 0, the win and death text to empty strings, and the
     * score text to 'Score: 0';
     */
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
        // Ensure that the game runs properly when resetting after winning or dying
        Time.timeScale = 1;
        health = 4;
        score = 0;
        winText.text = "";
        deathText.text = "";
        pulsed = false;
        coroutine = HeartBeat(1.0f);
        StartCoroutine(coroutine);
		fireRate = 0.5f;
		lastShot = 0.0f;
        healthUI.GetComponent<HealthDisplayController>().DisplayHealth(health);
        pauseMenu.active = false;

        SetScoreText();
	}
	
    /**
     * Update contains the logic for reading user input, correctly setting
     * the Player's score, and checking if the Player has won the game or
     * died. Each of these features have been separated into their own
     * functions.
     */
	void Update () {
        SetScoreText();
        CheckState();
	}

    /**
     * OnTriggerEnter checks for a collision with an enemy bullet. If this
     * collision occurs, the bullet is destroyed and the player's health is
     * decremented by 1.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Bullet"))
        {
            Destroy(other.gameObject);
            health = health - 1;
            healthUI.GetComponent<HealthDisplayController>().DisplayHealth(health);
        }
		else if (other.gameObject.CompareTag ("Wall"))
		{
			rigidBody.velocity = Vector2.zero;
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
        if (health > 0 && score < 20)
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
			y = 3;
		}
		if (Input.GetKey (KeyCode.A))
		{
			x = -3;
		}
		if (Input.GetKey (KeyCode.S))
		{
			y = -3;
		}
		if (Input.GetKey (KeyCode.D))
		{
			x = 3;
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
     * the death state. If score = 20, the player has entered a win state.
     */
    void CheckState()
    {
        if (health == 0)
        {
            Time.timeScale = 0;
            deathText.text = "YOU DIED.\nPress 'R' to restart.";
        }
        else if (score == 20)
        {
            Time.timeScale = 0;
            winText.text = "YOU WIN.\nPress 'R' to restart.";
        }
    }

    /**
     * Sets a Text object to the correct value for the score.
     */
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
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

    /**
     * Adds 1 to the score for this Player. This is called in the
     * BulletController whenever a Player bullet collides with an
     * enemy bullet.
     */
    public void incrementScore()
    {
        score += 1;
    }
}
