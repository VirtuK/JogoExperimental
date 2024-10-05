using System.Collections;
using UnityEngine;
//---------------------------------------------\\
public class Roulette : MonoBehaviour
{
    private bool spinning = false;
    private int minigameIndex = 0;

    //---------------------------------------------\\
    private float targetRotation;
    private float currentRotation = 0f;
    //
    Coroutine coroutine;
    //
    [SerializeField] LoadMinigame loadMinigame;
    //
    [SerializeField] Animator[] icons;


    //---------------------------------------------\\

    void Update()
    {
        if (spinning)
        {
            coroutine ??= StartCoroutine(SetRotation());
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                loadMinigame.DontGoToMinigame();
                Spin();
            }
        }
    }
    //
    IEnumerator SetRotation()
    {
        float elapsedTime = 0f;
        float startRotation = currentRotation;

        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 5f;

            //t = t * t * (3f - 2f * t); //ease inout quad
            t = Mathf.Pow(t, 2) * (3f - 2f * t); //intensifica

            currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
            yield return null;

        }

        currentRotation = targetRotation;
        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotation);

        spinning = false;

        icons[minigameIndex].SetTrigger("pulse");
        loadMinigame.GoToMinigame(minigameIndex % 3);

        coroutine = null;
    }
    //
    public void Spin()
    {
        minigameIndex = Random.Range(0, 6);

        while (minigameIndex % 3 == ScoreManager.instance.lastMinigameIndex)
        {
            minigameIndex = Random.Range(0, 6);
        }

        ScoreManager.instance.lastMinigameIndex = minigameIndex % 3;

        targetRotation = 60 * minigameIndex;
        targetRotation += 360 * 7;
        targetRotation = -targetRotation;

        currentRotation = transform.localEulerAngles.z;

        spinning = true;
    }
}