using System.Collections.Generic;
using UnityEngine;

public class MinecartGameManager : MonoBehaviour
{
    //carts
    [SerializeField] List<MinecartController> carts = new();
    private MinecartController cartScript;
    private int cartsRemaining;

    //timer
    [SerializeField] private MinigameTimer timer;

    //----------------------------\\
    void Start(){
        
        cartsRemaining = carts.Count;
        Sort();
    }
    //
    void Update(){

        if(cartScript != null){

            if(cartScript.IsTrackComplete()) {

                cartsRemaining--;

                if(cartsRemaining <=0){
                    timer.StopTimer();
                    print("Ganhou :)");
                }
                else {
                    Sort();
                }
            }
        }

        if(timer.TimeUp()){

            print("Perdeu :/");
            Time.timeScale = 0f;
        }
    }
    //sorteia o próximo carrinho
    void Sort(){

        int index = Random.Range(0, carts.Count - 1);
        
        if(carts.Count > 0){

            cartScript = carts[index];
            carts.Remove(carts[index]);
        
            cartScript.SetCurrent();
        }
    }
}