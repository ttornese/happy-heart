using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainerController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("hi");
		if (other.gameObject.CompareTag ("Player"))
		{
			other.gameObject.GetComponent<PlayerController> ().IncrementHealth ();
		}
	}

}