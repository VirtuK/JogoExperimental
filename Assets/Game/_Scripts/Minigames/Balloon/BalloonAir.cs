using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (sensor.GetDistance() >= 30f) movementStarted = true;
        }
        else
        {
            if (sensor.GetDistance() <= 10f)
            {

                AddAir();
                movementStarted = false;
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
                }

            }

            else if (airInside >= airMax)
            {
                print("Player 1 ganhou");
                playing = false;
                airInside = airMax;
                timer.StopTimer();
                ScoreManager.instance.score_Player1 = 2;
                ScoreManager.instance.score_Player2 = 1;
                SceneManager.LoadScene("SpinningWheel");
            }

            else if (airInside < airMin && started)
            {
                print("Player 2 ganhou");
                playing = false;
                timer.StopTimer();
                ScoreManager.instance.score_Player1 = 1;
                ScoreManager.instance.score_Player2 = 2;
                SceneManager.LoadScene("SpinningWheel");
            }

            if (playing && timer.TimeUp())
            {
                print("Player 2 ganhou");
                playing = false;
                ScoreManager.instance.score_Player1 = 1;
                ScoreManager.instance.score_Player2 = 2;
                SceneManager.LoadScene("SpinningWheel");
            }
        }
    }
}
