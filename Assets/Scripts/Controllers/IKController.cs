using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IKController : MonoBehaviour
{
    Animator _animator = null;
    public Transform LeftHandTarget { get; set; } = null;
    public Transform RightHandTarget { get; set; } = null;

    public float IK_Weight { get; set; } = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.Log("Animator is null : IKController");
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (null == _animator)
            return;

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);

        _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
    }
}
