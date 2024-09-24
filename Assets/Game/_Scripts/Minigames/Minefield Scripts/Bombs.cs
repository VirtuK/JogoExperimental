using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    // Start is called before the first frame update
    float timer_time = 5;
    Color bombColor;
    public Grid grid;
    bool bombs;
    bool timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bombs)
        {
            if (!timer)
            {
                if (timer_time > 0)
                {
                    timer_time -= Time.deltaTime;
                }
                else
                {
                    timer = true;
                    bombs = true;
                }
            }
        }
        else
        {
            /*for (int i = 0; i < grid.bombList.Count; i++)
            {
                bombColor = grid.bombList[i].GetComponent<SpriteRenderer>().color;
                grid.bombList[i].GetComponent<SpriteRenderer>().color = new Color(bombColor.r, bombColor.g, bombColor.b, 0);
            }
            bombs = false;*/
        }
        
        
    }
}
