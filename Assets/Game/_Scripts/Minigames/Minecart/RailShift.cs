using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;


//---------------------------------------------\\
[System.Serializable]
public struct Lever
{
    public SpriteRenderer codeLSprite;
    public SpriteRenderer codeRSprite;
    public SpriteRenderer curveSprite;
    public BoxCollider2D rightTrack;
}
//---------------------------------------------\\
public class RailShift : MonoBehaviour
{
    [SerializeField] Image leverLeft;
    [SerializeField] Image leverRight;

    //---------------------------------------------\\
    [SerializeField] Lever[] levers;
    //---------------------------------------------\\

    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite highlight;

    //---------------------------------------------\\
    [SerializeField] Color activeColor;
    private int selectionIndex = 0;
    private int activeIndex = 0;
    //---------------------------------------------\\
    [SerializeField] float cooldown;
    Coroutine cooldownCoroutine;

    //---------------------------------------------\\
    void Start()
    {
        leverLeft.color = Color.gray;
        leverRight.color = Color.gray;
    }
    //
    void CheckCode()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (leverLeft.color == levers[i].codeLSprite.color)
            {
                if (leverRight.color == levers[i].codeRSprite.color)
                {
                    if (cooldownCoroutine != null)
                    {
                        if (selectionIndex != activeIndex)
                        {
                            levers[selectionIndex].curveSprite.sprite = normalSprite;
                        }
                    }
                    else levers[selectionIndex].curveSprite.sprite = normalSprite;

                    levers[i].curveSprite.sprite = highlight;
                    selectionIndex = i;
                    return;
                }
            }
        }
    }
    //
    public void OnLeftStick(InputAction.CallbackContext context)
    {
        if (Mathf.RoundToInt(context.ReadValue<float>()) > 0)
        {
            leverLeft.color = Color.blue;
        }
        else if (Mathf.RoundToInt(context.ReadValue<float>()) < 0)
        {
            leverLeft.color = Color.red;
        }

        CheckCode();
    }
    //
    public void OnRightStick(InputAction.CallbackContext context)
    {
        if (Mathf.RoundToInt(context.ReadValue<float>()) > 0)
        {
            leverRight.color = Color.blue;
        }
        else if (Mathf.RoundToInt(context.ReadValue<float>()) < 0)
        {
            leverRight.color = Color.red;
        }

        CheckCode();
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && cooldownCoroutine == null)
        {
            leverLeft.color = Color.gray;
            leverRight.color = Color.gray;

            activeIndex = selectionIndex;

            levers[activeIndex].curveSprite.color = activeColor;
            levers[activeIndex].curveSprite.GetComponent<BoxCollider2D>().enabled = true;

            if (levers[activeIndex].rightTrack != null)
            {
                levers[activeIndex].rightTrack.enabled = false;
            }

            cooldownCoroutine ??= StartCoroutine(Cooldown());
        }
    }
    //
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        levers[activeIndex].curveSprite.sprite = normalSprite;
        levers[activeIndex].curveSprite.color = Color.white;
        levers[activeIndex].curveSprite.GetComponent<BoxCollider2D>().enabled = false;

        if (levers[activeIndex].rightTrack != null)
        {
            levers[activeIndex].rightTrack.enabled = true;
        }

        cooldownCoroutine = null;
    }
}