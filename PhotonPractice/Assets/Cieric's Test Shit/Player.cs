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
    [SerializeField] GameObject CameraDolly;
    Transform CameraDollyTransform;

    float distToGround;

    //Vector2 lastMousePosition;
    Vector2 tempRotation = new Vector2();

    void Start()
    {
        distToGround = collider.bounds.extents.y;
        CameraDollyTransform = CameraDolly.transform;
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
        bool moved = false;

        //if(Input.GetMouseButtonDown(1))
        //    lastMousePosition = Input.mousePosition;
        if(Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            tempRotation += mouseMovement;
            // Debug.Log(tempRotation);
            CameraDollyTransform.rotation = new Quaternion();
            //Vector3 temp = CameraDollyTransform.rotation.eulerAngles;
            //CameraDollyTransform.rotation = Quaternion.Euler(temp.x, temp.y, 0);
            CameraDollyTransform.Rotate( 0, tempRotation.x, 0 );
            CameraDollyTransform.Rotate( -tempRotation.y, 0, 0 );
            //lastMousePosition = Input.mousePosition;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, MoveSpeed * Time.deltaTime);
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -MoveSpeed * Time.deltaTime);
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
            moved = true;
        }
            //transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
            moved = true;
        }
            //transform.position -= new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rigidbody.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);
            //moved = true;
        }

        if(moved || Input.GetKeyDown(KeyCode.E))
        {
            Vector3 temp = CameraDollyTransform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, temp.y, 0);
            tempRotation.x = 0;
        }

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
