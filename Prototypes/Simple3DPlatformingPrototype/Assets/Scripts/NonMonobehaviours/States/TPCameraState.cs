using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class TPCameraState : CameraState 
{
    // Superclass of camera states for third-person cameras
    protected ThirdPersonCamera tpCam;
    protected virtual Vector3 offset { get { return tpCam.offset; } }
    protected virtual float easing { get { return tpCam.easing; } }
    protected override Transform target
    {
        get{ return tpCam.target; }
    }

    public TPCameraState(ThirdPersonCamera tpCam) : base(tpCam.camera, tpCam.target)
    {
        this.tpCam = tpCam;
    }
	
}
