using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMinefield : MonoBehaviour
{
    [Header("Bomba de ar")]
    public Pump_SensorDistance sensor;
    private bool movementStarted = false;

    [Header("Controles")]
    public GameObject pump;
    public GameObject stickL;
    public GameObject stickR;
    public Animator stickLAnim;
    public Animator stickRAnim;
    public GameObject r2;
    public Animator r2Anim;


    [Header("Animators")]
    public Animator arrow;
    public GameObject title;

    [Header("Minigame OBj")]
    public GameObject labr;
    public GameObject levers;
    public GameObject bombs;

    int page = 0;

    public bool tutorial = true;

    void Update()
    {
        if (tutorial)
        {
            if (page == 0) CheckPumpInput();
        }
    }

    void CheckPumpInput()
    {
        if (!movementStarted)
        {
            if (sensor.GetDistance() >= 20f) movementStarted = true;
        }
        else
        {
            if (sensor.GetDistance() <= 10f)
            {
                movementStarted = false;
                arrow.SetTrigger("jump");
                page++;

                pump.SetActive(false);
                labr.SetActive(false);

                stickL.SetActive(true);
                stickR.SetActive(true);
                levers.SetActive(true);

                stickLAnim.SetTrigger("2");
                stickRAnim.SetTrigger("2");
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            arrow.SetTrigger("jump");
            page++;

            pump.SetActive(false);
            labr.SetActive(false);

            stickL.SetActive(true);
            stickR.SetActive(true);
            levers.SetActive(true);
            stickLAnim.SetTrigger("2");
            stickRAnim.SetTrigger("2");
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && tutorial)
        {
            if (page > 0)
            {
                arrow.SetTrigger("jump");
                page++;

                if (page == 2)
                {
                    stickL.SetActive(false);
                    stickR.SetActive(false);
                    levers.SetActive(false);

                    r2.SetActive(true);
                    bombs.SetActive(true);

                    r2Anim.SetTrigger("3");
                }
                else if (page == 3)
                {
                    StartCoroutine(End());
                }
            }
        }
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(0.1f);
        tutorial = false;
        title.SetActive(true);
        gameObject.SetActive(false);
    }
}
