using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDisplayController : MonoBehaviour
{

	private GameObject player;

	void Start()
	{
		player = GameObject.Find ("/Player");
	}

	public void RenderPowerUpIcon(Sprite sprite)
	{
		transform.Find ("Power Up Sprite").gameObject.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
}
