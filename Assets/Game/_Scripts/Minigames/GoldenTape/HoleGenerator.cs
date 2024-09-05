using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private GameObject balloon;

    private List<GameObject> holes = new List<GameObject>();

    [SerializeField] private int initialTimer;
    [SerializeField] private int holeIntervalMax;
    [SerializeField] private int holeIntervalMin;
    [SerializeField] private int nMaxHole;



    private void Start()
    {
        StartCoroutine( Timer(initialTimer) );
    }


    private void Update()
    {
        
    }

    IEnumerator Timer(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (holes.Count < nMaxHole)
        {
            GameObject holeInstance = Instantiate(holePrefab);
            Vector3 targetPosition = generateHolePosition(balloon.transform, holeInstance.transform);
            for (int i = 0; i < holes.Count; i++)
            {
                if (Mathf.Pow(targetPosition.x - holes[i].transform.position.x, 2)
                    + Mathf.Pow(targetPosition.y - holes[i].transform.position.y, 2)
                    <= Mathf.Pow(holes[i].transform.localScale.x, 2))
                {
                    targetPosition = generateHolePosition(balloon.transform, holeInstance.transform);
                    i = 0;
                }
            }
            holeInstance.transform.position = targetPosition;
            holeInstance.transform.parent = balloon.transform;
            holes.Add(holeInstance);
        }
        StartCoroutine( Timer(Random.Range(holeIntervalMin, holeIntervalMax) ) );


    }


    private Vector3 generateHolePosition(Transform t, Transform b)
    {
        float randRX = Random.Range( 0, (t.localScale.x/ 2) - (b.localScale.x) );
        float randRY = Random.Range(0, (t.localScale.y/ 2) - (b.localScale.y) );
        float angle = Random.Range(0f, 360f);

        return new Vector3 ( ((randRX) * Mathf.Cos(angle) + t.position.x), ((randRY) * Mathf.Sin(angle) + t.position.y), 0 );
    }

    public int TotalHoles()
    {
        return holes.Count;
    }

    public void RemoveHole(GameObject hole)
    {
        foreach(GameObject obj in holes)
        {
            if(obj.transform.position == hole.transform.position)
            {
                holes.Remove(obj);
                return;
            }
        }
    }
}
