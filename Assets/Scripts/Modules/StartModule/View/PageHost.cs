using UnityEngine;
using System.Collections;

public class PageHost : MonoBehaviour 
{
	public UIButton btnHostGame;
	public UIInput inputRoomName;

	// Use this for initialization
	void Start () {
		PoolManager.GetComponent<UIEventListener>(btnHostGame).onClick = OnClickbtnHostGame;
	}


	void OnClickbtnHostGame (GameObject go)
	{
		if(inputRoomName.value != "")
		{
			NetworkManager.CreateRoom(inputRoomName.value);
		}
	}
}
