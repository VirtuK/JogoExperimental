using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class SpinButton : MonoBehaviour
{
    //-------------------------------------------\\
    //barra UI do botão de girar
    [SerializeField] private Image bar;
    private float targetFillAmount = 0f;
    [SerializeField] private float animDuration = 0.2f;

    //-------------------------------------------\\
    //input bomba de ar
    [SerializeField] Pump_SensorDistance pumpScript;
    private bool movementStarted;

    //-------------------------------------------\\
    //verificar se jogador mandou girar
    private bool p1Ready = false;
    private bool p2Ready = false;

    //-------------------------------------------\\
    [SerializeField] Roulette rouletteScript;
    private bool canSpin = true;

    //-------------------------------------------\\

    void Update()
    {
        if (!p1Ready) CheckPumpInput();

        if (canSpin && p1Ready && p2Ready)
        {
            rouletteScript.Spin();
            canSpin = false;
        }
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && !p2Ready)
        {
            targetFillAmount += 0.5f;
            p2Ready = true;

            if (!p1Ready) bar.fillClockwise = false;

            StartCoroutine(LerpBar(targetFillAmount, animDuration));
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
                targetFillAmount += 0.5f;
                movementStarted = false;
                p1Ready = true;

                StartCoroutine(LerpBar(targetFillAmount, animDuration));
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            targetFillAmount += 0.5f;
            p1Ready = true;

            StartCoroutine(LerpBar(targetFillAmount, animDuration));
        }
    }
    //
    IEnumerator LerpBar(float targetValue, float duration)
    {
        float elapsedTime = 0f;
        float startValue = bar.fillAmount;

        while (elapsedTime < duration)
        {
            bar.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bar.fillAmount = targetValue;
    }
}