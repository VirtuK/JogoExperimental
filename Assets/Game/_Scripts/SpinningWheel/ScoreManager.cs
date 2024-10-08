using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager instance;
    public int score_Player1;
    public int score_Player2;
    public int minigameCount = -1;
    public int lastMinigameIndex = 10;
    public List<int> player1score;
    public List<int> player2score;
    public int p1 = 0, p2 = 0;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
