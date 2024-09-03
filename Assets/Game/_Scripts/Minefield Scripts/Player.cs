using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            checkPosition();
            this.transform.position = new Vector3(grid.gridPosition[actualPositionX][actualPositionY + 1].x, grid.gridPosition[actualPositionX][actualPositionY + 1].y, -0.3f);
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            checkPosition();
            this.transform.position = new Vector3(grid.gridPosition[actualPositionX][actualPositionY - 1].x, grid.gridPosition[actualPositionX][actualPositionY - 1].y, -0.3f);
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            checkPosition();
            this.transform.position = new Vector3(grid.gridPosition[actualPositionX + 1][actualPositionY].x, grid.gridPosition[actualPositionX + 1][actualPositionY].y, -0.3f);
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            checkPosition();
            this.transform.position = new Vector3(grid.gridPosition[actualPositionX - 1][actualPositionY].x, grid.gridPosition[actualPositionX - 1][actualPositionY].y, -0.3f);
            
        }
    }

    public void checkPosition()
    {
        for (int i = 0; i < grid.gridPosition.Count; i++)
        {
            for (int j = 0; j < grid.gridPosition[i].Length; j++)
            {
                if (grid.gridPosition[i][j].x == this.transform.position.x && grid.gridPosition[i][j].y == this.transform.position.y)
                {
                    actualPositionX = i;
                    actualPositionY = j;

                }

            }
        }
        
    }
}
