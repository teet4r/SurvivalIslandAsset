using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    Transform tr;
    Ray ray;
    RaycastHit raycastHit;
    public float dist = 15f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        ray = new Ray(tr.position, tr.forward * dist);
        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);

        // 적이 광선에 맞았다면
        // out: 함수 안에서 변경된 값을 밖으로 꺼내옴
        if (Physics.Raycast(ray, out raycastHit, dist, 1 << 8 | 1 << 9 | 1 << 10))
            Crosshair.instance.isGazing = true;
        else
            Crosshair.instance.isGazing = false;
    }
}