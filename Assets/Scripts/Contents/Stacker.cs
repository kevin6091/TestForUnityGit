using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    private Stack<GameObject> _stack = new Stack<GameObject>();

    /* Local Space Offsets */
    [SerializeField]
    private Vector3 _offsetFromParent = Vector3.zero;

    public Transform LeftHandSocket { get; private set; } = null;
    public Transform RightHandSocket { get; private set; } = null;
    private Define.ItemType ItemType { get; set; } = Define.ItemType.END;

    private StackerState _state = StackerState.IDLE;

    public Action popEvent;
    public Action pushEvent;
    public Action emptyEvent;
    
    public StackerState State 
    { 
        get { return _state; }
        set
        {
            _state = value;
            switch(_state)
            {
                case StackerState.IDLE:

                    break;
                case StackerState.Lean:

                    break;
            }
        }
    }

    public int Count { get { return _stack.Count; } }
    public bool IsEmpty { get { return Count == 0; } }

    public enum StackerState
    {
        IDLE,
        Lean, 
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).name == "LeftHandSocket")
                LeftHandSocket = transform.GetChild(i);
            else if (transform.GetChild(i).name == "RightHandSocket")
                RightHandSocket = transform.GetChild(i);
        }
    }

    private void Start()
    {
        _offsetFromParent.y = 0.1f;
    }

    private void Update()
    {
    }

    public bool Push(GameObject gameObject)
    {
        if (null == gameObject)
        {
            return false;
        }

        Define.ItemType newItemType = Managers.Item.GetType(gameObject);
        if (!IsEmpty &&
            newItemType != ItemType)
        {
            return false;
        }

        ItemType = newItemType;

        Transform parent = transform;
        if (!IsEmpty)
        {
            parent = _stack.Peek().transform;
        }

        gameObject.transform.SetParent(parent, true);
        //  TODO: Scale 깨지는 버그있음 이유 모름 스케일따로 안건드는데
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(Co_MoveToStackingPosition(gameObject));
        StartCoroutine(Co_Lean(Quaternion.identity, 0.1f, 0f));

        _stack.Push(gameObject);
        pushEvent?.Invoke();
        return true;
    }

    public GameObject Pop(Transform parent = null) 
    {
        if(IsEmpty)
        {
            return null;
        }

        GameObject gameObject = _stack.Pop();
        popEvent?.Invoke();
        gameObject.transform.parent = parent;

        if (IsEmpty)
        {
            emptyEvent?.Invoke();
        }

        return gameObject;
    }


    public IEnumerator Co_Lean(Quaternion targetRotation, float time, float waitTime)
    {
        if(waitTime > 0f)
            yield return new WaitForSeconds(waitTime);

        Quaternion minRotate = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        GameObject[] objects = _stack.ToArray();
        Quaternion[] startRotates = new Quaternion[objects.Length];
        Quaternion[] targetRotates = new Quaternion[objects.Length];

        for(int i = 0; i < objects.Length; ++i)
        {
            startRotates[i] = objects[i].transform.localRotation;
            objects[i].transform.localRotation = Quaternion.identity;
            targetRotates[i] = Quaternion.Slerp(minRotate, targetRotation, 10f / i);

        }

        float accTime = 0f;

        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / time, 1f);


            for (int i = 0; i < objects.Length; ++i)
            {
                objects[i].transform.localRotation = Quaternion.Slerp(startRotates[i], targetRotates[i], ratio);
            }

            if (ratio >= 1f - Mathf.Epsilon)
                yield break;

            yield return null;
        }
    }

    private IEnumerator Co_MoveToStackingPosition(GameObject gameObject)
    {
        float accTime = 0f;
        float maxTime = 0.2f;

        Vector3 startLocalPos = gameObject.transform.localPosition;
        Vector3 additionalForce = new Vector3(0f, 2f, 0);

        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / maxTime, 1f);

            Vector3 curAdditonalForce = additionalForce * Mathf.Sin(ratio * Mathf.Deg2Rad * 180f);
            gameObject.transform.localPosition = Vector3.Lerp(startLocalPos, _offsetFromParent, ratio) + curAdditonalForce;

            if (ratio >= 1f - Mathf.Epsilon)
                yield break;

            yield return null;
        }
    }

    //  Todo : Player의 경우에만 충돌로인한 오브젝트와의 협업이 이루어져야함
    //float lastPop = 0f;
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Table")
    //    {
    //        if (lastPop + 0.1f >= Time.time)
    //            return;
    //        other.gameObject.GetComponent<Stacker>().Push(Pop());
    //        lastPop = Time.time;
    //    }
    //}
}
