using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] new Transform transform;
    [SerializeField] new Collider collider;
    [SerializeField] GameObject throwable;
    [SerializeField] float MoveSpeed = 1;
    [SerializeField] float JumpSpeed = 400;

    float distToGround;


    void Start()
    {
        distToGround = collider.bounds.extents.y;
    }

    void Update()
    {

    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, 1, 0);
            //transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -1, 0);
            //transform.position -= new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            rigidbody.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject nObj = (GameObject)Instantiate(throwable, transform.position, transform.rotation);
            nObj.transform.Translate(0, 0.5f, 1.0f);
            var rb = nObj.GetComponent<Rigidbody>();
            var dir = nObj.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            rb.AddForce(new Vector3(dir.x, 1, dir.z) * 10, ForceMode.Impulse);
        }
    }
}
