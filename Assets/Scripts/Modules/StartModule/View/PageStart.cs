using UnityEngine;
using System.Collections;
using Core.Manager;
using PureMVC.Patterns;
using System.IO;
using System;

public class PageStart : MonoBehaviour 
{
	public UIButton btnHostGame;
	public UIButton btnJoinGame;
	public UIButton btnSingleGame;

	// Use this for initialization
	void Start () {
		PoolManager.GetComponent<UIEventListener>(btnHostGame).onClick = OnClickbtnHostGame;
		PoolManager.GetComponent<UIEventListener>(btnJoinGame).onClick = OnClickbtnJoinGame;
		PoolManager.GetComponent<UIEventListener>(btnSingleGame).onClick = OnClickbtnSingleGame;
	}

	void _OnTest (object sender, System.EventArgs e)
	{
		Debug.Log ("------ _OnTest ------");
	}

	void OnClickbtnHostGame (GameObject go)
	{
		EventManager.instance.DispatchEvent(EventManager.instance, "TEST");
	}

	void OnClickbtnJoinGame (GameObject go)
	{
	}

	void OnClickbtnSingleGame (GameObject go)
	{
	}
}
