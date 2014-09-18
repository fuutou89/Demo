using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : BaseManager 
{
	protected override void OnInstanceMultiple ()
	{
		Debug.LogError ("------ Multiple instances of PoolManager ------");
	}

	public static T GetComponent<T>(GameObject go)
	{
		object t = go.GetComponent(typeof(T));
		if(t == null)
		{
			t = go.AddComponent(typeof(T));
		}
		return (T)t;
	}

	public static T GetComponent<T>(Component component)
	{
		return GetComponent<T>(component.gameObject);
	}
}
