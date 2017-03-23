using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller for the bullets created by the player character. Contains logic
 * for detecting collisions that bullets make with the edge of the play field
 * and enemy bullets.
 */
public class BulletController : MonoBehaviour {
    public GameObject player;

    /**
     * OnTriggerEnter checks for a collision with both the wall and an enemy
     * bullet. The tag 'Wall' is used on GameObjects that represent a wall and
     * 'Enemy Bullet' is used for GameObjects that represent an enemy bullet.
     * When colliding with a wall, this GameObject will be destroyed. When
     * colliding with an enemy bullet, this GameObject and the enemy bullet
     * GameObject will be destroyed. The player's score will also be
     * incremented.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
			other.GetComponent<EnemyController> ().DecrementHealth ();
            Destroy(this.gameObject);
            player.GetComponent<PlayerController>().incrementScore();
        }
    }
}
