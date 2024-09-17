using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject balloon;
    [SerializeField] private BalloonAir balloonScript;

    public List<GameObject> holes = new List<GameObject>();
    public List<GameObject> targets = new List<GameObject>();

    [SerializeField] private int initialTimer;
    [SerializeField] private int holeIntervalMax;
    [SerializeField] private int holeIntervalMin;
    [SerializeField] private int nMaxHole;



    private void Start()
    {
        StartCoroutine(Timer(initialTimer));
    }

    IEnumerator Timer(float interval)
    {

        yield return new WaitForSeconds(interval);
        if (balloon.transform.parent.GetComponent<BalloonAir>().started)
        {
            GenerateHole();
        }
        if (balloonScript.playing) StartCoroutine(Timer(Random.Range(holeIntervalMin, holeIntervalMax)));
    }

    private void GenerateHole()
    {
        if (targets.Count < nMaxHole)
        {
            GameObject targetInstance = Instantiate(targetPrefab);
            targetInstance.transform.position = GenerateRipPosition(balloon.transform, targetInstance.transform);
            targetInstance.transform.parent = balloon.transform;
            targets.Add(targetInstance);
        }
    }
    public Vector3 GenerateRipPosition(Transform t, Transform b)
    {
        float randRX = Random.Range(0, (t.localScale.x / 2) - (b.localScale.x));
        float randRY = Random.Range(0, (t.localScale.y / 2) - (b.localScale.y));
        float angle = Random.Range(0f, 360f);

        return new Vector3(((randRX) * Mathf.Cos(angle) + t.position.x), ((randRY) * Mathf.Sin(angle) + t.position.y), 0);
    }

    public void RemoveTarget(GameObject target)
    {
        foreach (GameObject obj in targets)
        {
            if (obj.transform.position == target.transform.position)
            {
                targets.Remove(obj);
                return;
            }
        }
    }
}
