using UnityEngine;
using System.Collections;

public class PageJoin : MonoBehaviour 
{
	public UIInput inputservername;
	public UIInput inputportname;
	public UILabel txtHostName;

	public UIButton btnJoin;

	void Start()
	{
		PoolManager.GetComponent<UIEventListener>(btnJoin).onClick = OnClickbtnJoin;
	}

	void OnClickbtnJoin (GameObject go)
	{
		//int port = int.Parse(inputportname.value);
		//NetworkManager.Instance.JoinServer(inputservername.value, 8080);
		HostData data = NetworkManager.Instance.hostList[0];
		NetworkManager.Instance.JoinServer(data);
	}
}
