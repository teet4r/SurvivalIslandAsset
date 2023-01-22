using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;
    AudioSource audioSource;
    Controller controller;

    const string bulletTag = "Bullet";

    [SerializeField]
    [Range(1, 1000)]
    int maxHp;
    int hp;
    [SerializeField]
    Collider[] weaponCollider;

    public Image hpBar;
    public Canvas hpCanvas;
    public AudioClip audioClip;

    public bool isDie { get; private set; }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<Controller>();
    }

    void OnEnable()
    {
        isDie = false;
        hp = maxHp;
        DrawHpBar();
        GetComponent<CapsuleCollider>().enabled = true;
        hpCanvas.enabled = true;
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(bulletTag))
        {
            HitAniEffect(collision);
            hp -= 3;
            DrawHpBar();
            if (hp <= 0)
                Die();
        }
    }

    void Die()
    {
        if (isDie) return;
        isDie = true;
        animator.SetTrigger("IsDie");
        GetComponent<CapsuleCollider>().enabled = false;
        hpCanvas.enabled = false;
        audioSource.PlayOneShot(audioClip);
        GameManager.instance.KillCount(1);
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = false;
        Invoke("RemoveObject", 3f);
    }

    void RemoveObject()
    {
        PoolManager.instance.Put(gameObject);
    }

    void HitAniEffect(Collision collision)
    {
        PoolManager.instance.Put(collision.gameObject);
        navMeshAgent.isStopped = true;
        animator.SetTrigger("IsHit");

        var blood = PoolManager.instance.Get("Blood");
        blood.transform.position = collision.transform.position;
        blood.transform.rotation = Quaternion.identity;
        blood.SetActive(true);
    }

    void DrawHpBar()
    {
        hpBar.fillAmount = (float)hp / maxHp;
        hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0);
    }
}