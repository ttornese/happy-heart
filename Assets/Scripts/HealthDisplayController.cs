using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayController : MonoBehaviour
{
    public Sprite healthSprite;

    private List<GameObject> hearts = new List<GameObject>();

    public void DisplayHealth(int health)
    {
        int i = 0;

        while (hearts.Count > 0)
        {
            Destroy(hearts[i]);
            hearts.RemoveAt(i);
        };

        for (i = 0; i < health; i++)
        {
            GameObject newGO = new GameObject($"health {i}");
            newGO.transform.SetParent(this.transform);

            Image img = newGO.AddComponent<Image>();
            img.sprite = healthSprite;
            RectTransform rt = newGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(2, 2);
            rt.localPosition = new Vector3(-10 + 1 * i, 0, 0);

            hearts.Add(newGO);
        }
    }
}
