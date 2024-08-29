using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class GetDistanceHCSR : MonoBehaviour
{
    public Transform distancePlane;

    public bool enableSmooth = false;
    public bool hideWhenTooFar = false;

    float distance = 0;
    int minDistance = 1;

    public float maxDistance = 200;

    [Range(0,30)]
    public float divideDistance = 15.0f;

    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }

    void Update()
    {
        // Move the plane
        if (enableSmooth)
        {
            distancePlane.transform.position = Vector3.Lerp(distancePlane.transform.position, new Vector3(0, 0, distance / divideDistance), Time.deltaTime * 10.0f);
        } else
        {
           distancePlane.transform.position = new Vector3(0, 0, distance / divideDistance);
        }
    }

    void DataReceived(string data, UduinoDevice baord)
    {
        float d = 0;
        bool ok = float.TryParse(data, out d); // Trying to parse data to a float

        if(ok)
        {
            if(d > 0)
                distance = d;
           
            
            if (hideWhenTooFar &&  distance > maxDistance)
            {
                distancePlane.gameObject.SetActive(false);
            } else
            {
                if(!distancePlane.gameObject.activeInHierarchy)
                 distancePlane.gameObject.SetActive(true);
            }
        } else
        {
            Debug.Log("Error parsing " + data);
        }
    }
}
