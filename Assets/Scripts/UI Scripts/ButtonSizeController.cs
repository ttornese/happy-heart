using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSizeController : MonoBehaviour {
    private bool growAndShrink;
    private bool growing;

	// Use this for initialization
	void Start () {
        growAndShrink = false;
        growing = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = gameObject.transform.localScale;
        if (growAndShrink)
        {
            if (scale.x < 1.25f && scale.y < 1.25f)
            {
                gameObject.transform.localScale = new Vector3(scale.x + 0.00625f, scale.y + 0.00625f, scale.z + 0.00625f);
            }
            else
            {
                growing = false;
            }

            if (!growing && scale.x > 1 && scale.y > 1)
            {
                gameObject.transform.localScale = new Vector3(scale.x - 0.00625f, scale.y - 0.00625f, scale.z - 0.00625f);
            }
            else
            {
                growing = true;
            }
        }
	}

    void SetGrowAndShrink(bool gas)
    {
        growAndShrink = gas;
    }

    public void OnPointerEnter()
    {
        SetGrowAndShrink(true);
    }

    public void OnPointerExit()
    {
        SetGrowAndShrink(false);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
