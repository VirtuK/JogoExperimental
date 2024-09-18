using UnityEngine;

public class CartMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float duration;

    private bool curve = false;
    private float currentTime;

    private float startRotation;
    private float targetRotation;
    private float rotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * -speed;
    }

    void Update()
    {
        if (curve)
        {
            if (currentTime < duration)
            {
                float t = currentTime / duration;
                rotation = Mathf.Lerp(startRotation, targetRotation, t);
            }
            else
            {
                curve = false;
                rotation = targetRotation;
            }
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, rotation);
            currentTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RailwayCurve"))
        {
            curve = true;
            currentTime = 0f;

            startRotation = transform.localEulerAngles.z;
            targetRotation = SetDirection(other.transform);

            other.enabled = false;
        }
    }
    float SetDirection(Transform curve)
    {

        float direction = 0f;

        if (curve.localScale.x > 0) //curva para direita
        {
            direction = startRotation - 90f;
        }

        else if (curve.localScale.x < 0) //curva para esquerda
        {
            direction = startRotation + 90f;
        }

        return direction;
    }
}