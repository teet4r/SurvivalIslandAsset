using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour, ICustomUpdate
{
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] ak47;
    public MeshRenderer[] m4a1;

    [SerializeField] Animation _animation;

    public bool HasM4A1 = false;

    void OnEnable()
    {
        RegisterCustomUpdate();
    }

    void OnDisable()
    {
        DeregisterCustomUpdate();
    }

    public void CustomUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HasM4A1 = false;
            _animation.Play("draw");
            spas12.enabled = true;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = false;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HasM4A1 = false;
            _animation.Play("draw");
            spas12.enabled = false;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = true;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HasM4A1 = true;
            _animation.Play("draw");
            spas12.enabled = false;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = false;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = true;
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
