using UnityEngine;

public class MinecartMovement : MonoBehaviour
{
    //movimento
    [Header("Velocidade do carrinho")]
    [SerializeField] private float maxSpeed = 2f;
    private float speed;
    [SerializeField] private float breakPower = 0.5f;
    Rigidbody2D rb;

    //curva
    private bool curve = false;
    private readonly Vector3[] curvePoints = new Vector3[3];
    private float interpolation;
    //--------------------------------------\\

    void OnEnable() {
        speed = maxSpeed;
    }
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    //
    void Update() {

         if(curve){

            interpolation += Time.deltaTime;

            if(interpolation >= 1f) curve = false;

            Vector3 targetPos = CubicLerp(curvePoints[0],curvePoints[1],curvePoints[2],interpolation); //calculo da curva de bezier
            rb.velocity = (targetPos - transform.position).normalized * speed;

            LookFoward(curvePoints[0],curvePoints[1],curvePoints[2], interpolation);
         }
         else{
            rb.velocity = transform.up * speed;
         }

    }
    //
    public bool SlowDown(){

        if(!curve) speed *= 0.6f;
        return speed <= 0.5f;
    }
    //
    Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, float t){

        Vector3 ab = Vector3.Lerp(a,b,t);
        Vector3 bc = Vector3.Lerp(b,c,t);

        return Vector3.Lerp(ab,bc,t);
    }
    //
    void LookFoward(Vector3 a, Vector3 b, Vector3 c, float t){

        Vector3 tangent = (1 - t) * (b - a) + t * (c - b);
        tangent = tangent.normalized;

        float angle = Mathf.Atan2(tangent.x, tangent.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,-angle);
    }
    //
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("RailwayCurve")) {

            for(int i = 0; i < curvePoints.Length; i++){
                curvePoints[i] = other.transform.GetChild(i).transform.position;
            }
            curve = true;
            interpolation = 0f;
        }
    }
}