using UnityEngine;
using System.Collections;

public class PageHost : MonoBehaviour 
{
	public UIButton btnHostGame;
	public UILabel txtHostInfo;

	// Use this for initialization
	void Start () {
		PoolManager.GetComponent<UIEventListener>(btnHostGame).onClick = OnClickbtnHostGame;
	}


	void OnClickbtnHostGame (GameObject go)
	{
		NetworkManager.Instance.StartServer(25000);
	}
}
