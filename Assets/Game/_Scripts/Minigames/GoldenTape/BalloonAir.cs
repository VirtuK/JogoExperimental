using Unity.VisualScripting;
using UnityEngine;

public class BalloonAir : MonoBehaviour
{
    [SerializeField] private Transform balloon;
    [Header("Air controller")]
    [Range(0,100)][SerializeField] private float airInside;
    [Range(0, 100)][SerializeField] private float airLoss;
    [Range(0, 100)][SerializeField] private float airMin;

    [Header("Rope components")]
    [SerializeField] private LineRenderer lineRendererLeft;
    [SerializeField] private LineRenderer lineRendererRight;

    [SerializeField] private Transform AnchorLeft;
    [SerializeField] private Transform AnchorRight;

    [Header("Hole generator")]
    private HoleGenerator holesScript;

    private void Start()
    {
        holesScript = GetComponent<HoleGenerator>();
    }


    private void Update()
    {
        if(airInside > airMin)
        {
            airInside -= holesScript.TotalHoles() * airLoss * Time.deltaTime;
            lineRendererLeft.SetPosition(1,AnchorLeft.position);
            lineRendererRight.SetPosition(1, AnchorRight.position);
            balloon.localScale = Vector3.Lerp(balloon.localScale, new Vector3(airInside, airInside, airInside), airLoss);

        }
        
        else
        {
            print("Perdeu");
        }
    }
}
