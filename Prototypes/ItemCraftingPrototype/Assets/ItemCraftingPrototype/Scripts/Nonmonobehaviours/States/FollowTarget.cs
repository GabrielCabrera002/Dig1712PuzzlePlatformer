using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class FollowTarget : TPCameraState
{
    protected Vector3 destination
    {
        get { return targetPos - offset; }
    }
    public FollowTarget(ThirdPersonCamera tpCam) : base(tpCam)
    {

    }

    public override void Enter()
    {
        //throw new NotImplementedException();
    }

    public override void Execute()
    {
        // do the target-following
        stateHaver.transform.position = Vector3.Lerp(cameraPos, destination, easing);
    }

    public override void Exit()
    {
        //throw new NotImplementedException();
    }
	
}
