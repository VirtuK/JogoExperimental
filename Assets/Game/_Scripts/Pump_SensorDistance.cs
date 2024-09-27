using UnityEngine;
using Uduino;

public class Pump_SensorDistance : MonoBehaviour
{
    float distance = 0;

    //------------------------------------\\

    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }
    //
    void DataReceived(string data, UduinoDevice board)
    {
        bool ok = float.TryParse(data, out float d);

        if (ok)
        {
            if (d > 0)
            {
                distance = d;
            }
        }
        else
        {
            Debug.Log("Error parsing " + data);
        }
    }
    //
    public float GetDistance()
    {
        return distance;
    }
}
/*
Controle bomba de ar:

Distância mínima (fechada) - <= 5cm
Distância máxima (aberta) - >= 44cm
*/