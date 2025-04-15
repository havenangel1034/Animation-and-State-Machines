using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform patrolPoint;
    private NavMeshAgent ai;
    public enum EnemyState { Idle, Walk, Run, Attack }
    public EnemyState enemyState;
    private Animator anim;
    private float distanceToTarget;
    Coroutine idleToWalk;


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyState = EnemyState.Idle;
        distanceToTarget = Mathf.Abs(Vector3.Distance(target.position, transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        switch (enemyState)
        {
            case EnemyState.Idle:
                SwitchToState(0);

                ai.SetDestination(transform.position);

                if (idleToWalk == null)
                {
                    idleToWalk = StartCoroutine(SwitchToWalk());
                }

                break;
                case EnemyState.Walk:
                    float distancetoPatrolPoint = Mathf.Abs(Vector3.Distance(patrolPoint.position, transform.position));

                if (distancetoPatrolPoint > 2)
                {
                    SwitchToState(1);
                    ai.SetDestination(patrolPoint.position);
                }
                else
                {
                    SwitchToState(0);
                }

                if (distanceToTarget <= 15)
                {
                    enemyState = EnemyState.Run;
                }
                break;
                case EnemyState.Run:
                SwitchToState(2);

                ai.SetDestination(target.position);

                if (distanceToTarget <= 5)
                {
                    enemyState = EnemyState.Attack;
                }
                else if (distanceToTarget > 15)
                {
                    enemyState = EnemyState.Idle;
                }

                break;
                case EnemyState.Attack:
                SwitchToState(3);

                if (distanceToTarget > 5 && distanceToTarget <= 15)
                {
                    enemyState = EnemyState.Run;
                }
                else if (distanceToTarget > 15)
                {
                    enemyState = EnemyState.Idle;
                }

                break;
            default:

                break;
        }
    }

    IEnumerator SwitchToWalk()
    {
        yield return new WaitForSeconds(5);
        enemyState = EnemyState.Walk;
        idleToWalk = null;
    }

    private void SwitchToState(int newState)
    {
        if (anim.GetInteger("State") != newState)
            anim.SetInteger("State", newState);
    }
}
