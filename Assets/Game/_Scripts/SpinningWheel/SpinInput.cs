using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpinInput : MonoBehaviour
{
    //-------------------------------------------\\
    //input bomba de ar
    [SerializeField] Pump_SensorDistance pumpScript;
    private bool movementStarted;

    //-------------------------------------------\\
    //verificar se jogador deu o ok
    private bool p1Ready = false;
    private bool p2Ready = false;

    //-------------------------------------------\\
    [SerializeField] Roulette rouletteScript;
    [SerializeField] SpinButtonUI spinUI;
    private bool canSpin = true;
    public Animator fade;
    Coroutine coroutine;

    //-------------------------------------------\\

    void Update()
    {
        if (!p1Ready) CheckPumpInput();

        if (canSpin && p1Ready && p2Ready && ScoreManager.instance.minigameCount < 4)
        {
            rouletteScript.Spin();
            canSpin = false;
            StartCoroutine(spinUI.FadeOutText());
        }
        else if (ScoreManager.instance.minigameCount >= 4)
        {
            spinUI.StopAnim();
        }

        if (p1Ready || p2Ready)
        {
            spinUI.StopAnim();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            coroutine ??= StartCoroutine(LoadMenu());
        }
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !p2Ready)
        {
            if (ScoreManager.instance.minigameCount < 4)
            {
                StartCoroutine(spinUI.PressedAnim(2));

                spinUI.AddFilling();
                p2Ready = true;

                if (!p1Ready) spinUI.FillClockwise(false);

                spinUI.FillBar();
            }
            else
            {
                coroutine ??= StartCoroutine(Load());
            }
        }
    }
    //
    void CheckPumpInput()
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
                if (ScoreManager.instance.minigameCount < 4)
                {
                    StartCoroutine(spinUI.PressedAnim(1));

                    spinUI.AddFilling();
                    p1Ready = true;

                    spinUI.FillBar();
                }
                else
                {
                    coroutine ??= StartCoroutine(Load());
                }
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ScoreManager.instance.minigameCount < 4)
            {
                StartCoroutine(spinUI.PressedAnim(1));

                spinUI.AddFilling();
                p1Ready = true;

                spinUI.FillBar();
            }
            else
            {
                coroutine ??= StartCoroutine(Load());
            }
        }
    }
    public IEnumerator Load()
    {
        yield return new WaitForSeconds(1f);
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(0);
    }
    IEnumerator LoadMenu()
    {
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(0);
    }
}