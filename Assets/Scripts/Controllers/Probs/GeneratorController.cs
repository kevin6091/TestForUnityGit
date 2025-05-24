using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratorController : ProbController
{
    public Stacker Stacker { get; private set; } = null;
    public float GenerateSpeed { get; set; } =  5f;
    public int MaxCount { get; set; } = 6;

    private Coroutine _Coroutine = null;
    public override void Init()
    {
        base.Init();

        Stacker = GetComponentInChildren<Stacker>();
        StateMachine.RegisterState<StateIdleGenerator>(Define.State.Idle, this);
        StateMachine.RegisterState<StateWaitGenerator>(Define.State.Wait, this);
        State = Define.State.Idle;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerController _PlayerController = other.gameObject.GetComponentInChildren<PlayerController>();
            _Coroutine = StartCoroutine(SendItemToPlayer(_PlayerController));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine(_Coroutine);
            _Coroutine = null;
        }
    }

    private IEnumerator SendItemToPlayer(PlayerController p)
    {
        while (true) {
            yield return null;

            if (Stacker.IsEmpty)    // Stacker is empty
            {
                continue;
            }

            if (!p._stacker.IsEmpty && (Stacker.ItemType != p._stacker.ItemType || p.MaxCount <= p._stacker.Count)) 
                // Cannot push Item to Player
            {
                continue;
            }

            GameObject gameObject = Stacker.Pop();
            if (gameObject != null)
            {
                p._stacker.Push(gameObject);
                yield return new WaitForSeconds(0.1f);
            }

        }
    }


    #region For Future. Upgrade
    private void Upgrade()
    {

    }
    private void ChangeModel()
    {
    }
    #endregion
}
