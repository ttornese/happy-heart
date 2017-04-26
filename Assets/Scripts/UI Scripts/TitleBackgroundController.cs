using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackgroundController : MonoBehaviour {
    private GameObject backgroundSprite;
    private List<GameObject> sprites;
    private GameObject tempSprite;

    void Start()
    {
        backgroundSprite = GameObject.Find("/Background/Background Image");
        sprites = new List<GameObject>();
        sprites.Add(Instantiate(backgroundSprite, backgroundSprite.transform.position, Quaternion.identity));
        sprites[0].transform.SetParent(gameObject.transform);
        sprites[0].GetComponent<Rigidbody2D> ().velocity = new Vector3(1, 0, 0);
        backgroundSprite.SetActive(false);
    }

    void Update()
    {
        if (sprites[0].transform.position.x >= 2.5)
        {
            tempSprite = sprites[0];

            if (sprites.Count == 2)
            {
                if (sprites[1].transform.position.x > 21.4)
                {
                    Destroy(sprites[1]);
                }
            }

            sprites = new List<GameObject>();
            sprites.Add(Instantiate(backgroundSprite, new Vector3(-21.4f, 0, 0), Quaternion.identity));
            sprites[0].SetActive(true);
            sprites[0].GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0, 0);
            sprites[0].transform.SetParent(gameObject.transform);

            sprites.Add(tempSprite);
        }
    } 

    /*
	// Use this for initialization
	void Start () {
        moveXRight = true;
        moveXLeft = true;
        moveYUp = false;
        moveYDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = gameObject.transform.position;
        if (moveXRight)
        {
            if (position.x < 1.5f)
            {
                gameObject.transform.Translate(new Vector3(0.01f, 0, 0));
            } else
            {
                moveXRight = false;
                moveXLeft = true;
            }
        }
        else if (moveXLeft)
        {
            if (position.x > -1.5f)
            {
                gameObject.transform.Translate(new Vector3(-0.01f, 0, 0));
            } else
            {
                moveXLeft = false;
                moveYUp = true;
            }
        }
        else if (moveYUp)
        {
            if (position.y < 1.5f)
            {
                gameObject.transform.Translate(new Vector3(0, 0.01f, 0));
            }
            else
            {
                moveYUp = false;
                moveYDown = true;
            }
        }
        else if (moveYDown)
        {
            if (position.y > -1.5f)
            {
                gameObject.transform.Translate(new Vector3(0, -0.01f, 0));
            }
            else
            {
                moveYDown = false;
                moveXRight = true;
            }
        }
	}
    */
}
