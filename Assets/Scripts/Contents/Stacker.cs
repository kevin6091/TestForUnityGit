using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    Stack<GameObject> _stack = new Stack<GameObject>();

    [SerializeField]
    private float Speed { get; set; } = 10f;

    /* Local Space Offsets */
    [SerializeField]
    private Vector3 _offsetFromParent = Vector3.zero;
    [SerializeField]
    private Vector3 _offsetFromObject = Vector3.zero;

    public Transform LeftHandSocket { get; private set; } = null;
    public Transform RightHandSocket { get; private set; } = null;

    List<GameObject> _pizzas = new List<GameObject>();
    private StackerState _state = StackerState.IDLE;
    
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
        _offsetFromObject = new Vector3(0f, 1f, 1f);

        for(int i = 0; i < 30; ++i)
            _pizzas.Add(Managers.Resource.Instantiate("Foods/Pizza"));
    }

    float accTime = 0f;

    private void Update()
    {
        accTime += Time.deltaTime;
        if (accTime >= 0.1f)
        {
            accTime -= 0.1f;
            if(_pizzas.Count > 0)
            {
                Push(_pizzas[_pizzas.Count - 1]);
                _pizzas.Remove(_pizzas[_pizzas.Count - 1]);
            }
        }
    }

    public void Push(GameObject gameObject)
    {
        if (null == gameObject)
            return;

        Transform parent = transform;
        if (_stack.Count > 0)
            parent = _stack.Peek().transform;

        gameObject.transform.parent = parent;
        StartCoroutine("Co_MoveToStackingPosition", gameObject);

        _stack.Push(gameObject);
    }

    public GameObject Pop(Transform parent = null) 
    {
        if(_stack.Count == 0) 
            return null;

        _stack.Peek().transform.parent = parent;
        return _stack.Pop();
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
        float maxTime = 1f;

        Vector3 startLocalPos = gameObject.transform.localPosition;
        Transform parentTransform = gameObject.transform.parent;

        while (true)
        {
            accTime += Time.deltaTime;
            float ratio = accTime / maxTime;    

            gameObject.transform.localPosition = Vector3.Lerp(startLocalPos, _offsetFromParent, ratio);           

            if(ratio >= 1f - Mathf.Epsilon)
                yield break;

            yield return null;
        }
    }
}
