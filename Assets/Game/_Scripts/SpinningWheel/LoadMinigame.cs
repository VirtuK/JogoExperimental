using UnityEngine;

public class LoadMinigame : MonoBehaviour
{
    public Animator fade;
    public float timer;

    //--------
    bool counting = false;
    float currentTime = 0;
    //---------------------------------\\

    void Update()
    {
        if (counting)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0f)
            {
                fade.SetTrigger("fadeOut");
            }
        }
    }
    //
    public void GoToMinigame(int index)
    {
        currentTime = timer;
        counting = true;

        switch (index)
        {
            case 0:
                LoadScene.sceneToLoad = "Balloon";
                break;
            case 1:
                LoadScene.sceneToLoad = "Minefield";
                break;
            case 2:
                LoadScene.sceneToLoad = "Minecart";
                break;
        }
    }
}