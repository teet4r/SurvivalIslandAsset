using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GetDamage : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;

    const string bulletTag = "Bullet";

    [SerializeField][Range(1, 1000)] int maxHp;
    int hp;
    [SerializeField] Collider[] weaponCollider;

    public Image hpBar;
    public Canvas hpCanvas;
    public AudioClip audioClip;

    public bool isDie { get; private set; }

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

    public void OnDamage(Vector3 hitPoint, int damage)
    {
        HitFromRayAniEffect(hitPoint);
        hp -= damage;
        DrawHpBar();
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        if (isDie) return;
        isDie = true;

        animator.SetTrigger("IsDie");
        GetComponent<CapsuleCollider>().enabled = false;
        hpCanvas.enabled = false;
        SoundManager.Instance.SfxAudio.Play($"EnemyKilled{Random.Range(0, 3)}");
        GameManager.instance.KillCount(1);
        for (int i = 0; i < weaponCollider.Length; i++)
            weaponCollider[i].enabled = false;

        Invoke("RemoveObject", 3f);
    }
    void RemoveObject()
    {
        PoolManager.Instance.Put(gameObject);
    }
    void HitAniEffect(Collision collision)
    {
        PoolManager.Instance.Put(collision.gameObject);
        navMeshAgent.isStopped = true;
        animator.SetTrigger("IsHit");

        // 출혈 생성
        var blood = PoolManager.Instance.Get("Blood");
        blood.transform.parent = transform;
        blood.transform.position = collision.transform.position;
        blood.transform.rotation = Quaternion.identity;
        blood.SetActive(true);
    }
    void HitFromRayAniEffect(Vector3 hitPoint)
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("IsHit");

        // 출혈 생성
        var blood = PoolManager.Instance.Get("Blood");
        blood.transform.parent = transform;
        blood.transform.position = hitPoint;
        blood.transform.rotation = Quaternion.identity;
        blood.SetActive(true);
    }
    void DrawHpBar()
    {
        hpBar.fillAmount = (float)hp / maxHp;
        hpBar.color = new Color(1 - hpBar.fillAmount, hpBar.fillAmount, 0);
    }
}