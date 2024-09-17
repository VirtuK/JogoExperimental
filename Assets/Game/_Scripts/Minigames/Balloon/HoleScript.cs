using System.Collections;
using UnityEngine;

public class HoleScript : MonoBehaviour
{

    [SerializeField][Range(0, 100)] private float speed;
    private SpriteRenderer sprite;
    private TargetGenerator targetScript;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        targetScript = FindObjectOfType<TargetGenerator>();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float colorAlpha = 1f;

        yield return new WaitForSeconds(1f);

        while (colorAlpha > 0f)
        {
            elapsedTime += speed * Time.deltaTime;
            colorAlpha = Mathf.Lerp(1f, 0f, elapsedTime);

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, colorAlpha);

            yield return null;
        }

        targetScript.holes.Remove(gameObject);

        Destroy(gameObject);
    }
}