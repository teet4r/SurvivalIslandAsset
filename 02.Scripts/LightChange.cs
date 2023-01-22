using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Light blueLight;
    public Light yellowLight;
    public Light greenLight;

    Coroutine lightOnOff = null;

    [SerializeField]
    [Range(0.1f, 5f)]
    float changTime;

    void Start()
    {
        TurnOn();
    }

    void TurnOn()
    {
        if (lightOnOff == null)
            lightOnOff = StartCoroutine(LightOnOff());
    }
    void TurnOff()
    {
        if (lightOnOff != null)
            StopCoroutine(lightOnOff);
    }

    IEnumerator LightOnOff()
    {
        while (true)
        {
            blueLight.enabled = true;
            yellowLight.enabled = false;
            greenLight.enabled = false;
            yield return new WaitForSeconds(changTime);
            blueLight.enabled = false;
            yellowLight.enabled = true;
            greenLight.enabled = false;
            yield return new WaitForSeconds(changTime);
            blueLight.enabled = false;
            yellowLight.enabled = false;
            greenLight.enabled = true;
            yield return new WaitForSeconds(changTime);
        }
    }
}
