using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public Light inLight;
    public AudioSource source;
    public AudioClip onSound;
    public AudioClip offSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inLight.enabled = true;
            SoundManager.Instance.SfxAudio.Play("LightOn");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inLight.enabled = false;
            SoundManager.Instance.SfxAudio.Play("LightOff");
        }
    }
}
