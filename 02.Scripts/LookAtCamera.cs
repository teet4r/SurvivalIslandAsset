using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class LookAtCamera : MonoBehaviour, ICustomUpdate
{
    Transform _canvasTr;
    Transform _cameraTr;

    void OnEnable()
    {
        _canvasTr = GetComponent<Transform>();
        _cameraTr = Camera.main.transform;
        
        RegisterCustomUpdate();
    }
    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    public void CustomUpdate()
    {
        _canvasTr.LookAt(_cameraTr);
    }
    public void DeregisterCustomUpdate()
    {
        CustomUpdateManager.Instance.DeregisterCustomUpdate(this);
    }
    public void RegisterCustomUpdate()
    {
        CustomUpdateManager.Instance.RegisterCustomUpdate(this);
    }
}
