using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public Grid grid;
    public int lever1;
    public int lever2;
    public List<int[]> bombCodes = new List<int[]>();
    public Player player;

    public Image lever1Object;
    public Image lever2Object;
    float timer_time = 1;
    bool timer = false;

    public List<Image> codesList = new List<Image>();

    public Vector2 input;

    int index;
    int codeIndex = 0;
    // Start is called before the first frame update

    public TextMeshProUGUI result;
    public Animator fade;


    public TutorialMinefield tutorial;
    public Image logo;
    Coroutine sceneCoroutine;

    public void OnLeftStick(InputAction.CallbackContext context)
    {
        if (!tutorial.tutorial) input = new(input.x, Mathf.RoundToInt(context.ReadValue<float>()));
    }
    //
    public void OnRightStick(InputAction.CallbackContext context)
    {
        if (!tutorial.tutorial) input = new(Mathf.RoundToInt(context.ReadValue<float>()), input.y);
    }
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !tutorial.tutorial && !logo.enabled)
        {
            for (int i = 0; i < bombCodes.Count; i++)
            {
                if (lever1 == bombCodes[i][0] && lever2 == bombCodes[i][1])
                {
                    bombAnimation(i);
                    //bombCodes.Remove(bombCodes[i]);
                }
            }
        }
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        List<int> bombs = new List<int>();
        List<Vector2> codes = new List<Vector2>();
        bombs.AddRange(new List<int> { 0, 1, 2, 3 });
        codes.AddRange(new List<Vector2> { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });

        for (int i = 0; i < bombs.Count; i++)
        {
            List<int> bools = new List<int>();
            int r = Random.Range(0, codes.Count);
            if (codes[r].x == 1)
            {
                bools.Add(1);
            }
            else
            {
                bools.Add(0);
            }
            if (codes[r].y == 1)
            {
                bools.Add(1);
            }
            else
            {
                bools.Add(0);
            }
            bombCodes.Add(bools.ToArray());
            codes.Remove(codes[r]);
        }

        for (int i = -2; i <= 4; i++)
        {
            if (i % 2 == 0)
            {
                if (i < 0)
                {
                    codeIndex = 0;
                }
                else
                {
                    codeIndex++;
                }

                if (bombCodes[codeIndex][1] == 1)
                {
                    codesList[i + 3].color = Color.blue;
                }
                else
                {
                    codesList[i + 3].color = Color.red;
                }
                if (bombCodes[codeIndex][0] == 1)
                {
                    codesList[i + 2].color = Color.blue;
                }
                else
                {
                    codesList[i + 2].color = Color.red;
                }

            }


        }

    }

    // Update is called once per frame
    void Update()
    {

        if (input.y == 1)
        {
            lever1 = 1;
            lever1Object.color = Color.blue;
        }
        if (input.y == -1)
        {
            lever1 = 0;
            lever1Object.color = Color.red;
        }
        if (input.x == 1)
        {
            lever2 = 1;
            lever2Object.color = Color.blue;
        }
        if (input.x == -1)
        {
            lever2 = 0;
            lever2Object.color = Color.red;
        }
        if (timer)
        {
            timer_time -= Time.deltaTime;
        }

        if (timer_time <= 0)
        {
            DetonateBomb(index);
            timer = false;
            timer_time = 1;
        }
    }

    void bombAnimation(int bombIndex)
    {
        grid.bombList[bombIndex].GetComponent<Animator>().SetBool("Explode", true);
        grid.bombList[bombIndex].GetComponent<SpriteRenderer>().color = new Color(grid.bombList[bombIndex].GetComponent<SpriteRenderer>().color.r,
            grid.bombList[bombIndex].GetComponent<SpriteRenderer>().color.r, grid.bombList[bombIndex].GetComponent<SpriteRenderer>().color.b, 255);
        index = bombIndex;
        timer = true;
        player.canMove = false;
    }

    void DetonateBomb(int bombIndex)
    {
        GameObject newPos = grid.gridPosition[player.actualPositionX][player.actualPositionY];

        Transform bombPos = grid.bombPosition[bombIndex];

        if (bombPos.gameObject.name != "Path")
        {
            if (newPos.name == bombPos.gameObject.name)
            {
                result.text = $"player 2 Venceu!";
                ScoreManager.instance.score_Player1 = 1;
                ScoreManager.instance.score_Player2 = 2;
                sceneCoroutine ??= StartCoroutine(LoadRoulette(true));
                bombPos.gameObject.name = "Path";
            }
            else
            {
                player.canMove = true;
                bombPos.gameObject.name = "Path";
            }
            grid.bombList[bombIndex].SetActive(false);
        }
        else
        {
            player.canMove = true;
        }
        

    }

    IEnumerator LoadRoulette(bool wait)
    {
        if (wait) yield return new WaitForSeconds(2f);
        LoadScene.sceneToLoad = "SpinningWheel";
        fade.SetTrigger("fadeOut");
    }
}