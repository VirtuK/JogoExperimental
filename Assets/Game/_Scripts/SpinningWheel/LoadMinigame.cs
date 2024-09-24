using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadMinigame : MonoBehaviour
{
    public IEnumerator Timer(int index)
    {
        float currentTime = 2f;

        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        switch (index)
        {
            case 0:
                SceneManager.LoadScene("Balloon");
                break;
            case 1:
                SceneManager.LoadScene("Minefield");
                break;
            case 2:
                SceneManager.LoadScene("Minecart");
                break;
        }
    }
}