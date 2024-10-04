using UnityEngine;

public class LoadMinigame : MonoBehaviour
{
    public Animator fade;
    public float timer;

    //--------
    bool counting = false;
    float currentTime = 0;
    bool go = false;
    //---------------------------------\\

    void Update()
    {
        if (counting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0f && go)
            {
                fade.SetTrigger("fadeOut");
            }
        }
    }
    //
    public void GoToMinigame(int index)
    {
        currentTime = timer;

        switch (index)
        {
            case 0:
                LoadScene.sceneToLoad = "Balloon";
                break;
            case 1:
                LoadScene.sceneToLoad = "Minecart";
                break;
            case 2:
                LoadScene.sceneToLoad = "Minefield";
                break;
        }

        counting = true;
        go = true;
    }
    public void DontGoToMinigame()
    {
        go = false;
        counting = false;
    }
}