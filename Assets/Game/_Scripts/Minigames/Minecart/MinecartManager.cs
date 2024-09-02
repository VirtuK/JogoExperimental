using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinecartManager : MonoBehaviour
{
    //carts
    [SerializeField] List<MinecartController> carts = new List<MinecartController>();
    private MinecartController cartScript;
    private int cartsRemaining;

    //contador de tempo
    private bool counting;
    [SerializeField] private float maxTime = 120;
    private float currentTime;

    [SerializeField] private string counterText;
    [SerializeField] private TextMeshProUGUI counterUI;

    //----------------------------\\
    void Start(){
        
        cartsRemaining = carts.Count;
        Sort();

        counting = true;
        currentTime = maxTime;

        SetCounterUIText();
    }
    //
    void Update(){

        if(counting){
            currentTime -= Time.deltaTime;
        }

        SetCounterUIText();

        if(cartScript != null){
            if(cartScript.IsTrackComplete()) {
                Sort();
                cartsRemaining--;
            }
        }

        if(cartsRemaining <=0){
            print("Ganhou :)");
        }

        if(currentTime <= 0){
            counting = false;
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
        
            cartScript.Move();
        }
    }
    //
    void SetCounterUIText(){
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        counterText = time .ToString(@"m\:ss");
        counterUI.text = counterText;
    }
}