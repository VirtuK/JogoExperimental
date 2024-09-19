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

    void Start()
    {

        counting = true;
        currentTime = maxTime;

        SetCounterUIText();
    }
    //
    void Update()
    {

        if (counting)
        {
            currentTime -= Time.deltaTime;
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
            counting = false;
            return true;
        }
        return false;
    }
}