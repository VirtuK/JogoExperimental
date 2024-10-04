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

    //-------------------------------------------\\

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
}