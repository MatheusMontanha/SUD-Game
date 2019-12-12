using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public FloatValue playerCurrentHealth;
    public FloatValue heartContainers;
    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.value; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float temHealth = playerCurrentHealth.value / 2;

        for (int i = 0; i < heartContainers.value; i++)
        {
            if (i <= temHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i >= temHealth)
            {
                hearts[i].sprite = emptyHeart;

            }
            else
            {
                hearts[i].sprite = halfHeart;

            }
        }


    }
}
