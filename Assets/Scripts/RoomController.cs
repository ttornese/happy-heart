using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public bool isSpawnRoom;
    public List<GameObject> neighbors;

    private GameObject camera;
    private bool occupied;

	void Start () {
        camera = GameObject.Find("/Camera");
        occupied = isSpawnRoom;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setAsUnoccupied()
    {
        occupied = false;
    }

    public void setAsCurrentRoom()
    {
        occupied = true;

        foreach (GameObject neighbor in neighbors)
        {
            neighbor.GetComponent<RoomController> ().setAsUnoccupied ();
        }

        camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
    }
}
