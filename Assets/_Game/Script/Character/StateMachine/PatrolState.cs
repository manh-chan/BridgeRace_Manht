using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class PatrolState : IState<Bot>
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_RUN);
        targetBrick = Random.Range(2, 7);
        SeekTarget(t);
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDestination)
        {
            if (t.BrickCount >= targetBrick)
            {
                t.ChangeState(new IdelState());
            }
            else
            {
                SeekTarget(t);
               
            }
        }
    }

    public void OnExit(Bot t)
    {

    }
    private void SeekTarget(Bot t)
    {
        if (t.Stage != null)
        {
            Brick brick = t.Stage.SeekBrickPoint(t.colorType);

            if (brick == null)
            {
                t.ChangeState(new IdelState());
            }
            else
            {
                t.SetDestination(brick.TF.position);
            }
        }
        else
        {
            t.SetDestination(t.TF.position);
        }
    }
}
