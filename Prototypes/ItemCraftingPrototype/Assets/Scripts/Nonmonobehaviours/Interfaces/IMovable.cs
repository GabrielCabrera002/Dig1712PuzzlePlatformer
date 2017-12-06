using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IMovable 
{
	Transform transform { get; }
    Renderer renderer { get; }
    Rigidbody rigidbody { get; }

    void Move();
}
