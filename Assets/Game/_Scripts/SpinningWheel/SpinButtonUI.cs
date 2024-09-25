using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpinButtonUI : MonoBehaviour
{
    //-------------------------------------------\\
    //barra UI do botão de girar
    [SerializeField] private Image bar;
    private float targetFillAmount = 0f;
    [SerializeField] private float fillAnimDuration = 0.2f;

    //-------------------------------------------\\
    [SerializeField] TextMeshProUGUI p1Text, p2Text;
    [SerializeField] Image spinButton;
    LerpScaleAnim anim;

    //-------------------------------------------\\

    void Start()
    {
        anim = GetComponent<LerpScaleAnim>();
        anim.PlayScaleAnim();
    }
    //
    public void StopAnim()
    {
        anim.StopScaleAnim();
    }
    //
    public void FillBar()
    {
        StartCoroutine(FillingAnim());
    }
    //
    public void AddFilling()
    {
        targetFillAmount += 0.5f;
    }
    //
    public void FillClockwise(bool clockwise)
    {
        if (clockwise) bar.fillClockwise = true;
        else bar.fillClockwise = false;
    }
    //
    IEnumerator FillingAnim()
    {
        float elapsedTime = 0f;
        float startValue = bar.fillAmount;

        while (elapsedTime < fillAnimDuration)
        {
            bar.fillAmount = Mathf.Lerp(startValue, targetFillAmount, elapsedTime / fillAnimDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bar.fillAmount = targetFillAmount;
    }
    //
    public IEnumerator FadeOutText()
    {
        while (bar.fillAmount < 1)
        {
            yield return null;
        }

        spinButton.enabled = true;

        float elapsedTime = 0f;
        float colorAlpha = 1f;

        yield return new WaitForSeconds(0.5f);

        while (colorAlpha > 0f)
        {
            elapsedTime += 2 * Time.deltaTime;
            colorAlpha = Mathf.Lerp(1f, 0f, elapsedTime);

            p1Text.color = new Color(p1Text.color.r, p1Text.color.g, p1Text.color.b, colorAlpha);
            p2Text.color = p1Text.color;

            yield return null;
        }
    }
    //
    public IEnumerator PressedAnim(int player)
    {
        float elapsedTime = 0f;
        float duration = 0.1f;

        Vector3 startValue = new(1f, 1f, 1f);
        Vector3 endValue = new(1.2f, 1.2f, 1.2f);

        while (elapsedTime < duration)
        {
            if (player == 1) p1Text.transform.localScale = Vector3.Lerp(startValue, endValue, elapsedTime / duration);
            else p2Text.transform.localScale = Vector3.Lerp(startValue, endValue, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (player == 1) p1Text.transform.localScale = Vector3.Lerp(endValue, startValue, elapsedTime / duration);
            else p2Text.transform.localScale = Vector3.Lerp(endValue, startValue, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (player == 1) p1Text.transform.localScale = startValue;
        else p2Text.transform.localScale = startValue;
    }
}