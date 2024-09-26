using System;
using UnityEngine;
using TMPro;

public class MinigameTimer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float maxTime = 120;
    private bool counting;
    private float currentTime;

    //UI
    [SerializeField] private string counterText;
    [SerializeField] private TextMeshProUGUI counterUI;
    //--------------------------------------------------\\

    void Awake()
    {
        counting = true;
        currentTime = maxTime;
        counterUI.color = Color.white;
        SetCounterUIText();
    }
    //
    void Update()
    {

        if (counting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 10)
            {
                counterUI.color = Color.red;
            }
        }
        SetCounterUIText();
    }
    //
    void SetCounterUIText()
    {

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        counterText = time.ToString(@"m\:ss");
        counterUI.text = counterText;
    }
    //
    public void StopTimer()
    {
        counting = false;
    }
    //
    public bool TimeUp()
    {

        if (currentTime <= 0)
        {
            counterUI.color = Color.white;
            counting = false;
            return true;
        }
        return false;
    }
}