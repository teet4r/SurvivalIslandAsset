using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] ak47;
    public MeshRenderer[] m4a1;

    public Animation animation;

    public bool isHaveM4A1 = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isHaveM4A1 = false;
            animation.Play("draw");
            spas12.enabled = true;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = false;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isHaveM4A1 = false;
            animation.Play("draw");
            spas12.enabled = false;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = true;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isHaveM4A1 = true;
            animation.Play("draw");
            spas12.enabled = false;
            for (int i = 0; i < ak47.Length; i++)
                ak47[i].enabled = false;
            for (int i = 0; i < m4a1.Length; i++)
                m4a1[i].enabled = true;
        }
    }
}
