using UnityEngine;
using UnityEngine.InputSystem;
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

    //-------------------------------------------\\

    void Update()
    {
        if (!p1Ready) CheckPumpInput();

        if (canSpin && p1Ready && p2Ready && ScoreManager.instance.minigameCount < 4)
        {
            rouletteScript.Spin();
            canSpin = false;
        }

        /*else if (ScoreManager.instance.minigameCount >= 4)
        {
        }

        if (p1Ready || p2Ready)
        {
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !p2Ready)
        {
            if (ScoreManager.instance.minigameCount < 4)
            {

                spinUI.AddFilling();
                p2Ready = true;

                if (!p1Ready) spinUI.FillClockwise(false);

                spinUI.FillBar();
            }
            else
            {
                MainMenu();
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
                    spinUI.AddFilling();
                    p1Ready = true;

                    spinUI.FillBar();
                }
                else
                {
                    MainMenu();
                }
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ScoreManager.instance.minigameCount < 4)
            {
                spinUI.AddFilling();
                p1Ready = true;

                spinUI.FillBar();
            }
            else
            {
                MainMenu();
            }
        }
    }
    void MainMenu()
    {
        LoadScene.sceneToLoad = "Menu";
        fade.SetTrigger("fadeOut");
    }
}