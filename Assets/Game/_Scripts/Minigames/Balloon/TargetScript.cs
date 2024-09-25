using System.Collections;
using UnityEngine;

public class RipScript : MonoBehaviour
{
    //---------------------------------------------\\
    private BalloonAir balloonScript;
    private SpriteRenderer sprite;
    private Coroutine fadeCoroutine;
    private Coroutine animCoroutine;

    //---------------------------------------------\\
    //lerp
    private Vector3 minScale;
    private Vector3 maxScale;
    private Vector3 targetScale;
    private bool scaleUp = true;

    //---------------------------------------------\\
    private void Start()
    {
        balloonScript = FindObjectOfType<BalloonAir>();
        sprite = GetComponent<SpriteRenderer>();

        minScale = transform.localScale;
        maxScale = new(minScale.x + 0.05f, minScale.y + 0.05f, minScale.z + 0.05f);
    }
    //
    void Update()
    {
        if (animCoroutine == null)
        {
            targetScale = scaleUp ? maxScale : minScale;
            scaleUp = !scaleUp;

            animCoroutine ??= StartCoroutine(LerpScale(targetScale, 0.7f));
        }

        if (!balloonScript.playing && fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    //---------------------------------------------\\
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float colorAlpha = 1f;

        while (colorAlpha > 0f)
        {
            elapsedTime += 4 * Time.deltaTime;
            colorAlpha = Mathf.Lerp(1f, 0f, elapsedTime);

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, colorAlpha);

            yield return null;
        }
        Destroy(gameObject);
    }
    //
    IEnumerator LerpScale(Vector3 target, float duration)
    {

        float elapsedTime = 0;
        Vector3 startValue = transform.localScale;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startValue, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;
        animCoroutine = null;
    }
}