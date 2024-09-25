using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadMinigame : MonoBehaviour
{
    public Animator fade;

    public IEnumerator Timer(int index)
    {
        float currentTime = 2f;

        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        fade.SetBool("fade", true);
        yield return new WaitForSeconds(0.4f);

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