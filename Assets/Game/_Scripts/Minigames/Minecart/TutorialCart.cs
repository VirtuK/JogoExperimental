using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialCart : MonoBehaviour
{
    [Header("Bomba de ar")]
    public Pump_SensorDistance sensor;
    private bool movementStarted = false;

    [Header("Controles")]
    public GameObject pump;
    public GameObject stickL;
    public GameObject stickR;
    public GameObject r2;
    public Animator r2Anim;


    [Header("Animators")]
    public Animator arrow;
    public GameObject title;

    [Header("Minigame OBj")]
    public GameObject rail1;
    public GameObject rail2;
    public GameObject rail3;
    public Animator rail3Anim;
    public GameObject cart1;
    public Animator cart1Anim;
    public GameObject cart2;
    public GameObject dynamite;

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
                rail1.SetActive(false);
                cart1.SetActive(false);

                stickL.SetActive(true);
                stickR.SetActive(true);
                cart2.SetActive(true);
                rail3.SetActive(true);
                dynamite.SetActive(true);

            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            arrow.SetTrigger("jump");
            page++;

            pump.SetActive(false);
            rail1.SetActive(false);
            cart1.SetActive(false);

            stickL.SetActive(true);
            stickR.SetActive(true);
            cart2.SetActive(true);
            rail3.SetActive(true);
            dynamite.SetActive(true);
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
                    cart2.SetActive(false);
                    rail3.SetActive(false);
                    dynamite.SetActive(false);

                    rail2.SetActive(true);
                    r2.SetActive(true);
                    cart1.SetActive(true);
                    cart1Anim.SetTrigger("2");
                    r2Anim.SetTrigger("2");
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
