using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class Effect
{
	string name;
	string description;

	public virtual void Apply(object target) {}
}
