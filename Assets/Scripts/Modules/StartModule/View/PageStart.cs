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

	// Use this for initialization
	void Start () {
		PoolManager.GetComponent<UIEventListener>(btnHostGame).onClick = OnClickbtnHostGame;
		PoolManager.GetComponent<UIEventListener>(btnJoinGame).onClick = OnClickbtnJoinGame;
	}

	void OnClickbtnHostGame (GameObject go)
	{
	}

	void OnClickbtnJoinGame (GameObject go)
	{
	}

	void OnClickbtnSingleGame (GameObject go)
	{
	}
}
