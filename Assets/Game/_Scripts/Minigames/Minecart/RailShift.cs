using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[System.Serializable]
public struct Curves
{
    public SpriteRenderer curveSprite;
    public BoxCollider2D rightTrack;
}
//---------------------------------------------\\
public class RailShift : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite highlight;

    //---------------------------------------------\\
    [SerializeField] float cooldown;
    Coroutine cooldownCoroutine;

    public CartMovement cartMovement;

    //---------------------------------------------\\

    [SerializeField] Curves[] curvesObj;
    [SerializeField] Curves[,] curves = new Curves[2, 2];

    int l = 0;
    int r = 1;

    //---------------------------------------------\\
    [SerializeField] Color activeColor;
    private int[] selectionIndex = new int[2];
    private int[] activeIndex = new int[2];

    //---------------------------------------------\\

    void Start()
    {
        curves[0, 0] = curvesObj[0];
        curves[1, 0] = curvesObj[1];
        curves[1, 1] = curvesObj[2];
        curves[0, 1] = curvesObj[3];

        CheckCode();
    }
    void CheckCode()
    {
        if (curves[selectionIndex[0], selectionIndex[1]].curveSprite.color != activeColor) curves[selectionIndex[0], selectionIndex[1]].curveSprite.sprite = normalSprite;

        selectionIndex[0] = l;
        selectionIndex[1] = r;

        curves[selectionIndex[0], selectionIndex[1]].curveSprite.sprite = highlight;
    }
    //
    public void OnLeftStick(InputAction.CallbackContext context)
    {
        if (Mathf.RoundToInt(context.ReadValue<float>()) > 0)
        {
            l = 0;
        }
        if (Mathf.RoundToInt(context.ReadValue<float>()) < 0)
        {
            l = 1;
        }
        CheckCode();
    }
    //
    public void OnRightStick(InputAction.CallbackContext context)
    {
        if (Mathf.RoundToInt(context.ReadValue<float>()) > 0)
        {
            r = 1;
        }
        if (Mathf.RoundToInt(context.ReadValue<float>()) < 0)
        {
            r = 0;
        }
        CheckCode();
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && cooldownCoroutine == null)
        {
            activeIndex[0] = l;
            activeIndex[1] = r;


            curves[activeIndex[0], activeIndex[1]].curveSprite.color = activeColor;

            if (!cartMovement.curve)
            {
                curves[activeIndex[0], activeIndex[1]].curveSprite.GetComponent<BoxCollider2D>().enabled = true;

                if (curves[activeIndex[0], activeIndex[1]].rightTrack != null)
                {
                    curves[activeIndex[0], activeIndex[1]].rightTrack.enabled = false;
                }
            }

            cooldownCoroutine ??= StartCoroutine(Cooldown());
        }
    }
    //
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        curves[activeIndex[0], activeIndex[1]].curveSprite.sprite = normalSprite;
        curves[activeIndex[0], activeIndex[1]].curveSprite.color = Color.white;
        curves[activeIndex[0], activeIndex[1]].curveSprite.GetComponent<BoxCollider2D>().enabled = false;


        if (curves[activeIndex[0], activeIndex[1]].rightTrack != null)
        {
            curves[activeIndex[0], activeIndex[1]].rightTrack.enabled = true;
        }

        cooldownCoroutine = null;
    }
}