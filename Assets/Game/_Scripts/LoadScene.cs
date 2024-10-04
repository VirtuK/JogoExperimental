using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    public static string sceneToLoad = "Menu";

    public void Load()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    //    
    public void Quit()
    {
        if (!Application.isEditor) Application.Quit();
    }
}