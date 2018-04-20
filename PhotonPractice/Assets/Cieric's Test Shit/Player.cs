using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    new Rigidbody rigidbody;

    [SerializeField]
    new Transform transform;

    [SerializeField]
    float CapSpeed = 1;

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(0, 0, 10);
            if(rigidbody.velocity.magnitude < CapSpeed)
                rigidbody.velocity = rigidbody.velocity.normalized * CapSpeed;
        }
        else if(rigidbody.velocity.magnitude < CapSpeed)
        {
            rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Vector3 rot = transform.rotation.eulerAngles;
            if(Input.GetKey(KeyCode.A))
                rot.y -= 1;
            if (Input.GetKey(KeyCode.D))
                rot.y += 1;
            transform.rotation = Quaternion.Euler(rot);
        }

    }
}
