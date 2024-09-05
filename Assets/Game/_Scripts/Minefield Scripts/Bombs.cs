using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    // Start is called before the first frame update
    float timer_time = 3;
    Color bombColor;
    public Grid grid;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer_time > 0)
        {
            timer_time -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < grid.bombList.Count; i++)
            {
                bombColor = grid.bombList[i].GetComponent<SpriteRenderer>().color;
                grid.bombList[i].GetComponent<SpriteRenderer>().color = new Color(bombColor.r, bombColor.g, bombColor.b, 0);
            }
        }
        
    }
}
