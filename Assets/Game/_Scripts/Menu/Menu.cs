using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEditor;

public class Menu : MonoBehaviour
{
    //-------------------------------------------\\
    //input bomba de ar
    [SerializeField] Pump_SensorDistance pumpScript;
    private bool movementStarted;
    public Animator fade;
    Coroutine coroutine;

    void Start()
    {
        ScoreManager.instance.minigameCount = -2;
        ScoreManager.instance.p1 = 0;
        ScoreManager.instance.p2 = 0;

        ScoreManager.instance.player1score.Clear();
        ScoreManager.instance.player2score.Clear();
    }
    void Update()
    {
        if (!movementStarted)
        {
            if (pumpScript.GetDistance() >= 20f) movementStarted = true;
        }

        else
        {
            if (pumpScript.GetDistance() <= 10f)
            {
                movementStarted = false;
                coroutine ??= StartCoroutine(Load());
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            coroutine ??= StartCoroutine(Load());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            coroutine ??= StartCoroutine(Quit());
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            coroutine ??= StartCoroutine(Load());
        }
    }
    IEnumerator Load()
    {
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(1);
    }
    IEnumerator Quit()
    {
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        if (!Application.isEditor) Application.Quit();
    }
}