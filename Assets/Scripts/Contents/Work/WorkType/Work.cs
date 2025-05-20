using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Work : MonoBehaviour
{
    public bool IsWorking { get; protected set; }
    public float WorkRange { get; protected set; }
    public Define.Worker Worker { get; protected set; }

    public void AarrivedWork(Define.Worker worker)
    {
        Worker = worker;
        IsWorking = true;
    }

    public virtual IEnumerator Co_WorkRoutine()
    {
        // Todo : ���� Work���� �ؾ��� Routine�� �ڽ� Ŭ������ override�ؾ���.
        yield break;
    }

    public IEnumerator Co_CheckIsWorking(IEnumerator employeeEscapeRoutine, IEnumerator moveToWorkRoutine)
    {
        while (true)
        {
            if (IsWorking == true)
            {
                CoroutineHelper.MyStopCoroutine(this, moveToWorkRoutine);

                if(Worker == Define.Worker.Player)
                {
                    StartCoroutine(employeeEscapeRoutine);
                }

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
            // Todo : �˹ٵ� ȸ�� �־����
            if ((transform.position - employeeTransform.position).magnitude <= WorkRange)
            {
                AarrivedWork(Define.Worker.Employee);
                StartCoroutine(this.Co_WorkRoutine());
                yield break;
            }

            yield return null;
        }
    }
}
