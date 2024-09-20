using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CartMovement : MonoBehaviour
{
    //---------------------------------------------\\
    [Header("Velocidade")]
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;
    private float speed;
    private Rigidbody2D rb;

    //---------------------------------------------\\
    private float startRotation;
    private float targetRotation;
    private float rotation;

    //---------------------------------------------\\
    [Header("Curva")]
    [SerializeField] float curveTime;
    private float curveCurrentTime;
    public bool curve = false;

    //---------------------------------------------\\
    [Header("Freio")]
    [SerializeField] float breakPower;
    private bool canBreak = true;
    private bool stopped = true;

    //---------------------------------------------\\
    [Header("Cooldown")]
    [SerializeField] float cooldownTime;
    private float coolElapsedTime;
    private bool cooldown = false;

    //---------------------------------------------\\
    [Header("Input")]
    [SerializeField] Pump_SensorDistance pumpScript;

    private bool movementStarted = false;

    [SerializeField] MinigameTimer timer;

    public int playerWin = 0;
    private bool playing = false;
    public TextMeshProUGUI result;

    //---------------------------------------------\\
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        timer.enabled = false;
    }
    //
    void FixedUpdate()
    {
        if (!stopped) rb.velocity = transform.up * -speed; //andanando sempre em frente
    }
    //
    void Update()
    {
        CheckPumpInput();

        if (curve && !stopped)
        {
            FollowCurve();
        }

        if (cooldown) Cooldown();

        if (timer.TimeUp())
        {
            playerWin = 2;
            Endgame();
        }

        if (!playing && playerWin > 0)
        {
            result.text = $"Player {playerWin} Venceu!";
        }
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
                if (!playing && playerWin == 0)
                {
                    playing = true;
                    stopped = false;
                    timer.enabled = true;

                }
                else if (canBreak)
                {
                    SlowDown();
                    movementStarted = false;
                }
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!playing && playerWin == 0)
            {
                timer.enabled = true;
                playing = true;
                stopped = false;
            }
            else if (canBreak)
            {
                SlowDown();
            }
        }
    }
    //
    void SlowDown()
    {
        speed *= breakPower;

        if (curve)
        {
            curveCurrentTime = 0f;
            startRotation = transform.localEulerAngles.z;
        }

        if (speed <= minSpeed)
        {
            rb.velocity = Vector2.zero;
            stopped = true;
            canBreak = false;

            coolElapsedTime = 0f;
        }
        if (!cooldown)
        {
            coolElapsedTime = 0f;
            cooldown = true;
        }
    }
    //
    void Cooldown()
    {
        if (coolElapsedTime >= cooldownTime)
        {
            speed = maxSpeed;
            stopped = false;
            canBreak = true;
            cooldown = false;

            if (curve)
            {
                curveCurrentTime = 0f;
                startRotation = transform.localEulerAngles.z;
            }
        }

        coolElapsedTime += Time.deltaTime;
    }
    //
    void Endgame()
    {
        playing = false;
        stopped = true;
        canBreak = false;
        rb.velocity = Vector2.zero;
        timer.StopTimer();

        if(playerWin == 1)
        {
            ScoreManager.instance.score_Player1 = 2;
            ScoreManager.instance.score_Player2 = 1;
            SceneManager.LoadScene("SpinningWheel");
        }
        else if(playerWin == 2)
        {
            ScoreManager.instance.score_Player1 = 1;
            ScoreManager.instance.score_Player2 = 2;
            SceneManager.LoadScene("SpinningWheel");
        }
    }
    //---------------------------------------------\\
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RailwayCurve"))
        {

            if (curve)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, targetRotation);
            }

            curve = true;
            curveCurrentTime = 0f;

            startRotation = transform.localEulerAngles.z;
            targetRotation = SetDirection(other.transform);

            other.enabled = false;
        }
        if (other.CompareTag("Dynamite"))
        {
            other.GetComponent<Animator>().SetBool("explode", true);
            playerWin = 2;
            Endgame();
        }
        if (other.CompareTag("Objective"))
        {
            playerWin = 1;
            Endgame();
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
        float duration = curveTime / speed;

        if (curveCurrentTime < duration)
        {
            float t = curveCurrentTime / duration;
            rotation = Mathf.Lerp(startRotation, targetRotation, t);
        }
        else
        {
            rotation = targetRotation;
            curve = false;
        }
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotation);
        curveCurrentTime += Time.deltaTime;
    }
}