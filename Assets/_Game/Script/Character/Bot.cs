using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;

    private Vector3 destionation;
    private IState<Bot> currentState;

    private void Start()
    {
        ChangeState(new PatrolState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
            CanMove(TF.position);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
    }
    internal void MoveStop()
    {
        agent.enabled = false;
        Debug.Log("asdasd");
    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
