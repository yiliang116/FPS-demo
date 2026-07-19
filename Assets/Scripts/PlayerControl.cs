using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

// 这里的类名必须和外面的文件名一模一样，且必须继承 MonoBehaviour
public class PlayerControl : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public float yScensitivity = 10;
    public float xScensitivity = 10;
    [HideInInspector]
    public bool highSpeed = false;
    [HideInInspector]
    public bool isAiming = false;
    private float xRotation = 0;
    private Rigidbody rb;
    private Animator ani;
    private Vector3 velocity;
    private bool jump = false;
    
    
    void Start()
    {
        // 游戏刚开始时会执行这里的代码
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Aim();
        Mouse();
        // 游戏每一帧都会反复执行这里的代码（用来写移动逻辑）
        HighSpeed();
        Move();
        Jump();
    }

    void Mouse()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        
        // 上下旋转
        xRotation -= y * yScensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        ani.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        // 左右旋转
        transform.Rotate(Vector3.up * x * xScensitivity);


    }
    void Aim()
    {
        float aim = ani.GetFloat("Aiming");
        if(Input.GetMouseButton(1))
        {
            isAiming = true;
            ani.SetBool("Aim", true);
            ani.SetFloat("Aiming",Mathf.Lerp(aim, 1, 0.1f));
        }
        else
        {   
            isAiming = false;
            ani.SetBool("Aim", false);
            ani.SetFloat("Aiming", Mathf.Lerp(aim, 0, 0.1f));
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = (transform.forward * vertical + transform.right * horizontal).normalized;
        velocity = dir * speed;
        velocity.y = rb.linearVelocity.y;
        ani.SetFloat("Movement",dir.magnitude);
    }

    void HighSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift) )
        {
            highSpeed = true;
            speed = 5f;
            ani.SetBool("Holstered",true);
        }
        else
        {
            highSpeed = false;
            speed = 3f;
            ani.SetBool("Holstered",false);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            jump = true;
        }
    }

    public bool IsGround()
    {
        RaycastHit hit;
        bool res = Physics.Raycast(transform.position + Vector3.up * 0.2f, 
        -Vector3.up, out hit, 0.4f, LayerMask.GetMask("Ground"));
        return res;
    }
    private void FixedUpdate()
    {
        if(jump)
        {
            jump = false;
            velocity.y = jumpForce;
        }
        rb.linearVelocity = velocity;
    }
}