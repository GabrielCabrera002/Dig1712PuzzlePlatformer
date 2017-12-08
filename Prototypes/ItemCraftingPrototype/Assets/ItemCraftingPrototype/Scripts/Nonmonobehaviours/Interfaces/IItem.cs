using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IItem : System.IEquatable<IItem>
{
    string name { get; }
    string description { get; }
    Sprite sprite { get; }
    Mesh mesh { get; }
    List<Category> categories { get; }
    Effect effect { get; }
	
}
