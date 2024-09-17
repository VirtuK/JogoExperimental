using UnityEngine;
using UnityEngine.InputSystem;

public class HoleShooter : MonoBehaviour
{
    //---------------------------------------------\\

    private Rigidbody2D rb;
    private Vector2 input;
    [SerializeField] private float speed;
    //
    [SerializeField] private GameObject holePrefab;
    private GameObject holeObj;
    private GameObject targetObj;
    //
    [SerializeField] private TargetGenerator generatorScript;
    [SerializeField] BalloonAir balloonScript;
    //
    private bool canShoot = false;

    //---------------------------------------------\\

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //
    public void FixedUpdate()
    {
        rb.velocity = speed * Time.fixedDeltaTime * input;
    }
    //---------------------------------------------\\

    public void OnLeftStick(InputAction.CallbackContext context)
    {
        input = new(input.x, Mathf.RoundToInt(context.ReadValue<float>()));
    }
    //
    public void OnRightStick(InputAction.CallbackContext context)
    {
        input = new(Mathf.RoundToInt(context.ReadValue<float>()), input.y);
    }
    //
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed && canShoot && balloonScript.playing)
        {
            holeObj = Instantiate(holePrefab, targetObj.transform.position, Quaternion.identity);
            holeObj.transform.parent = balloonScript.transform.GetChild(0);
            generatorScript.holes.Add(holeObj);

            generatorScript.RemoveTarget(targetObj);
            Destroy(targetObj);
        }
    }
    //---------------------------------------------\\

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            canShoot = true;
            targetObj = collision.gameObject;
        }
    }
    //
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            canShoot = false;
        }
    }
}