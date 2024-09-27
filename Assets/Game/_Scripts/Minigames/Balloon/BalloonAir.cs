using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BalloonAir : MonoBehaviour
{
    public bool playing = true;
    public bool started = false;

    [SerializeField] private MinigameTimer timer;
    [SerializeField] private Transform balloon;
    [Header("Air controller")]
    [SerializeField] private float airInside;
    [Range(0, 100)][SerializeField] private float airLoss;
    [Range(0, 100)][SerializeField] private float airMin;
    [Range(0, 100)][SerializeField] private float airMax;
    [SerializeField] private Pump_SensorDistance sensor;
    [Range(0, 100)][SerializeField] private float pumpAir;
    [Range(0, 100)][SerializeField] private float airToStart;
    private float totalAirLoss;


    [Header("Rope components")]
    [SerializeField] private LineRenderer lineRendererLeft;
    [SerializeField] private LineRenderer lineRendererRight;

    [SerializeField] private Transform AnchorTopLeft;
    [SerializeField] private Transform AnchorTopRight;
    [SerializeField] private Transform AnchorBottomLeft;
    [SerializeField] private Transform AnchorBottomRight;

    [Header("Rip generator")]
    private TargetGenerator targetScript;
    private bool movementStarted;
    //
    public TextMeshProUGUI result;
    Coroutine coroutine;
    public Animator fade;
    public Image logo;
    public Animator balloonAnim;
    public GameObject balloonbottom, seat;

    private void Start()
    {
        targetScript = GetComponent<TargetGenerator>();
        timer.enabled = false;
        airInside = airMin;

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
                AddAir();
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddAir();
        }
    }
    //

    private void AddAir()
    {
        airInside += pumpAir;

    }

    private void Update()
    {
        lineRendererLeft.SetPosition(0, AnchorBottomLeft.position);
        lineRendererRight.SetPosition(0, AnchorBottomRight.position);

        lineRendererLeft.SetPosition(1, AnchorTopLeft.position);
        lineRendererRight.SetPosition(1, AnchorTopRight.position);

        if (playing)
        {
            CheckPumpInput();
            totalAirLoss = targetScript.holes.Count * airLoss * Time.deltaTime;
            airInside -= totalAirLoss;

            if (airInside > airMin && airInside < airMax)
            {
                balloon.localScale = Vector3.Lerp(balloon.localScale, new Vector3(airInside, airInside, airInside), airLoss);

                if (airInside > airToStart && !started)
                {
                    started = true;
                    timer.enabled = true;
                    logo.enabled = false;
                }

            }
            else if (airInside >= airMax)
            {
                balloonAnim.GetComponent<SpriteRenderer>().color = Color.white;
                balloonAnim.SetBool("explode", true);
                lineRendererLeft.enabled = false;
                lineRendererRight.enabled = false;
                balloonbottom.SetActive(false);
                seat.SetActive(false);

                result.text = $"player 2 Venceu!";
                playing = false;
                airInside = airMax;
                timer.StopTimer();
                ScoreManager.instance.score_Player1 = 1;
                ScoreManager.instance.score_Player2 = 2;
                coroutine ??= StartCoroutine(LoadRoulette());
            }

            else if (airInside < airMin && started)
            {
                balloonAnim.GetComponent<SpriteRenderer>().color = Color.white;
                result.text = $"player 2 Venceu!";
                playing = false;
                timer.StopTimer();
                ScoreManager.instance.score_Player1 = 1;
                ScoreManager.instance.score_Player2 = 2;
                coroutine ??= StartCoroutine(LoadRoulette());
            }

            if (playing && timer.TimeUp())
            {
                balloonAnim.GetComponent<SpriteRenderer>().color = Color.white;

                if (airInside > airMin && airInside < airMax)
                {
                    result.text = $"player 1 Venceu!";
                    playing = false;
                    timer.StopTimer();
                    ScoreManager.instance.score_Player1 = 2;
                    ScoreManager.instance.score_Player2 = 1;
                    coroutine ??= StartCoroutine(LoadRoulette());
                }
                else
                {
                    result.text = $"player 2 Venceu!";
                    playing = false;
                    timer.StopTimer();
                    ScoreManager.instance.score_Player1 = 1;
                    ScoreManager.instance.score_Player2 = 2;
                    coroutine ??= StartCoroutine(LoadRoulette());
                }
            }
        }
        if ((airInside >= (airMax - 1) && airInside < airMax) || (airInside <= (airMin + 0.3) && airInside > airMin))
        {
            balloonAnim.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            balloonAnim.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            coroutine ??= StartCoroutine(LoadMenu());
        }
    }
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
