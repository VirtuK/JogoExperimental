using UnityEngine;

public class HoleScript : MonoBehaviour
{
    private float time = 0f;

    [Range(0, 100)]
    [SerializeField]
    private float speed = 3f;

    private float colorAlpha;

    private SpriteRenderer sprite;

    private RipGenerator ripScript;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        time += speed * Time.deltaTime;
        colorAlpha = Mathf.Lerp(1f, 0f, time);

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, colorAlpha);

        if (colorAlpha == 0f)
        {
            Destroy(gameObject);
        }
    }

    public void RemoveHole(GameObject hole)
    {
        foreach (GameObject obj in ripScript.holes)
        {
            if (obj.transform.position == hole.transform.position)
            {
                ripScript.holes.Remove(obj);
                return;
            }
        }

    }
}
