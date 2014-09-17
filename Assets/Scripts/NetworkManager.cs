using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private NetworkPlayer nplayer = new NetworkPlayer();

	public HostData[] hostList;

	public void StartServer()
	{
		//MasterServer.ipAddress = nplayer.externalIP;
		Network.InitializeServer(4, nplayer.externalPort, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied" + MasterServer.ipAddress);
	}

	public void RefreshHostList()
	{
		MasterServer.ipAddress = nplayer.externalIP;
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		Debug.Log ("------ Get Server List ------");
		if(msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}
}
