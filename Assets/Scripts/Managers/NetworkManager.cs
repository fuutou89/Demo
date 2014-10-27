using UnityEngine;
using System.Collections;
using Core.Manager;

public class NetworkManager : Singleton<NetworkManager> 
{
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private NetworkPlayer nplayer = new NetworkPlayer();

	public HostData[] hostList;

	void Start()
	{
		MasterServer.ipAddress = "127.0.0.1";
	}

	public void StartServer(int port)
	{
		//MasterServer.ipAddress = "127.0.0.1";//nplayer.externalIP;
		//MasterServer.port = port;
		Network.InitializeServer(4, port, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied" + MasterServer.ipAddress + ":" + MasterServer.port.ToString());
		string masterserver = MasterServer.ipAddress + ":" + MasterServer.port.ToString();
		EventManager.instance.DispatchEvent(NetworkManager.instance, GameEvent.NETWORK_MASTER_SERVER_UP, masterserver);
	}

	public void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		Debug.Log ("------ Get Server List ------");
		if(msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
		EventManager.instance.DispatchEvent(NetworkManager.instance, GameEvent.NETWORK_HOST_LIST_UPDATE);
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

	public void JoinServer(string ip, int port)
	{
		Network.Connect(ip, port);
	}

	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}
}
