using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayController : MonoBehaviour
{
    public GameObject player;
    public Sprite healthSprite;

    private List<GameObject> hearts = new List<GameObject>();
    private bool rendered;

    public void Start()
    {
        rendered = false;
    }

    public void Update()
    {
        int health = player.GetComponent<PlayerController>().GetHealth();
        int i = 0;

        if (!rendered)
        {
            for (i = 0; i < health; i++)
            {
                GameObject newGO = new GameObject($"health {i}");
                newGO.transform.SetParent(this.transform);

                Image img = newGO.AddComponent<Image>();
                img.sprite = healthSprite;
                RectTransform rt = newGO.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(2, 2);
                rt.localPosition = new Vector3(-325 + 50 * i, 295, 0);
                rt.pivot = new Vector2(1, 1);
            }

            rendered = true;
        }
    }
}
