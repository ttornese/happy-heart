using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller script for an Enemy. This enemy will chase the player
 * and shoot bullets out in four directions on a cycle.
 */
public class EnemyController : MonoBehaviour {
	private int health;

	// Reference to the player
	private GameObject player;

	// Reference to RigidBody2D component on this Enemy
	private Rigidbody2D rigidBody;

	// **** Shooting Variables ****
	private float fireRate;
	private float lastShot;
	private GameObject bullet;
	private GameObject downBullet;
	private GameObject leftBullet;
	private GameObject rightBullet;
	private GameObject upBullet;

	void Start ()
	{
		bullet = (GameObject)Resources.Load ("Enemy Prefabs/Enemy Bullet");
        player = GameObject.Find("/Player");
		rigidBody = this.GetComponent<Rigidbody2D> ();
		health = 2;
		fireRate = 3.0f;
		lastShot = 0.0f;
	}

	void Update ()
	{
		FollowPlayer ();
		ShootBullets ();
		DestroyIfDead ();
	}

	private void FollowPlayer()
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
	private void ShootBullets()
	{
		if (Time.time > fireRate + lastShot)
		{
			upBullet = Instantiate (
				bullet,
				gameObject.transform.position,
				Quaternion.identity
			);
			upBullet.GetComponent<Rigidbody2D> ().velocity = Vector3.up;
			upBullet.GetComponent<SpriteRenderer> ().enabled = true;
			upBullet.transform.SetParent(gameObject.transform.parent.parent.transform);

            downBullet = Instantiate (
				bullet,
				gameObject.transform.position,
				Quaternion.identity
			);
			downBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(0.0f, -1.0f);
			downBullet.GetComponent<SpriteRenderer> ().enabled = true;
			downBullet.transform.SetParent(gameObject.transform.parent.parent.transform);

			leftBullet = Instantiate (
				bullet,
				gameObject.transform.position,
				Quaternion.identity
			);
			leftBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(-1.0f, 0.0f);
			leftBullet.GetComponent<SpriteRenderer> ().enabled = true;
			leftBullet.transform.SetParent(gameObject.transform.parent.parent.transform);

			rightBullet = Instantiate (
				bullet,
				gameObject.transform.position,
				Quaternion.identity
			);
			rightBullet.GetComponent<Rigidbody2D> ().velocity = new Vector2(1.0f, 0.0f);
			rightBullet.GetComponent<SpriteRenderer> ().enabled = true;
			leftBullet.transform.SetParent(gameObject.transform.parent.parent.transform);

			lastShot = Time.time;
		}
	}

	private void DestroyIfDead()
    {
        if (health == 0)
        {
            Destroy (gameObject);
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
