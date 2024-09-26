using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

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
    [SerializeField] float curvaRadius = 1f; // Raio da curva
    private float curvaLength; // Comprimento da curva baseado no raio
    private float curvaDistanceTraveled; // Distância percorrida na curva
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
    Coroutine coroutine;
    public Animator fade;
    public Image logo;

    //---------------------------------------------\\
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        timer.enabled = false;

        //comprimento da curva baseado no raio da circunferência
        curvaLength = Mathf.PI * curvaRadius / 2f; // 90 graus
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
        CheckPumpInput();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            coroutine ??= StartCoroutine(LoadMenu());
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
		    logo.enabled = false;

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
                logo.enabled = false;
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

        if (playerWin == 1)
        {
            ScoreManager.instance.score_Player1 = 2;
            ScoreManager.instance.score_Player2 = 1;
        }
        else if (playerWin == 2)
        {
            ScoreManager.instance.score_Player1 = 1;
            ScoreManager.instance.score_Player2 = 2;
        }

        result.text = $"player {playerWin} Venceu!";

        coroutine ??= StartCoroutine(LoadRoulette());
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

            startRotation = transform.localEulerAngles.z;
            targetRotation = SetDirection(other.transform);
            curvaDistanceTraveled = 0f;
            curve = true;

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
        //distância que o carrinho percorre na curva baseado na velocidade
        float distanceToTravel = speed * Time.deltaTime;
        curvaDistanceTraveled += distanceToTravel;

        //percentual percorrido da curva
        float curvaPercentComplete = curvaDistanceTraveled / curvaLength;

        //completou a curva
        if (curvaPercentComplete >= 1f)
        {
            curvaPercentComplete = 1f;
            curve = false;
        }

        //lerp da rotação com base na distância percorrida na curva
        rotation = Mathf.Lerp(startRotation, targetRotation, curvaPercentComplete);
        transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }
    //
    IEnumerator LoadRoulette()
    {
        yield return new WaitForSeconds(2f);
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("SpinningWheel");
    }
    IEnumerator LoadMenu()
    {
        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(0);
    }
}