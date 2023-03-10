using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour, ICustomUpdate
{
    public bool isGazing { get; set; } = false;
    public static Crosshair instance = null;
    
    [SerializeField] Image crosshair;
    [SerializeField] float duration;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    float startTime;
    Color originColor = new Color(1f, 1f, 1f, 0.8f);
    Color gazingColor = Color.red;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void OnEnable()
    {
        RegisterCustomUpdate();
    }
    void Start()
    {
        startTime = Time.time;
        transform.localScale = Vector3.one * minSize;
        crosshair.color = originColor;
    }
    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    public void CustomUpdate()
    {
        if (isGazing)
        {
            float t = (Time.time - startTime) / duration;
            transform.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t); // 보간함수
            crosshair.color = gazingColor;
        }
        else
        {
            transform.localScale = Vector3.one * minSize;
            crosshair.color = originColor;
            startTime = Time.time;
        }
    }
    public void RegisterCustomUpdate()
    {
        CustomUpdateManager.Instance.RegisterCustomUpdate(this);
    }
    public void DeregisterCustomUpdate()
    {
        CustomUpdateManager.Instance.DeregisterCustomUpdate(this);
    }
}
