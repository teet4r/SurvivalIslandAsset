using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Animation combatAni;
    public Transform firePos;
    public AudioSource audioSource;
    public AudioClip bulletSound;
    public AudioClip flashSound;
    public Light flashLight;
    public bool isReloading { get; private set; } = false;

    RunningGun runningGun;
    WeaponChange weaponChange;
    int bulletCount = 0;

    [SerializeField] float reloadTime;
    WaitForSeconds _wfsReload0 = null;
    WaitForSeconds _wfsReload1 = null;

    void Awake()
    {
        runningGun = GetComponent<RunningGun>();
        weaponChange = GetComponent<WeaponChange>();

        _wfsReload0 = new WaitForSeconds(reloadTime);
        _wfsReload1 = new WaitForSeconds(0.8f);
    }

    void Update()
    {
        // 왼쪽 버튼
        if (Input.GetMouseButtonDown(0))
        {
            if (!runningGun.isRunning)
            {
                if (weaponChange.HasM4A1)
                    StartCoroutine(Jum4Fire());
                else
                    Fire();
            }
        }

        // flash on/off
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLight.enabled = !flashLight.enabled;
            audioSource.PlayOneShot(flashSound, 1f);
        }

        Reload();
    }

    void Fire()
    {
        if (isReloading) return;

        _MakeBullet();

        combatAni.Play("fire");
        audioSource.PlayOneShot(bulletSound, 1f);
        ++bulletCount;
    }

    IEnumerator Jum4Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            Fire();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void _MakeBullet()
    {
        var bullet = PoolManager.Instance.Get("Bullet");
        bullet.transform.position = firePos.position;
        bullet.transform.rotation = firePos.rotation;
        bullet.SetActive(true);
    }

    #region Reload Method
    void Reload()
    {
        if (bulletCount == 10 && !isReloading)
            StartCoroutine(ReloadDelay());
    }

    IEnumerator ReloadDelay()
    {
        isReloading = true;

        yield return _wfsReload0;
        combatAni.Stop("fire");
        combatAni.CrossFade("pump1", 0.3f); // 앞선 애니메이션과 지금하려는 애니메이션을 0.3초간 겹치게 하여
                                            // 부드러운 애니메이션, 즉 블렌드 애니메이션을 만들기 위함
        bulletCount = 0;
        yield return _wfsReload1;

        isReloading = false;
    }
    #endregion
}