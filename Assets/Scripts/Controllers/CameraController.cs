using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Define.CameraMode _mode = Define.CameraMode.QuarterView;
    [SerializeField] Vector3 _delta = new Vector3();
    [SerializeField] GameObject _player = null;

    Camera _camera;
    Coroutine _coInterpolateFov;
    float _fov;
    void Start()
    {
        _camera = GetComponent<Camera>();
        if (null == _camera)
        {
            Debug.Log("Camera is Null : CameraController");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_coInterpolateFov != null)
            {
                StopCoroutine(_coInterpolateFov);
                _coInterpolateFov = null;
            }

            _coInterpolateFov = StartCoroutine(Co_InterpolateFov(90f, 3f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_coInterpolateFov != null)
            {
                StopCoroutine(_coInterpolateFov);
                _coInterpolateFov = null;
            }

            _coInterpolateFov = StartCoroutine(Co_InterpolateFov(30f, 3f));
        }
    }

    private void LateUpdate()
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
                transform.LookAt(_player.transform);
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }        
    }

    public void SetQuaterView(Vector3 delta)
    {

    }

    IEnumerator Co_InterpolateFov(float targetFov, float time)
    {

        float accTime = 0f;
        float startFov = _camera.fieldOfView;

        while (accTime < time)
        {
            accTime += Time.deltaTime;
            float ratio = Mathf.Min(accTime / time, 1f);

            float curFov = Mathf.Lerp(startFov, targetFov, ratio);
            _camera.fieldOfView = curFov;

            yield return null;
        }

        _coInterpolateFov = null;
        yield break;
    }
}
