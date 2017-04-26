using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
		if (!other.gameObject.CompareTag ("Enemy") &&
			!other.gameObject.CompareTag ("Turret") &&
			!other.gameObject.CompareTag ("Broken Heart") &&
			!other.gameObject.CompareTag ("Enemy Bullet"))
		{
			Destroy (gameObject);
		}
    }
}