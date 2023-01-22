using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform canvasTr;
    Transform cameraTr;

    void Start()
    {
        canvasTr = GetComponent<Transform>();
        cameraTr = Camera.main.transform;
    }

    void Update()
    {
        canvasTr.LookAt(cameraTr);
    }
}
