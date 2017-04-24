using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomKeyDisplayController : MonoBehaviour {
    public GameObject player;
    public Text keyCount;

	// Update is called once per frame
	void Update () {
        keyCount.text = "x " + player.GetComponent<PlayerController>().GetKeyCount().ToString();
	}
}
