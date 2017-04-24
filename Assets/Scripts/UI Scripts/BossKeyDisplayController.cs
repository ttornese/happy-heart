using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKeyDisplayController : MonoBehaviour
{
	private GameObject xMark;
	private GameObject checkMark;
	private GameObject player;

	void Start()
	{
		xMark = (GameObject)Resources.Load ("UI Prefabs/X Mark Display");
		checkMark = (GameObject)Resources.Load ("UI Prefabs/Check Mark Display");
		player = GameObject.Find ("/Player");
	}

	public void SetMarkSprite()
	{
		RectTransform cloneRT;

		if (transform.childCount > 1)
		{
			Destroy (transform.GetChild (1).gameObject);
		}

		if (player.GetComponent<PlayerController> ().hasBossKey)
		{
		    GameObject checkMarkClone = Instantiate (checkMark);
		    checkMarkClone.transform.SetParent (transform);
		    cloneRT = checkMarkClone.GetComponent<RectTransform> ();
		    cloneRT.anchoredPosition = new Vector3 (-75, -10, 0);
		    cloneRT.localScale = new Vector3 (10, 10, 10);
		}
		else
		{
		    GameObject xMarkClone = Instantiate (xMark);
		    xMarkClone.transform.SetParent (transform);
		    cloneRT = xMarkClone.GetComponent<RectTransform> ();
		    cloneRT.anchoredPosition = new Vector3 (-75, -10, 0);
		    cloneRT.localScale = new Vector3 (10, 10, 10);
		}
			
	}
}
