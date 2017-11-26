using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TPCameraStates
{
    // cached instances of the states this camera can execute
    public FollowTarget followTarget;

    public TPCameraStates(ThirdPersonCamera cam)
    {
        followTarget = new FollowTarget(cam);
    }
}
public class ThirdPersonCamera : MonoBehaviour
{
    /*
     * Makes the camera this is attached to behave like a third person 
     * camera, as per the puzzle platformer's design.
     */

    #region Fields
    [SerializeField]
    Vector3 _offset;
    [SerializeField]
    float _easing = 0.1f;
    [SerializeField]
    Transform _target;
    CameraState currentState = null;
    Camera _camera;

    TPCameraStates states;
    #endregion

    #region Properties
    public float easing
    {
        get { return _easing; }
        protected set { _easing = value; }
    }
    public Transform target
    {
        get { return _target; }
        set { _target = value; }
    }

    public Vector3 offset
    {
        get { return _offset; }
        set { _offset = value; }
    }

    public Camera camera
    {
        get { return _camera; }
    }

    #endregion

    #region Methods
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        states = new TPCameraStates(this);
        ChangeState(states.followTarget);
    }

    // Use this for initialization
    void Start ()
    {
		
	}

    private void FixedUpdate()
    {
        currentState.Execute();
    }

    public void ChangeState(CameraState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }
    #endregion
}
