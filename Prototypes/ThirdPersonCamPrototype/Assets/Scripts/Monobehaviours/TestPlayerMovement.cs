using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestPlayerMovement : MonoBehaviour 
{
    public float moveSpeed = 10;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        HandleTestControls();
	}

    void HandleTestControls()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position = transform.position + (transform.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.position = transform.position + (-transform.right * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.position = transform.position + (-transform.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.position = transform.position + (transform.right * moveSpeed * Time.deltaTime);
    }
	
}
