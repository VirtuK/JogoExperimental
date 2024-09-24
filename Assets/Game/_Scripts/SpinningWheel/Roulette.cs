using UnityEngine;
//---------------------------------------------\\
[System.Serializable]
public struct MinMax
{
    public float min;
    public float max;
}
//---------------------------------------------\\

public class Roulette : MonoBehaviour
{
    [Header("Velocidade")]
    [SerializeField][Range(0f, 20f)] float maxSpeed;
    [SerializeField] private MinMax accelerationDuration, decelerationDuration;

    //---------------------------------------------\\
    //"lerp"
    float speed = 0f;
    private float accelerationD_Current;
    private float decelerationD_Current;
    float currentTime;

    //---------------------------------------------\\
    private bool spinning = false;
    private float rotation = 0f;
    //
    [SerializeField] LoadMinigame goToMinigame;
    [SerializeField] LerpScaleAnim[] icons;

    //---------------------------------------------\\

    void Update()
    {
        if (spinning)
        {
            SetRotation();
        }
    }
    //
    void SetRotation()
    {
        if (currentTime < accelerationD_Current)
        {
            float t = currentTime / accelerationD_Current;
            speed = Mathf.SmoothStep(0f, maxSpeed, t);
        }
        else if (currentTime < accelerationD_Current + decelerationD_Current)
        {
            float decelTime = currentTime - accelerationD_Current;
            speed = Mathf.SmoothStep(maxSpeed, 0f, decelTime / decelerationD_Current);
        }
        else
        {
            speed = 0;
            spinning = false;

            StartCoroutine(goToMinigame.Timer(GetIndex()));
        }

        rotation += 100 * Time.deltaTime * speed;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotation);

        currentTime += Time.deltaTime;
    }
    //
    public int GetIndex()
    {
        float rotationZ = transform.localEulerAngles.z % 360;
        float slice = rotationZ / 60f;

        int minigameIndex = Mathf.RoundToInt(slice) % 6;

        icons[minigameIndex].PlayScaleAnim();

        return minigameIndex % 3;
    }
    //
    public void Spin()
    {
        spinning = true;
        accelerationD_Current = Mathf.Round(Random.Range(accelerationDuration.min, accelerationDuration.max));
        decelerationD_Current = Mathf.Round(Random.Range(decelerationDuration.min, decelerationDuration.max));
        currentTime = 0f;
    }
}