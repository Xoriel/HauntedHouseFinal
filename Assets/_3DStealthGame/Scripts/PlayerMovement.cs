using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    public InputAction MoveAction;
    public InputAction SprintAction;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    public float sprintSpeed = 2.5f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody> ();
        MoveAction.Enable();
        m_Animator = GetComponent<Animator>();
        SprintAction.Enable();
    }

    void OnDisable()
    {
        MoveAction.Disable();
        SprintAction.Disable();
    }

     void FixedUpdate ()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        
        float horizontal = pos.x;
        float vertical = pos.y;
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        bool isSprinting = SprintAction.IsPressed() && isWalking;
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        m_Animator.SetBool ("IsWalking", isWalking);

        if(isWalking)
       { 
            Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
             m_Rotation = Quaternion.LookRotation (desiredForward);
        
            m_Rigidbody.MoveRotation (m_Rotation);
            m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * currentSpeed * Time.deltaTime);
       }
    }


}