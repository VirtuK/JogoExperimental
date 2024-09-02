using UnityEngine;

public class MinecartManager : MonoBehaviour
{
    [SerializeField] GameObject[] carts;
    private MinecartController cartScript;

    //----------------------------\\
    void Start()
    {
        Sort();
    }
    //sorteia o próximo carrinho
    void Sort(){

       cartScript = carts[Random.Range(0,carts.Length-1)].GetComponent<MinecartController>();
       if(cartScript != null) cartScript.Move();
    }
}