using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller script for an Enemy. This enemy will chase the player
 * and shoot bullets out in four directions on a cycle.
 */
public class TurretEnemy: MonoBehaviour {
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
	private GameObject bulletClone;
    private int speed;

	void Start ()
	{
        player = GameObject.Find("/Player");
		rigidBody = this.GetComponent<Rigidbody2D> ();
		health = 2;
        fireRate = Random.Range(2.5f, 4.0f);
        speed = 3;
		lastShot = 0.0f;
	}

	void Update () {
		ShootBullets ();
        DestroyIfAlive ();
	}

	/**
	 * Uses lastShot and fireRate to determine if this Enemy should shoot bullets.
	 * The bullets are shot on an interval in all four cardinal directions.
     */
	void ShootBullets()
	{
		if (Time.time > fireRate + lastShot)
		{
		Vector2 playerPosition = player.transform.position;
		Vector2 enemyPosition = gameObject.transform.position;
		float velocityX = 1.0f, velocityY = 1.0f;


        bulletClone = Instantiate(enemyBullet, gameObject.transform.position, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody2D> ().velocity = (player.transform.position - gameObject.transform.position).normalized * speed;
            bulletClone.GetComponent<SpriteRenderer> ().enabled = true;

		lastShot = Time.time;
		}
	}

    void DestroyIfAlive()
    {
        if (health == 0)
        {
            Destroy(gameObject);
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
