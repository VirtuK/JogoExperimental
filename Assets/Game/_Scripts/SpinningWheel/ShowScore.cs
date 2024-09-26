using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Image> p1Score = new List<Image>();
    [SerializeField] List<Image> p2Score = new List<Image>();
    [SerializeField] Sprite first;
    [SerializeField] Sprite second;
    [SerializeField] Image[] trophy;
    public GameObject victory;
    public GameObject text;

    void Start()
    {
        ScoreManager.instance.minigameCount++;
        if (ScoreManager.instance.minigameCount >= 0) checkScore();


        if (ScoreManager.instance.minigameCount >= 4)
        {
            enabled = true;
            victory.SetActive(true);
            text.SetActive(true);

            if (ScoreManager.instance.p1 > ScoreManager.instance.p2) trophy[0].enabled = true;
            else trophy[1].enabled = true;
        }
    }

    void checkScore()
    {
        if (ScoreManager.instance.score_Player1 == 2)
        {
            p1Score[ScoreManager.instance.minigameCount].sprite = first;
            ScoreManager.instance.player1score.Add(2);
            ScoreManager.instance.p1++;
        }
        else if (ScoreManager.instance.score_Player1 == 1)
        {
            p1Score[ScoreManager.instance.minigameCount].sprite = second;
            ScoreManager.instance.player1score.Add(1);
        }
        if (ScoreManager.instance.score_Player2 == 2)
        {
            p2Score[ScoreManager.instance.minigameCount].sprite = first;
            ScoreManager.instance.player2score.Add(2);
            ScoreManager.instance.p2++;
        }
        else if (ScoreManager.instance.score_Player2 == 1)
        {
            p2Score[ScoreManager.instance.minigameCount].sprite = second;
            ScoreManager.instance.player2score.Add(1);
        }
        ScoreManager.instance.score_Player1 = 0;
        ScoreManager.instance.score_Player2 = 0;

        checkPreviousScores();
    }

    void checkPreviousScores()
    {
        if (ScoreManager.instance.player1score.Count > 0)
        {
            for (int i = 0; i < ScoreManager.instance.player1score.Count; i++)
            {
                if (ScoreManager.instance.player1score[i] == 2)
                {
                    p1Score[i].sprite = first;
                }
                else if (ScoreManager.instance.player1score[i] == 1)
                {
                    p1Score[i].sprite = second;
                }
            }
            for (int i = 0; i < ScoreManager.instance.player2score.Count; i++)
            {
                if (ScoreManager.instance.player2score[i] == 2)
                {
                    p2Score[i].sprite = first;
                }
                else if (ScoreManager.instance.player2score[i] == 1)
                {
                    p2Score[i].sprite = second;
                }
            }
        }
    }
}