using UnityEngine;
using UnityEngine.InputSystem;

public class TapeShooter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input;
    [SerializeField] private float speed;
    [SerializeField] private GameObject tape;
    [SerializeField] private HoleGenerator holeScript;
    private GameObject hole;

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
            Instantiate(tape, hole.transform.position, Quaternion.identity);
            holeScript.RemoveHole(hole);
            Destroy(hole);
        }


    }


    public void FixedUpdate()
    {
        rb.velocity = speed * Time.fixedDeltaTime * input;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            colliding = true;
            hole = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            colliding = false;
        }
    }
}
