using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float moveSpeed = 5f; // �ړ����x
    public float jumpForce = 5f; // �W�����v��
    public float rotationSpeed = 10f;  // ��]���x
    [SerializeField] private Transform groundCheck; // �ڒn����Ɏg����̃Q�[���I�u�W�F�N�g
    [SerializeField] private LayerMask groundLayer; // �ڒn����Ɏg�����C���[
    [SerializeField] private float groundCheckRadius = 0.2f; // �ڒn����̔��a

    private Rigidbody rb; // �v���C���[��Rigidbody
    private Vector2 moveInput; // �v���C���[�̈ړ�����
    private Vector3 moveDirection; // �L�����N�^�[�̈ړ�����
    private bool isJumping; // �W�����v��
    private bool isGrounded; // �ڒn��
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �ڒn����
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        // �ړ�����
        MovePlayer();

        // ��]����
        if (moveDirection.magnitude > 0.1f) // ���͂�����ꍇ
        {
            RotatePlayer();
        }

        // �W�����v����
        if (isGrounded && isJumping) //�ڒn�����W�����v���͂��������Ă���ꍇ
        {
            JumpPlayer();
        }
    }

    // �ړ�����
    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // ���͂Ɋ�Â��Ĉړ�������ݒ�
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    // ��]����
    private void RotatePlayer()
    { 
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up); // �X�e�B�b�N�̓��͕���������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime); // �X���[�Y�ɉ�]
    }

    //�W�����v����
    private void JumpPlayer()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // �W�����v�͂�Y�����ɉ�����
        isJumping = false; // �W�����v��������
        Debug.Log("Jump");
    }

    //�ڒn����
    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer); //�����̃Q�[���I�u�W�F�N�g��groundLayer�ɐڂ��Ă��邩���肷��
    }


    // ���̓C�x���g�F�ړ�
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // ���͂��ꂽ�ړ��x�N�g�����擾
        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y); // �ړ��x�N�g����3D�����ɕϊ�
        Debug.Log("Move Input: " + moveInput + " Direction:" + moveDirection);
    }

    // ���̓C�x���g�F�W�����v
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded) // �{�^���������ꂽ�u�Ԃɐڒn���ł���΃W�����v���͐���
        {
            isJumping = true; // �W�����v���ɂ���
            Debug.Log("Input:Jump");
        }
    }
}
