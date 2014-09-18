using UnityEngine;
using System.Collections;

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

	void OnClickbtnHostGame (GameObject go)
	{
	}

	void OnClickbtnJoinGame (GameObject go)
	{
	}

	void OnClickbtnSingleGame (GameObject go)
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
