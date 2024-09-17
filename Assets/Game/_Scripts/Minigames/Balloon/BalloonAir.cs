using Unity.VisualScripting;
using UnityEngine;

public class BalloonAir : MonoBehaviour
{
    private bool playing = true;
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

    [SerializeField] private Transform AnchorLeft;
    [SerializeField] private Transform AnchorRight;

    [Header("Rip generator")]
    private RipGenerator ripScript;
    private bool movementStarted;

    private void Start()
    {
        ripScript = GetComponent<RipGenerator>();
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
        lineRendererLeft.SetPosition(1, AnchorLeft.position);
        lineRendererRight.SetPosition(1, AnchorRight.position);

        if (playing)
        {
            CheckPumpInput();
            totalAirLoss = ripScript.holes.Count * airLoss * Time.deltaTime;
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
            }

            else if (airInside < airMin && started)
            {
                print("Player 2 ganhou");
                playing = false;
            }
        }
    }
}
