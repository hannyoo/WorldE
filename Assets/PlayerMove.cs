using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed = 10f;
    public float jumpHeight = 10f;
    public float dash = 5f;
    public float rotSpeed = 3f;

    private Vector3 dir = Vector3.zero;
    private bool ground = false;
    public LayerMask layer;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        dir.Normalize(); //대각선으로 이동해도 속도 일정(빨라지지 않음)

        ChcekGround();

        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = this.transform.forward * dash;
            rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }




    }

    private void fixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            //바라보는 방향 부호가 반대일때 회전
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir,
            rotSpeed * Time.deltaTime);
        }

        rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    void ChcekGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }



}
