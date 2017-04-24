using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public bool isSpawnRoom;
    public List<GameObject> neighbors;

    private GameObject camera;
    private GameObject enemies;
    private bool occupied;

	void Start () {
        camera = GameObject.Find("/Camera");
        occupied = isSpawnRoom;
        enemies = GameObject.Find(gameObject.name + "/Enemies");
        enemies.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setAsUnoccupied()
    {
        occupied = false;
        enemies.SetActive(false);

		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Enemy Bullet");

		for (int i = 0; i < bullets.Length; i++)
		{
			if (bullets [i].name == "Enemy Bullet(Clone)")
			{
				Destroy (bullets [i]);
			}
		}
    }

    public void setAsCurrentRoom()
    {
        occupied = true;

        foreach (GameObject neighbor in neighbors)
        {
            neighbor.GetComponent<RoomController> ().setAsUnoccupied ();
        }

        enemies.SetActive(true);
        camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
    }
}
