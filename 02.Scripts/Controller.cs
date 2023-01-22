using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(GetDamage))]
public class Controller : MonoBehaviour
{
    public Animator animator; // 애니메이터
    public NavMeshAgent navMeshAgent; // 플레이어 추적
    public Transform thisObjTr;
    public float traceDist; // 추적 범위
    public float attackDist; // 공격 범위

    Transform playerTr;
    NavMeshPath _navMeshPath;

    public GetDamage getDamage { get; private set; }

    void Awake()
    {
        getDamage = GetComponent<GetDamage>();
        _navMeshPath = new NavMeshPath();
    }

    void OnEnable()
    {
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (getDamage.isDie) return;

        navMeshAgent.destination = playerTr.position; // 추적 대상은 플레이어
        float dist = Vector3.Distance(playerTr.position, thisObjTr.position);
        if (dist <= attackDist)
        {
            navMeshAgent.isStopped = true; // 공격 시에는 추적 중지
            animator.SetBool("IsAttack", true);
        }
        else
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("IsTrace", true);
            animator.SetBool("IsAttack", false);
        }
    }

    bool IsReachableToPlayer()
    {
        return navMeshAgent.SetPath(_navMeshPath) && _navMeshPath.status != NavMeshPathStatus.PathComplete;
    }
}
