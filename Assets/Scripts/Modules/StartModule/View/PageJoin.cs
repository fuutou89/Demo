using UnityEngine;
using System.Collections;

public class PageJoin : MonoBehaviour 
{
	public UIButton btnRefreshHost;
	public UILabel txtHostlist;
	public GameObject HostUnitPrefab;
	public UIGrid grid;

	void Start()
	{
		PoolManager.GetComponent<UIEventListener>(btnRefreshHost).onClick = OnClickbtnRefreshHost;
	}

	void OnClickbtnRefreshHost (GameObject go)
	{
		foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
		{
			GameObject hgo = NGUITools.AddChild(grid.gameObject, HostUnitPrefab);
			HostUnit unit = hgo.GetComponent<HostUnit>();
			unit.UpdateHostUnit(roomInfo);
		}
		grid.Reposition();
	}
}
