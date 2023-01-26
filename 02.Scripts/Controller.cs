using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(GetDamage))]
public class Controller : CustomUpdateBehaviour
{
    [SerializeField] Animator _animator; // 애니메이터
    [SerializeField] NavMeshAgent _navMeshAgent; // 플레이어 추적
    [SerializeField] float _traceDist = 15f; // 추적 범위
    [SerializeField] float _attackDist = 2f; // 공격 범위

    Transform playerTr;

    public GetDamage getDamage { get; private set; }

    void Awake()
    {
        getDamage = GetComponent<GetDamage>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public override void CustomUpdate()
    {
        if (getDamage.isDie) return;

        _navMeshAgent.destination = playerTr.position; // 추적 대상은 플레이어
        float dist = Vector3.Distance(playerTr.position, transform.position);
        if (dist <= _attackDist)
        {
            _navMeshAgent.isStopped = true; // 공격 시에는 추적 중지
            _animator.SetBool("IsAttack", true);
        }
        else
        {
            _navMeshAgent.isStopped = false;
            _animator.SetBool("IsTrace", true);
            _animator.SetBool("IsAttack", false);
        }
    }
}
