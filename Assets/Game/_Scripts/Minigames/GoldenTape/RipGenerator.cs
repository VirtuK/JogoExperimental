using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ripPrefab;
    [SerializeField] private GameObject balloon;

    public List<GameObject> holes = new List<GameObject>();
    public List<GameObject> rips = new List<GameObject>();

    [SerializeField] private int initialTimer;
    [SerializeField] private int holeIntervalMax;
    [SerializeField] private int holeIntervalMin;
    [SerializeField] private int nMaxHole;



    private void Start()
    {
        StartCoroutine( Timer(initialTimer) );
    }

    IEnumerator Timer(float interval)
    {

        yield return new WaitForSeconds(interval);
        if (balloon.transform.parent.GetComponent<BalloonAir>().started)
        {
            GenerateHole();
        }
            StartCoroutine(Timer(Random.Range(holeIntervalMin, holeIntervalMax)));
    }

    private void GenerateHole()
    {
        if (rips.Count < nMaxHole)
        {
            GameObject ripInstance = Instantiate(ripPrefab);
            ripInstance.transform.position = GenerateRipPosition(balloon.transform, ripInstance.transform);
            ripInstance.transform.parent = balloon.transform;
            rips.Add(ripInstance);
        }
    }
    public Vector3 GenerateRipPosition(Transform t, Transform b)
    {
        float randRX = Random.Range( 0, (t.localScale.x/ 2) - (b.localScale.x) );
        float randRY = Random.Range(0, (t.localScale.y/ 2) - (b.localScale.y) );
        float angle = Random.Range(0f, 360f);

        return new Vector3 ( ((randRX) * Mathf.Cos(angle) + t.position.x), ((randRY) * Mathf.Sin(angle) + t.position.y), 0 );
    }

    public void RemoveRip(GameObject rip)
    {
        foreach(GameObject obj in rips)
        {
            if(obj.transform.position == rip.transform.position)
            {
                rips.Remove(obj);
                return;
            }
        }
    }
}
