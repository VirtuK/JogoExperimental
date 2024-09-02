using System.Collections;
using UnityEngine;

public class MinecartController : MonoBehaviour
{
    //controle bomba de ar
    [SerializeField] Pump_SensorDistance pump;
    private bool movementStarted = false;
    private bool currentCart = false;
    private bool canBreak = false;

    //movimento do carrinho;
    private Rigidbody2D rb;
    [SerializeField] float speed = 3f;
    [SerializeField] float breakPower = 0.5f;
    private bool finishedTrack = false;

    //contador de tempo
    private bool countingdown = false;

    //----------------------------\\

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    //
    void Update() {
        
        if(currentCart){

            if(canBreak){
            
                 if(!countingdown) CheckInput();
            }
        }
    }
    //
    public void Move(){

        rb.velocity = Vector2.up * speed;

        if(!currentCart) {
            currentCart = true;
            canBreak = true;
        }
    }
    //
    void CheckInput(){

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

       rb.velocity = new(0f, rb.velocity.y - breakPower);

        if(rb.velocity.y <= 0f){

            rb.velocity = Vector2.zero;
            canBreak = false;
            StartCoroutine(Countdown());
        }
    }
    //
    IEnumerator Countdown(){

        countingdown = true;

        yield return new WaitForSeconds(5f);

        Move();

        yield return new WaitForSeconds(2f);

        canBreak = true;

        countingdown = false;
    }
    //
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Destiny")){
            rb.velocity = Vector2.zero;
            currentCart = false;
            finishedTrack = true;
        }
    }
    //
    public bool IsTrackComplete(){
        return finishedTrack;
    }
}