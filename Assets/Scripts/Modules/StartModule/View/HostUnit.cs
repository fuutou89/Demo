using UnityEngine;
using System.Collections;

public class HostUnit : MonoBehaviour 
{
	public UIButton btnJoinHost;
	public UILabel txtHostName;
	private RoomInfo _roominfo;

	void Start()
	{
		PoolManager.GetComponent<UIEventListener>(btnJoinHost).onClick = OnClickbtnJoinHost;
	}

	void OnClickbtnJoinHost (GameObject go)
	{
		if(_roominfo != null)
		{
			NetworkManager.JoinRoom(_roominfo.name);
		}
	}

	public void UpdateHostUnit(RoomInfo info)
	{
		if(_roominfo != info)
		{
			_roominfo = info;
			if(_roominfo != null)
			{
				txtHostName.text = _roominfo.name;
			}
		}
	}
}
