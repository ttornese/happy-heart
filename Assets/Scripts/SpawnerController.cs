using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controller for spawner objects. Spawner objects are located on each of the
 * four walls in the play area. They spawn enemy bullet game objects at random
 * intervals using a coroutine.
 */
public class SpawnerController : MonoBehaviour {
    // Reference for an Enemy Bullet game object
    public GameObject bullet;
    /**
     *  Determines the direction at which the spawner will shoot bullets.
     *  Must be defined in the editor.
     */
    public string direction;

    /** Reference for a clone of an Enemy Bullet. Used to set the velocity of
     *  the RigideBody component.
     */
    private GameObject bulletClone;
    private IEnumerator coroutine;

    // Initalizes the bullet spawning coroutine.
    void Start ()
    {
        // Shoot a bullet at a random interval
        coroutine = WaitAndShoot(1.5f + Random.Range(0.01f, 2.5f));
        StartCoroutine(coroutine);
    }

    /**
     * Method that spawns an 'Enemy Bullet' GameObject at a random interval.
     * After the GameObject has been instantiated, the RigidBody component
     * of the Gameobject is given a velocity based on the direction of the
     * spawner.
     * 
     * waitTime - a random interval for the coroutine to shoot bullets at
     */
    private IEnumerator WaitAndShoot(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            switch (direction)
            {
                case "East":
                    bulletClone = Instantiate(bullet, new Vector3(10, 0.5f, 0), Quaternion.Euler(90, 0, 0));
                    bulletClone.GetComponent<Rigidbody>().velocity = new Vector3(-3, 0, 0);
                    break;
                case "West":
                    bulletClone = Instantiate(bullet, new Vector3(-10, 0.5f, 0), Quaternion.Euler(90, 0, 0));
                    bulletClone.GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);
                    break;
                case "North":
                    bulletClone = Instantiate(bullet, new Vector3(0, 0.5f, 10), Quaternion.Euler(90, 0, 0));
                    bulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);
                    break;
                case "South":
                    bulletClone = Instantiate(bullet, new Vector3(0, 0.5f, -10), Quaternion.Euler(90, 0, 0));
                    bulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3);
                    break;
            }
        }
    }
}
