using System.Collections;
using UnityEngine;

public class MinecartController : MonoBehaviour
{
    //gerenciamento de trajeto
    private bool currentCart = false;
    private bool finishedTrack = false;

    private bool dynamite = false;

    //movimento
    private MinecartMovement movementScript;
    private Rigidbody2D rb;

    //controle da bomba de ar
    [SerializeField] Pump_SensorDistance pump;
    private bool movementStarted = false;

    [Header("Freio")]
    [SerializeField] private float cooldown = 5f;
    private Coroutine cooldownCoroutine; 
    private bool canBreak = false;

    //-------------------------------------------\\

    void Start(){
        movementScript = GetComponent<MinecartMovement>();
        movementScript.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }
    //
    void Update(){
        
        if(currentCart){

            if(canBreak){
                CheckPumpInput();
            }
        }
    }
    //

    public void SetCurrent(){
        currentCart = true;
        canBreak = true;
        movementScript.enabled = true;
    }
    //
    public bool IsTrackComplete(){
        return finishedTrack;
    }
    //
    public bool Dynamite(){
        return dynamite;
    }
    //
    void StopMovement(){
        rb.velocity = Vector2.zero;
        movementScript.enabled = false;
    }
    //
    void CheckPumpInput(){

        if(!movementStarted){

            if(pump.GetDistance() >= 30f) movementStarted = true;
        }
        else{
            if(pump.GetDistance() <= 10f){

                Break();
                movementStarted = false;
            }
        }

        //teste
        if(Input.GetKeyDown(KeyCode.Space)){
           Break();
        }
    }
    //
    void Break(){

        if(movementScript.SlowDown()){
            StopMovement();
            canBreak = false;
        }

        cooldownCoroutine ??= StartCoroutine(Cooldown());
    }
    //
    IEnumerator Cooldown(){

        yield return new WaitForSeconds(cooldown);

        movementScript.enabled = true;

        yield return new WaitForSeconds(1f);

        movementScript.SetToMaxSpeed();
        canBreak = true;
        cooldownCoroutine = null;
    }
    //
    
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Objective")){

            StopMovement();
            currentCart = false;
            finishedTrack = true;
        }

        if(other.CompareTag("Dynamite")){
            StopMovement();
            movementScript.enabled = false;
            dynamite = true;
        }
    }
}