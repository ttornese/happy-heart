using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayController : MonoBehaviour
{
    public Sprite healthSprite;

    private List<GameObject> hearts = new List<GameObject>();
    private bool rendered;
    private GameObject player;

    public void Start()
    {
        rendered = false;
        player = GameObject.Find("/Player");
    }

    public void DisplayHealth()
    {
        int health = player.GetComponent<PlayerController>().GetHealth();
        int i;

        for (i = 0; i < hearts.Count; i++)
        {
            Destroy(hearts[i]);
        }

        for (i = 0; i < health; i++)
        {
          GameObject heart = new GameObject($"health {i}");
          heart.transform.SetParent(gameObject.transform);

          Image img = heart.AddComponent<Image>();
          img.sprite = healthSprite;
          RectTransform rt = heart.GetComponent<RectTransform>();
          rt.sizeDelta = new Vector2(2, 2);
          rt.localPosition = new Vector3(275 + 100 * i, 130, 0);
          rt.pivot = new Vector2(1, 1);
          hearts.Add(heart);
        }
    }
}
