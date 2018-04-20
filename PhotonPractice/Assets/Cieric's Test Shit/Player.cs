using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    new Rigidbody rigidbody;

	void Start ()
    {
		
	}

	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(0, 0, 10);
        }
    }
}
