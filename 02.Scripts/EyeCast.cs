using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour, ICustomFixedUpdate
{
    Transform tr;
    Ray ray;
    RaycastHit raycastHit;
    public float dist = 50f;

    void OnEnable()
    {
        RegisterCustomFixedUpdate();
    }

    void OnDisable()
    {
        DeregisterCustomFixedUpdate();
    }

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    public void CustomFixedUpdate()
    {
        ray = new Ray(tr.position, tr.forward);
        //Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);

        // 적이 광선에 맞았다면
        // out: 함수 안에서 변경된 값을 밖으로 꺼내옴
        if (Physics.Raycast(ray, out raycastHit, dist, 1 << 8 | 1 << 9 | 1 << 10))
            Crosshair.instance.isGazing = true;
        else
            Crosshair.instance.isGazing = false;
    }

    public void RegisterCustomFixedUpdate()
    {
        CustomUpdateManager.Instance.RegisterCustomFixedUpdate(this);
    }

    public void DeregisterCustomFixedUpdate()
    {
        CustomUpdateManager.Instance.DeregisterCustomFixedUpdate(this);
    }
}