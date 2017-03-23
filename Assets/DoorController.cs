using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public GameObject player;
    public GameObject doorParter;
    public GameObject roomCamera;
    public GameObject room;
    private bool doorEntered;

	// Use this for initialization
	void Start () {
        doorEntered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TeleportPlayerToOtherDoor()
    {
        if (!doorEntered)
        {
            player.transform.position = new Vector3(doorParter.transform.position.x, doorParter.transform.position.y, -1);
            SetCamera(false);
            room.SetActive(false);
            doorParter.GetComponent<DoorController>().room.SetActive(true);
            doorParter.GetComponent<DoorController>().FlipDoor(true);
            doorParter.GetComponent<DoorController>().SetCamera(true);
        }
    }

    public void FlipDoor(bool entered)
    {
        doorEntered = entered;
    }

    public void SetCamera(bool onOrOff)
    {
        roomCamera.SetActive(onOrOff);
    }
}
