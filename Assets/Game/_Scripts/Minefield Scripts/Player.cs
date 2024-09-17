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
    int positionIndex = 0;

    [SerializeField] private Pump_SensorDistance sensor;
    bool movementStarted;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void CheckPumpInput()
    {
        if (!movementStarted)
        {
            if(sensor.GetDistance() >= 30) movementStarted = true;
        }
        else
        {
            if(sensor.GetDistance() <= 10)
            {
                movePlayer();
                movementStarted = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            movePlayer();
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

    public void movePlayer()
    {
        positionIndex++;
        transform.position = grid.walkPosition[positionIndex];
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.3f);
        checkPosition();
        print("aaaaaaaa");
        GameObject newPos = grid.gridPosition[actualPositionX][actualPositionY];
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
