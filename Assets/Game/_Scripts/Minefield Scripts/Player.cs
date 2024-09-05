using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid grid;
    int actualPositionX;
    int actualPositionY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject newPos = grid.gridPosition[actualPositionX][actualPositionY + 1];
            if (newPos.name != "Wall")
            {
                this.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y, -0.3f);
                checkPosition();
                if (newPos.name == "Finish Point")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
           
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject newPos = grid.gridPosition[actualPositionX][actualPositionY - 1];
            if (newPos.name != "Wall")
            {
                this.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y, -0.3f);
                checkPosition();
                if (newPos.name == "Finish Point")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
           
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject newPos = grid.gridPosition[actualPositionX + 1][actualPositionY];
            if (newPos.name != "Wall")
            {
                
                this.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y, -0.3f);
                checkPosition();
                if (newPos.name == "Finish Point")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject newPos = grid.gridPosition[actualPositionX - 1][actualPositionY];
            if (newPos.name != "Wall")
            {
                
                this.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y, -0.3f);
                checkPosition();
                if (newPos.name == "Finish Point")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            
            
        }
    }

    public void checkPosition()
    {
        for (int i = 0; i < grid.gridPosition.Count; i++)
        {
            for (int j = 0; j < grid.gridPosition[i].Length; j++)
            {
                if (grid.gridPosition[i][j].transform.position.x == this.transform.position.x && grid.gridPosition[i][j].transform.position.y == this.transform.position.y)
                {
                    actualPositionX = i;
                    actualPositionY = j;

                }

            }
        }
        
    }
}
