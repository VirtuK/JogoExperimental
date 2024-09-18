using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid grid;
    public int actualPositionX;
    public int actualPositionY;
    public int positionIndex = 0;
    public bool canMove = true;

    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Pump_SensorDistance sensor;
    bool movementStarted;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckPumpInput();
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
        if (Input.GetKeyDown(KeyCode.P) && canMove)
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
                    checkAnimation();
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

    void checkAnimation()
    {
        if (actualPositionX - 1 > -1 && actualPositionX + 1 < 17 && actualPositionY - 1 > -1 && actualPositionY + 1 < 17)
        {
            if (grid.walkPosition[positionIndex + 1].x > grid.walkPosition[positionIndex].x && grid.walkPosition[positionIndex + 1].y == grid.walkPosition[positionIndex].y)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("right", true);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("left", false);
            }
            if (grid.walkPosition[positionIndex + 1].x < grid.walkPosition[positionIndex].x && grid.walkPosition[positionIndex + 1].y == grid.walkPosition[positionIndex].y)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", false);
                animator.SetBool("left", true);
            }
            if (grid.walkPosition[positionIndex + 1].x == grid.walkPosition[positionIndex].x && grid.walkPosition[positionIndex + 1].y > grid.walkPosition[positionIndex].y)
            {
                animator.SetBool("right", false);
                animator.SetBool("up", true);
                animator.SetBool("down", false);
                animator.SetBool("left", false);
            }
            if (grid.walkPosition[positionIndex + 1].x == grid.walkPosition[positionIndex].x && grid.walkPosition[positionIndex + 1].y < grid.walkPosition[positionIndex].y)
            {
                spriteRenderer.flipX = true;
                animator.SetBool("right", false);
                animator.SetBool("up", false);
                animator.SetBool("down", true);
                animator.SetBool("left", false);
            }
        }
    }
}
