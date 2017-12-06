using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class TPCameraMode : TPCameraState
{
    Vector3 camPos
    {
        get { return tpCam.transform.position; }
        set { tpCam.transform.position = value; }
    }
    public TPCameraMode(ThirdPersonCamera tpCam) : base(tpCam) { }
    public override void Enter()
    {
        //throw new NotImplementedException();
    }

    public override void Execute()
    {
        //throw new NotImplementedException();
        HandleMovementControls();
    }

    public override void Exit()
    {
        //throw new NotImplementedException();
    }
    
	void HandleMovementControls()
    {
        // move forward, backward, left, right
        if (Input.GetKey(KeyCode.W))
            camPos = camPos + (Vector3.forward * tpCam.moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            camPos = camPos + (Vector3.left * tpCam.moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            camPos = camPos + (-Vector3.forward * tpCam.moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            camPos = camPos + (-Vector3.left * tpCam.moveSpeed * Time.deltaTime);
    }
}
