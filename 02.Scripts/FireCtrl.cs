using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour, ICustomUpdate
{
    public Animation combatAni;
    public Transform firePos;
    public AudioSource audioSource;
    public AudioClip bulletSound;
    public AudioClip flashSound;
    public Light flashLight;
    public bool isReloading { get; private set; } = false;

    RunningGun _runningGun;
    WeaponChange _weaponChange;
    int _bulletCount = 0;

    int _enemyLayers = 1 << 8 | 1 << 9 | 1 << 10;

    [SerializeField] float _reloadTime = 0.5f;
    WaitForSeconds _wfsReload0 = null;
    WaitForSeconds _wfsReload1 = null;

    void Awake()
    {
        _runningGun = GetComponent<RunningGun>();
        _weaponChange = GetComponent<WeaponChange>();

        _wfsReload0 = new WaitForSeconds(_reloadTime);
        _wfsReload1 = new WaitForSeconds(0.8f);
    }
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
        // 왼쪽 버튼
        if (Input.GetMouseButtonDown(0))
        {
            if (!_runningGun.isRunning)
            {
                if (_weaponChange.HasM4A1)
                    StartCoroutine(Jum4Fire());
                else
                    _Fire();
            }
        }

        // flash on/off
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLight.enabled = !flashLight.enabled;
            audioSource.PlayOneShot(flashSound, 1f);
        }

        _Reload();
    }

    void _Fire()
    {
        if (isReloading) return;

        _MakeBulletTrailer();
        //_MakeBullet();

        combatAni.Play("fire");
        audioSource.PlayOneShot(bulletSound, 1f);
        ++_bulletCount;
    }
    IEnumerator Jum4Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            _Fire();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void _MakeBulletTrailer()
    {
        var bulletTracer = PoolManager.Instance.Get("BulletTracer").GetComponent<LineRenderer>();
        if (Physics.Raycast(firePos.position, firePos.forward, out RaycastHit _raycastHit, 50f, _enemyLayers))
        {
            if (_raycastHit.transform.TryGetComponent(out GetDamage getDamage))
                getDamage.OnDamage(_raycastHit.point, 10);
            bulletTracer.SetPositions(new Vector3[] { firePos.position, _raycastHit.point });
        }
        else
            bulletTracer.SetPositions(new Vector3[] { firePos.position, firePos.forward * 50f });
        bulletTracer.gameObject.SetActive(true);
    }
    void _MakeBullet()
    {
        var bullet = PoolManager.Instance.Get("Bullet");
        bullet.transform.position = firePos.position;
        bullet.transform.rotation = firePos.rotation;
        bullet.SetActive(true);
    }

    void _Reload()
    {
        if (_bulletCount == 10 && !isReloading)
            StartCoroutine(_ReloadDelay());
    }
    IEnumerator _ReloadDelay()
    {
        isReloading = true;

        yield return _wfsReload0;
        combatAni.Stop("fire");
        combatAni.CrossFade("pump1", 0.3f); // 앞선 애니메이션과 지금하려는 애니메이션을 0.3초간 겹치게 하여
                                            // 부드러운 애니메이션, 즉 블렌드 애니메이션을 만들기 위함
        _bulletCount = 0;
        yield return _wfsReload1;

        isReloading = false;
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