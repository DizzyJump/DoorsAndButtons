using System;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace CodeBase.UnityRelatedScripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] AnimatorController idleAnimation;
        [SerializeField] AnimatorController walkAnimation;

        Animator animator;
        Vector3 prevPosition;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            var currentPosition = transform.position;
            var direction = currentPosition - prevPosition;
            transform.LookAt(transform.position + direction, Vector3.up);
            if (direction.sqrMagnitude > float.Epsilon)
            {
                if (animator.runtimeAnimatorController != walkAnimation)
                    animator.runtimeAnimatorController = walkAnimation;
            }
            else
            {
                if (animator.runtimeAnimatorController != idleAnimation)
                    animator.runtimeAnimatorController = idleAnimation;
            }
            prevPosition = currentPosition;
        }
    }
}
