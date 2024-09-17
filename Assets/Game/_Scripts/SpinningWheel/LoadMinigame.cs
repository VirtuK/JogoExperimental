using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class LoadMinigame : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] GameObject panel;
    [Header("Icon")]
    [SerializeField] Sprite[] minigameIcons;
    [SerializeField] Image icon;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Roleta")]
    [SerializeField] Roulette rouletteScript;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (!panel.activeSelf && rouletteScript.Stopped())
        {
            icon.sprite = minigameIcons[rouletteScript.GetIndex()];
            panel.SetActive(true);
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        float currentTime = 3f;

        while (currentTime > 0f)
        {
            timerText.text = currentTime.ToString() + "...";
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        switch (rouletteScript.GetIndex())
        {
            case 0:
                SceneManager.LoadScene("Balloon");
                break;
            case 1:
                print("Bomba");
                break;
            case 2:
                SceneManager.LoadScene("Minecart");
                break;
        }
    }
}