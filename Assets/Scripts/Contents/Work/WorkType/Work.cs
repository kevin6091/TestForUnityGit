using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Work : MonoBehaviour
{
    protected bool _isWorking;
    public bool IsWorking { get; protected set; }

    protected float _workRange;
    public float WorkRange { get; protected set; }

    public virtual IEnumerator Co_WorkRoutine()
    {
        // Todo : 각자 Work에서 해야할 Routine을 자식 클래스가 override해야함.
        yield break;
    }

    public IEnumerator Co_CheckIsWork(IEnumerator employeeEscapeRoutine)
    {
        while (true)
        {
            if (IsWorking == true)
            {
                StopAllCoroutines();
                StartCoroutine(employeeEscapeRoutine);

                yield break;
            }

            yield return null;
        }
    }

    public IEnumerator Co_MoveToWorkRoutine(Transform employeeTransform, float moveSpeed)
    {
        Vector3 targetDir = (transform.position - employeeTransform.position).normalized;

        while (true)
        {
            employeeTransform.position += targetDir * moveSpeed * Time.deltaTime;

            if ((transform.position - employeeTransform.position).magnitude <= WorkRange)
            {
                IsWorking = true;
                StartCoroutine(this.Co_WorkRoutine());
                yield break;
            }

            yield return null;
        }
    }
}
