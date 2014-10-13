using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : BaseManager 
{
	public static PoolManager Instance
	{
		get
		{
			return _instance as PoolManager;
		}
	}

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

	private Dictionary<string, BaseView> viewDic = new Dictionary<string, BaseView>();

	public bool stes;

	public void AddView(string name, BaseView view)
	{
		viewDic.Add(name, view);
	}

	public BaseView GetView(string name)
	{
		if(viewDic.ContainsKey(name)) return viewDic[name];
		else return null;
	}
}
