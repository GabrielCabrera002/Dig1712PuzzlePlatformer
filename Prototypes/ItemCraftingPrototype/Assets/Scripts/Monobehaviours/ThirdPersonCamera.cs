using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TPCameraStates
{
    // cached instances of the states this camera can execute
    public FollowTarget followTarget;
    public TPCameraMode cameraMode;
    public TPCameraStates(ThirdPersonCamera cam)
    {
        followTarget = new FollowTarget(cam);
        cameraMode = new TPCameraMode(cam);
    }
}
public class ThirdPersonCamera : MonoBehaviour
{
    /*
     * Makes the camera this is attached to behave like a third person 
     * camera, as per the puzzle platformer's design.
     */

    #region Fields
    public static ThirdPersonCamera instance;
    [SerializeField]
    Vector3 _offset;
    [SerializeField]
    float _easing = 0.1f;
    [SerializeField]
    Transform _target;
    [SerializeField]
    float _moveSpeed = 10f;
    [SerializeField]
    float _stateSwitchBuffer = 2f;
    // ^ Cooldown period between state changes so you can't just spam it

    CameraState _currentState = null;
    Camera _camera;
    TPCameraStates states;
    IEnumerator stateSwitchCoroutine;
    #endregion

    #region Properties
    public CameraState currentState
    {
        get { return _currentState; }
        protected set { _currentState = value; }
    }
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

    public float moveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public float stateSwitchBuffer
    {
        get { return _stateSwitchBuffer; }
        set { _stateSwitchBuffer = value; }
    }

    public Camera camera
    {
        get { return _camera; }
    }

    #endregion

    #region Methods
    private void Awake()
    {
        instance = this;
        _camera = GetComponent<Camera>();
        states = new TPCameraStates(this);
        ChangeState(states.followTarget);
        stateSwitchCoroutine = HandleManualStateChanges();
        StartCoroutine(stateSwitchCoroutine);
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

    IEnumerator HandleManualStateChanges()
    {
        yield return null;
        float frameRate = 1f / Time.deltaTime;
        float timer = 0f;
        float framesToWait = _stateSwitchBuffer * frameRate;

        while (true)
        {
            // right click to change camera state
            if (timer >= framesToWait && Input.GetMouseButtonDown(1))
            {
                SwitchCameraState();
                timer = 0f;
            }

            // keep timer from potentially looping around the float type limits
            else if (timer < framesToWait)
                timer++;

            yield return null;

        }
        
    }

    void SwitchCameraState()
    {
        if (currentState == states.followTarget)
        {
            Debug.Log("Changing state to Camera Mode!");
            ChangeState(states.cameraMode);
        }
        else if (currentState == states.cameraMode)
        {
            ChangeState(states.followTarget);
            Debug.Log("Changing state to Follow Target!");
        }
    }
    #endregion
}
