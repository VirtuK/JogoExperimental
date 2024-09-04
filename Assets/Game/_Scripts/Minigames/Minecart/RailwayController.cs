using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailwayController : MonoBehaviour
{
    //triggers da curva
    public List<BoxCollider2D> curveUp = new();
    public List<BoxCollider2D> curveDown = new();
    public List<BoxCollider2D> curveLeft = new();
    public List<BoxCollider2D> curveRight = new();
    public Vector2 inputDirection;

    //efeito viusal
    public Color activated,deactivated;
    private SpriteRenderer sprite;

    //cooldown
    public float cooldown = 4f;
    private bool waiting = false;
    void Start()
    {
        SetCurveTo(curveUp,false,deactivated);
        SetCurveTo(curveDown,false,deactivated);
        SetCurveTo(curveLeft,false,deactivated);
        SetCurveTo(curveRight,false,deactivated);
    }
    //
    public void ActivateCurve(InputAction.CallbackContext context)
    {
        if(context.performed && !waiting){

            inputDirection = context.ReadValue<Vector2>();
            
            if(inputDirection == Vector2.up){
                SetCurveTo(curveUp,true,activated);
            }
            else if(inputDirection == Vector2.down){
                SetCurveTo(curveDown,true,activated);
            }
            else if(inputDirection == Vector2.left){
                SetCurveTo(curveLeft,true,activated);
            }
            else if(inputDirection == Vector2.right){
                SetCurveTo(curveRight,true,activated);
            }

            StartCoroutine(Cooldown());
        }
    }
    //
    IEnumerator Cooldown(){

        waiting = true;

        yield return new WaitForSeconds(cooldown);

        waiting = false;
        
        if(inputDirection == Vector2.up){
            SetCurveTo(curveUp,false,deactivated);
        }
        else if(inputDirection == Vector2.down){
            SetCurveTo(curveDown,false,deactivated);
        }
        else if(inputDirection == Vector2.left){
            SetCurveTo(curveLeft,false,deactivated);
        }
        else if(inputDirection == Vector2.right){
            SetCurveTo(curveRight,false,deactivated);
        }
    }
    //
    private void SetCurveTo(List<BoxCollider2D> curveList, bool state, Color color){

        foreach(BoxCollider2D curve in curveList){
            curve.enabled = state;
            sprite = curve.gameObject.GetComponent<SpriteRenderer>();
            sprite.color = color;
        }
    }
}