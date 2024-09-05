using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            DetonateBombs();
            print("KABOOM");
        }
    }

    void DetonateBombs()
    {
        for(int i = 0; i < grid.bombList.Count; i++)
        {
            if (grid.bombList[i].transform.position.x == grid.player.transform.position.x && grid.bombList[i].transform.position.y == grid.player.transform.position.y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
