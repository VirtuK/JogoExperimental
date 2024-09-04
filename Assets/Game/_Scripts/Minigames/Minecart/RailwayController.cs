using UnityEngine;

public class RailwayController : MonoBehaviour
{
    public BoxCollider2D curve;
    void Start()
    {
        curve.enabled = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            curve.enabled = !curve.enabled;
            Debug.Log("Apertou!");
        }
    }
}