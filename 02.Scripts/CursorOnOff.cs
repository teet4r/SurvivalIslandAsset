using UnityEngine;
using UnityEngine.UI;

public class CursorOnOff : MonoBehaviour, ICustomUpdate
{
    [SerializeField]
    Image panelImage;
    [SerializeField]
    [Range(0.1f, 5f)]
    float blinkInterval;
    float timePrev;

    void OnEnable()
    {
        RegisterCustomUpdate();
    }
    void Start()
    {
        panelImage = GetComponent<Image>();
        timePrev = Time.time;
    }
    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    public void CustomUpdate()
    {
        if (Time.time - timePrev > blinkInterval)
        {
            timePrev = Time.time;
            panelImage.enabled = !panelImage.enabled;
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
