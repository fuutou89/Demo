using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> 
{
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

	public void AddView(string name)
	{
		GameObject go = Get(name, Scene.instance.uiLayer);
		BaseView view = go.GetComponent<BaseView>();
		viewDic.Add(name, view);
	}

	public void RemoveView(string name)
	{
		GameObject go = viewDic[name].gameObject;
		Recycle(go);
		viewDic.Remove(name);
	}

	public BaseView GetView(string name)
	{
		if(viewDic.ContainsKey(name)) return viewDic[name];
		else return null;
	}

	public static GameObject Get(string name, Transform parent)
	{
		return Get(name, parent, Vector3.zero, Quaternion.identity);
	}

	public static GameObject Get (string name, Transform parent, Vector3 position, Quaternion rotation)
	{
		Object ro = Resources.Load(name);
		GameObject go = Instantiate(ro) as GameObject;
		if (go != null) {
			go.transform.position = position;
			go.transform.rotation = rotation;
			go.transform.parent = parent;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
			go.SetActive (true);
		}
		return go;
	}

	public static void Recycle(GameObject go)
	{
		GameObject.Destroy(go);
	}

}
