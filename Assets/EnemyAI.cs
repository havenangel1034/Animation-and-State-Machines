using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform partrolPoint;
    private NavMeshAgent ai;
    public enum EnemyState { Idle, Walk, Run, Attack }
    public EnemyState enemyState;
    private Animator anim;
    private float distanceToTarget;
    Coroutine idleToPatrol;


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
        
    }

    IEnumerator SwitchToPatrol()
    {
        yield return new WaitForSeconds(5);
        enemyState = EnemyState.Walk;
        idleToPatrol = null;
    }

    private void SwitchToState(int newState)
    {
        if (anim.GetInteger("State") != newState)
            anim.SetInteger("State", newState);
    }
}
