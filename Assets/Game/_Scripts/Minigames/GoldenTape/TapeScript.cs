using UnityEngine;

public class TapeScript : MonoBehaviour
{
    [SerializeField] private RipGenerator generatorScript;
    [SerializeField] private GameObject balloon;
    private bool stay = false;

    private void Start()
    {
        generatorScript = FindObjectOfType<RipGenerator>();
        balloon = generatorScript.transform.GetChild(0).gameObject;

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rip") && (stay == false))
        {

        }
    }
}
