using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialBalloon : MonoBehaviour
{
    [Header("Bomba de ar")]
    public Pump_SensorDistance sensor;
    private bool movementStarted = false;

    [Header("Controles")]
    public GameObject pump;
    public GameObject stick;
    public Animator stickAnim;
    public GameObject r2;
    public Animator r2Anim;

    [Header("Tiro")]
    public GameObject aim;
    public Animator aimAnim;
    public GameObject target;

    [Header("Animators")]
    public Animator arrow;
    public Animator balloon;
    public GameObject title;

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
                balloon.SetTrigger("2");
                stick.SetActive(true);
                aim.SetActive(true);
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            arrow.SetTrigger("jump");
            page++;

            pump.SetActive(false);
            balloon.SetTrigger("2");
            stick.SetActive(true);
            aim.SetActive(true);
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
                    balloon.SetTrigger("3");
                    aimAnim.SetTrigger("2");
                    stick.SetActive(false);
                    r2.SetActive(true);
                    target.SetActive(true);
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