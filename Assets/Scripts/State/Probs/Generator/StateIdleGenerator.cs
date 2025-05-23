using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StateIdleGenerator : StateGenerator
{
    private Coroutine CurrentCoroutine = null;
    public StateIdleGenerator(StateMachine stateMachine, MonoBehaviour context) : base(stateMachine, context)
    {}
    public override void Enter()
    {
        base.Enter();
        CurrentCoroutine = Context.StartCoroutine(GenerateFood());
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();

        Context.StopCoroutine(CurrentCoroutine);
        CurrentCoroutine = null;
    }

    private IEnumerator GenerateFood()
    {
        while(true)
        {
            yield return new WaitForSeconds(Context.GenerateSpeed);
            
            GameObject gameObject = Managers.Item.CreateItem(Define.ItemType.Pizza).gameObject;
            gameObject.transform.position = Context.transform.position;
            Context.Stacker.Push(gameObject);

            if (Context.Stacker.Count >= Context.MaxCount)
            {
                Context.State = Define.State.Wait;
            }
        }

    }
}
