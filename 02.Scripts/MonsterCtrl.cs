using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public Animator animator; // 애니메이터
    public NavMeshAgent navMeshAgent; // 플레이어 추적
    public Transform monsterTr;
    public float traceDist; // 추적 범위
    public float attackDist; // 공격 범위

    [SerializeField]
    int damage;

    Transform playerTr;
    GetDamage getDamage;

    void Start()
    {
        getDamage = GetComponent<GetDamage>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (getDamage.isDie) return;

        float dist = Vector3.Distance(playerTr.position, monsterTr.position);
        if (dist <= attackDist)
        {
            animator.SetBool("IsAttack", true);
            navMeshAgent.isStopped = true; // 공격 시에는 추적 중지
        }
        else if (dist <= traceDist)
        {
            navMeshAgent.isStopped = false; // 추적 중에는 활성화
            navMeshAgent.destination = playerTr.position; // 추적 대상은 플레이어

            animator.SetBool("IsAttack", false);
            animator.SetBool("IsTrace", true);
        }
        else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("IsTrace", false);
        }
    }

    public int GetDamage() { return damage; }
}
