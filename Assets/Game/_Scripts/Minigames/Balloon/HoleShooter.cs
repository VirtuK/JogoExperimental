using UnityEngine;
using UnityEngine.InputSystem;

public class HoleShooter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input;
    [SerializeField] private float speed;
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private RipGenerator ripScript;
    private GameObject ripObj;
    private GameObject holeObj;

    private bool colliding = false;


    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();


    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if(context.performed && colliding)
        {
            holeObj = Instantiate(holePrefab, ripObj.transform.position, Quaternion.identity);
            ripScript.RemoveRip(ripObj);
            ripScript.holes.Add(holeObj);
            Destroy(ripObj);
        }


    }


    public void FixedUpdate()
    {
        rb.velocity = speed * Time.fixedDeltaTime * input;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rip"))
        {
            colliding = true;
            ripObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rip"))
        {
            colliding = false;
        }
    }
}
