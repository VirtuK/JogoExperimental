using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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

    public TextMeshProUGUI result;
    public Animator fade;
    public Image logo;

    public TutorialMinefield tutorial;
    Coroutine sceneCoroutine;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckPumpInput();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneCoroutine ??= StartCoroutine(LoadRoulette(false));
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void CheckPumpInput()
    {
        if (!movementStarted)
        {
            if (sensor.GetDistance() >= 20) movementStarted = true;
        }
        else
        {
            if (sensor.GetDistance() <= 10 && canMove && !tutorial.tutorial)
            {
                movementStarted = false;
                if (!logo.enabled) movePlayer();
                logo.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && canMove && !tutorial.tutorial)
        {
            if (!logo.enabled) movePlayer();
            else logo.enabled = false;
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
                result.text = $"player 1 Venceu!";
                ScoreManager.instance.score_Player1 = 2;
                ScoreManager.instance.score_Player2 = 1;
                sceneCoroutine ??= StartCoroutine(LoadRoulette(true));
            }
        }
    }

    void checkAnimation()
    {
        if (actualPositionX - 1 > -1 && actualPositionX + 1 < 9 && actualPositionY - 1 > -1 && actualPositionY + 1 < 9)
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
    //
    IEnumerator LoadRoulette(bool wait)
    {
        if (wait) yield return new WaitForSeconds(2f);
        LoadScene.sceneToLoad = "SpinningWheel";
        fade.SetTrigger("fadeOut");
    }
    //
    void Reset()
    {
        LoadScene.sceneToLoad = "Minefield";
        fade.SetTrigger("fadeOut");
    }
}