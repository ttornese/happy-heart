using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenHeartController : MonoBehaviour
{
	private GameObject bullet;
	private bool direction;
	private int health;
	private IEnumerator shootCoroutine;

	public void SetUp()
	{
		bullet = (GameObject)Resources.Load ("Enemy Prefabs/Enemy Bullet");
		direction = true;
		health = 10;
		shootCoroutine = ShootBullets ();
		StartCoroutine (shootCoroutine);
	}

	void Update ()
	{
		if (health == 0)
		{
			Destroy (gameObject);
		}

		if (transform.localPosition.y >= 2.5f)
		{
			direction = false;
		}
		else if (transform.localPosition.y <= -2.5f)
		{
			direction = true;
		}

		if (direction)
		{
			transform.localPosition += new Vector3 (0, 0.05f, 0);
		}
		else
		{
			transform.localPosition += new Vector3 (0, -0.05f, 0);
		}
	}

	private IEnumerator ShootBullets()
	{
		GameObject bulletClone;

		while (true)
		{
			for (int i = 0; i < 12; i++)
			{
				float theta = i * 2 * Mathf.PI / 12;
				float x = Mathf.Sin (theta) * 2;
				float y = transform.localPosition.y + (Mathf.Cos (theta) * 2);

				bulletClone = Instantiate (bullet);
				bulletClone.transform.SetParent (transform.parent.Find("Boss Bullets"));
				bulletClone.transform.localPosition = new Vector3 (-x, y, 0);

				bulletClone.GetComponent<Rigidbody2D> ().velocity = bulletClone.transform.localPosition.normalized;
			}

			yield return new WaitForSeconds (5.0f);
		}
	}

	public void DecrementHealth()
	{
		health -= 1;
		Debug.Log (health);
	}
}
