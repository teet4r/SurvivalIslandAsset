using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    Transform tr;
    Image crosshair;

    float startTime;

    [SerializeField]
    float duration;
    [SerializeField]
    float minSize;
    [SerializeField]
    float maxSize;

    Color originColor = new Color(1f, 1f, 1f, 0.8f);
    Color gazingColor = Color.red;

    public bool isGazing = false;
    public static Crosshair instance;

    void Start()
    {
        tr = GetComponent<Transform>();
        crosshair = GetComponent<Image>();
        startTime = Time.time;
        tr.localScale = Vector3.one * minSize;
        crosshair.color = originColor;
        instance = this;
    }

    void Update()
    {
        if (isGazing)
        {
            float t = (Time.time - startTime) / duration;
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t); // 보간함수
            crosshair.color = gazingColor;
        }
        else
        {
            tr.localScale = Vector3.one * minSize;
            crosshair.color = originColor;
            startTime = Time.time;
        }
    }
}
