using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class CameraState : State
{
    protected new Camera stateHaver { get; set; }
    protected virtual Transform target { get; set; }

    protected virtual Vector3 cameraPos
    {
        get { return stateHaver.transform.position; }
    }

    protected virtual Vector3 targetPos
    {
        get { return target.position; }
    }

    public CameraState(Camera cam, Transform target)
    {
        stateHaver = cam;
        this.target = target;
    }
	
}
