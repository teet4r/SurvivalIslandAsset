using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorOnOff : MonoBehaviour
{
    [SerializeField]
    Image panelImage;
    [SerializeField]
    [Range(0.1f, 5f)]
    float blinkInterval;
    float timePrev;

    void Start()
    {
        panelImage = GetComponent<Image>();
        timePrev = Time.time;
    }

    void Update()
    {
        if (Time.time - timePrev > blinkInterval)
        {
            timePrev = Time.time;
            panelImage.enabled = !panelImage.enabled;
        }
    }
}
