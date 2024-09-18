using System.Collections;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    //---------------------------------------------\\
    [SerializeField] float maxSpeed;
    private float speed;
    private Rigidbody2D rb;

    //---------------------------------------------\\
    private float startRotation;
    private float targetRotation;
    private float rotation;

    //---------------------------------------------\\
    float curveTime;
    private float currentTime;
    private bool curve = false;

    //---------------------------------------------\\
    [SerializeField] Pump_SensorDistance pumpScript;

    private bool movementStarted = false;

    //---------------------------------------------\\
    [SerializeField] float breakPower;
    [SerializeField] float cooldown;
    private bool stopped = false;
    private bool canBreak = true;
    private Coroutine cooldownCoroutine;

    //---------------------------------------------\\
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
    }
    //
    void FixedUpdate()
    {
        if (!stopped) rb.velocity = transform.up * -speed; //andanando sempre em frente
    }
    //
    void Update()
    {
        if (curve)
        {
            FollowCurve();
        }

        if (canBreak) CheckPumpInput();
    }

    //---------------------------------------------\\
    void CheckPumpInput()
    {

        if (!movementStarted)
        {

            if (pumpScript.GetDistance() >= 30f) movementStarted = true;
        }
        else
        {
            if (pumpScript.GetDistance() <= 10f)
            {

                SlowDown();
                movementStarted = false;
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SlowDown();
        }
    }
    //
    void SlowDown()
    {
        speed *= breakPower;

        if (speed <= 0.05f)
        {
            rb.velocity = Vector2.zero;
            stopped = true;
            canBreak = false;
        }
        cooldownCoroutine ??= StartCoroutine(Cooldown());
    }
    //
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        speed = maxSpeed;
        stopped = false;
        canBreak = true;
        cooldownCoroutine = null;
    }

    //---------------------------------------------\\
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RailwayCurve"))
        {
            canBreak = false;

            curve = true;
            currentTime = 0f;
            curveTime = 2.1f / speed;

            startRotation = transform.localEulerAngles.z;
            targetRotation = SetDirection(other.transform);

            other.enabled = false;
        }
    }
    //
    float SetDirection(Transform curve)
    {

        float direction = 0f;

        if (curve.localScale.x > 0) //curva para direita
        {
            direction = startRotation - 90f;
        }

        else if (curve.localScale.x < 0) //curva para esquerda
        {
            direction = startRotation + 90f;
        }

        return direction;
    }
    //
    void FollowCurve()
    {
        if (currentTime < curveTime)
        {
            float t = currentTime / curveTime;
            rotation = Mathf.Lerp(startRotation, targetRotation, t);
        }
        else
        {
            curve = false;
            rotation = targetRotation;
            canBreak = true;
        }
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotation);
        currentTime += Time.deltaTime;
    }
}