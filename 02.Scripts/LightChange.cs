using System.Collections;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public bool IsLighting { get; private set; } = false;

    [SerializeField] Light[] lights;
    [SerializeField][Range(0.1f, 5f)] float _changeTime = 1f;
    WaitForSeconds _wfsChangeTime = null;

    void Awake()
    {
        _wfsChangeTime = new WaitForSeconds(_changeTime);
    }

    void Start()
    {
        TurnOn();
    }

    void TurnOn()
    {
        if (IsLighting) return;

        StartCoroutine(LightOnOff());
    }

    void TurnOff()
    {
        if (!IsLighting) return;

        IsLighting = false;
    }

    IEnumerator LightOnOff()
    {
        IsLighting = true;
        int idx = 0;
        while (IsLighting)
        {
            lights[idx].enabled = true;
            yield return _wfsChangeTime;
            lights[idx].enabled = false;
            
            ++idx;
            if (idx >= lights.Length)
                idx = 0;
        }
        IsLighting = false;
    }
}
