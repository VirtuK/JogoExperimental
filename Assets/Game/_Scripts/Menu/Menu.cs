using UnityEngine;
using UnityEngine.InputSystem;
public class Menu : MonoBehaviour
{
    //-------------------------------------------\\
    //input bomba de ar
    [SerializeField] Pump_SensorDistance pumpScript;
    private bool movementStarted;
    public Animator fade;

    //-------------------------------------------\\
    void Start()
    {
        Reset();
        LoadScene.sceneToLoad = "SpinningWheel";
    }
    //
    void Update()
    {
        if (!movementStarted)
        {
            if (pumpScript.GetDistance() >= 20f) movementStarted = true;
        }

        else
        {
            if (pumpScript.GetDistance() <= 10f)
            {
                movementStarted = false;
                fade.SetTrigger("fadeOut");
            }
        }

        //teste
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fade.SetTrigger("fadeOut");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fade.SetTrigger("quit");
        }
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fade.SetTrigger("fadeOut");
        }
    }
    //
    void Reset()
    {
        ScoreManager.instance.minigameCount = -1;
        ScoreManager.instance.p1 = 0;
        ScoreManager.instance.p2 = 0;

        ScoreManager.instance.player1score.Clear();
        ScoreManager.instance.player2score.Clear();
    }
}