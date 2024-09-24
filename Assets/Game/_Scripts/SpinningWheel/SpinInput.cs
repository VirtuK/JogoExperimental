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

    //-------------------------------------------\\

    void Update()
    {
        if (!p1Ready) CheckPumpInput();

        if (canSpin && p1Ready && p2Ready && ScoreManager.instance.minigameCount < 5)
        {
            rouletteScript.Spin();
            canSpin = false;
            StartCoroutine(spinUI.FadeOutText());
        }

        if (p1Ready || p2Ready)
        {
            spinUI.StopAnim();
        }
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !p2Ready)
        {
            StartCoroutine(spinUI.PressedAnim(2));

            spinUI.AddFilling();
            p2Ready = true;

            if (!p1Ready) spinUI.FillClockwise(false);

            spinUI.FillBar();
        }
    }
    //
    void CheckPumpInput()
    {
        if (!movementStarted)
        {
            if (pumpScript.GetDistance() >= 25f) movementStarted = true;
        }

        else
        {
            if (pumpScript.GetDistance() <= 10f)
            {
                StartCoroutine(spinUI.PressedAnim(1));

                spinUI.AddFilling();
                movementStarted = false;
                p1Ready = true;

                spinUI.FillBar();
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(spinUI.PressedAnim(1));

            spinUI.AddFilling();
            p1Ready = true;

            spinUI.FillBar();
        }
    }
}