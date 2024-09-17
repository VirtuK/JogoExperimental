using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed;
    private float offset;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {

        offset += Time.deltaTime * speed;

        material.SetTextureOffset("_MainTex", new(offset, offset));
    }
}