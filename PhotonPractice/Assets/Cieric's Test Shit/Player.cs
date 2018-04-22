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
    [SerializeField] float RotSpeed = 1;
    [SerializeField] float JumpSpeed = 400;
    [SerializeField] GameObject CameraDolly;
    Transform CameraDollyTransform;

    float distToGround;

    //Vector2 lastMousePosition;
    Vector2 tempRotation = new Vector2();
    PhotonView photonView;
    void Start()
    {
        distToGround = collider.bounds.extents.y;
        CameraDollyTransform = CameraDolly.transform;
        photonView = GetComponent<PhotonView>();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Update()
    {
        if(photonView.isMine)
            return;

        bool bs = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.E);
        //
        if (bs)
        {
            Vector3 temp = CameraDollyTransform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, temp.y, 0);
            tempRotation.x = 0;
            CameraDollyTransform.rotation = new Quaternion();
            CameraDollyTransform.Rotate(0, tempRotation.x, 0);
            CameraDollyTransform.Rotate(-tempRotation.y, 0, 0);
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            tempRotation += mouseMovement * RotSpeed * Time.deltaTime;
            CameraDollyTransform.rotation = new Quaternion();
            CameraDollyTransform.Rotate(0, tempRotation.x, 0);
            CameraDollyTransform.Rotate(-tempRotation.y, 0, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            tempRotation.x = 0;
        }

        Vector2 move =
            new Vector2((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0),
                        (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0)).normalized
                        * MoveSpeed * Time.deltaTime;

        transform.Translate(move.x, 0, move.y);

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            rigidbody.AddForce(new Vector3(0, JumpSpeed, 0), ForceMode.Impulse);

        if (Input.GetKey(KeyCode.LeftShift))
            rigidbody.AddForce(new Vector3(0, -JumpSpeed, 0), ForceMode.Impulse);

        if (Input.GetKeyDown(KeyCode.E))
        {

            //GameObject nObj = (GameObject)Instantiate(throwable, transform.position, transform.rotation);
            GameObject nObj = PhotonNetwork.Instantiate(throwable.name,  transform.position, transform.rotation, 0);
            nObj.transform.Translate(0, 0.5f, 1.0f);
            nObj.transform.rotation = Random.rotationUniform;
            var rb = nObj.GetComponent<Rigidbody>();
            var dir = nObj.transform.position - transform.position;
            rb.angularVelocity = new Vector3(Random.value * 2.0f - 1f, 0, Random.value * 2.0f - 1f);
            dir.y = 0;
            dir.Normalize();
            rb.AddForce(new Vector3(dir.x, 1, dir.z) * 10, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Capsule(Clone)")
        {
            other.gameObject.GetComponent<Rigidbody>()
            .AddExplosionForce(100, transform.position, 1.0f, 1.0f, ForceMode.Impulse);
        }
    }
}
