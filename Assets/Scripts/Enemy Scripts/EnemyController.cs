using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller script for an Enemy. This enemy will chase the player
 * and shoot bullets out in four directions on a cycle.
 */
public class EnemyController : MonoBehaviour {
	// Reference to Enemy Bullet prefab
	public GameObject enemyBullet;

	// Reference to the player
	private GameObject player;
	// Reference to RigidBody2D component on this Enemy
	private Rigidbody2D rigidBody;
	private int health;
	// Controls the rate at which the enemy shoots bullets
	private float fireRate;
	private float lastShot;
	// Refrences to bullet clones that are created each time
	// this Enemy shoots
	private GameObject upBullet;
	private GameObject downBullet;
	private GameObject leftBullet;
	private GameObject rightBullet;

	void Start ()
	{
        player = GameObject.Find("/Player");
		rigidBody = this.GetComponent<Rigidbody2D> ();
		health = 2;
		fireRate = 3.0f;
		lastShot = 0.0f;
	}

	void Update () {
		FollowPlayer ();
		ShootBullets ();
        DestroyIfAlive ();
	}

	/**
	 * Contains logic to follow the player around the map.
	 */
	void FollowPlayer()
	{
		Vector2 playerPosition = player.transform.position;
		Vector2 enemyPosition = this.transform.position;
		float velocityX = 1.0f, velocityY = 1.0f;

		if (enemyPosition.x > playerPosition.x) {
			velocityX = -1.0f;
		}

		if (enemyPosition.y > playerPosition.y)
		{
			velocityY = -1.0f;
		}
		rigidBody.velocity = new Vector2 (velocityX, velocityY);

	}

	/**
	 * Uses lastShot and fireRate to determine if this Enemy should shoot bullets.
	 * The bullets are shot on an interval in all four cardinal directions.
     */
	void ShootBullets()
	{
		if (Time.time > fireRate + lastShot)
		{
			upBullet = Instantiate (enemyBullet, gameObject.transform.position, Quaternion.identity);
			upBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(0.0f, 1.0f);
			upBullet.GetComponent<SpriteRenderer> ().enabled = true;

            downBullet = Instantiate (enemyBullet, gameObject.transform.position, Quaternion.identity);
			downBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(0.0f, -1.0f);
			downBullet.GetComponent<SpriteRenderer> ().enabled = true;

			leftBullet = Instantiate (enemyBullet, gameObject.transform.position, Quaternion.identity);
			leftBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(-1.0f, 0.0f);
			leftBullet.GetComponent<SpriteRenderer> ().enabled = true;

			rightBullet = Instantiate (enemyBullet, gameObject.transform.position, Quaternion.identity);
			rightBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(1.0f, 0.0f);
			rightBullet.GetComponent<SpriteRenderer> ().enabled = true;

			lastShot = Time.time;
		}
	}

    void DestroyIfAlive()
    {
        if (health == 0)
        {
            Destroy(this.gameObject);
        }
    }

	// *** PUBLIC FUNCTIONS ***
	/**
	 * Removes health from this enemy when it is shot by the player.
	 */
	public void DecrementHealth()
	{
		health -= 1;
	}
}
