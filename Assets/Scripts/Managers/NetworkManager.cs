using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Core.Manager;
using Random = UnityEngine.Random;

public class NetworkManager : Singleton<NetworkManager> 
{
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";
	private NetworkPlayer nplayer = new NetworkPlayer();

	public HostData[] hostList;

	//private string roomName = "myRoom";
	private bool connectFailed = false;
	private Vector2 scrollPos = Vector2.zero;
	private string errorDialog;
	private double timeToClearDialog;
	public string ErrorDialog
	{
		get 
		{ 
			return errorDialog; 
		}
		private set
		{
			errorDialog = value;
			if (!string.IsNullOrEmpty(value))
			{
				timeToClearDialog = Time.time + 4.0f;
			}
		}
	}
	public void Awake()
	{
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		
		// generate a name for this player, if none is assigned yet
		if (String.IsNullOrEmpty(PhotonNetwork.playerName))
		{
			PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		}
	}

	
	void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		ModuleManager.instance.GotoModule(new InitalizeModule());
	}

	public void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			if (PhotonNetwork.connecting)
			{
				GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
			}
			else
			{
				GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
			}
			
			if (this.connectFailed)
			{
				GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
				GUILayout.Label(String.Format("Server: {0}", new object[] {PhotonNetwork.ServerAddress}));
				GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID);
				
				if (GUILayout.Button("Try Again", GUILayout.Width(100)))
				{
					this.connectFailed = false;
					PhotonNetwork.ConnectUsingSettings("0.9");
				}
			}
			
			return;
		}
		
		
//		GUI.skin.box.fontStyle = FontStyle.Bold;
//		GUI.Box(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400, 300), "Join or Create a Room");
//		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400, 300));
//		
//		GUILayout.Space(25);
//		
//		// Player name
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Player name:", GUILayout.Width(100));
//		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
//		GUILayout.Space(105);
//		if (GUI.changed)
//		{
//			// Save name
//			PlayerPrefs.SetString("playerName", PhotonNetwork.playerName);
//		}
//		GUILayout.EndHorizontal();
//		
//		GUILayout.Space(15);
//		
//		// Join room by title
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Roomname:", GUILayout.Width(100));
//		this.roomName = GUILayout.TextField(this.roomName);
//		
//		if (GUILayout.Button("Create Room", GUILayout.Width(100)))
//		{
//			PhotonNetwork.CreateRoom(this.roomName, new RoomOptions() { maxPlayers = 10 }, null);
//		}
//		
//		GUILayout.EndHorizontal();
//		
//		// Create a room (fails if exist!)
//		GUILayout.BeginHorizontal();
//		GUILayout.FlexibleSpace();
//		//this.roomName = GUILayout.TextField(this.roomName);
//		if (GUILayout.Button("Join Room", GUILayout.Width(100)))
//		{
//			PhotonNetwork.JoinRoom(this.roomName);
//		}
//		
//		GUILayout.EndHorizontal();
//		
//		
//		if (!string.IsNullOrEmpty(this.ErrorDialog))
//		{
//			GUILayout.Label(this.ErrorDialog);
//			
//			if (timeToClearDialog < Time.time)
//			{
//				timeToClearDialog = 0;
//				this.ErrorDialog = "";
//			}
//		}
//		
//		GUILayout.Space(15);
//		
//		// Join random room
//		GUILayout.BeginHorizontal();
//		
//		GUILayout.Label(PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.");
//		GUILayout.FlexibleSpace();
//		if (GUILayout.Button("Join Random", GUILayout.Width(100)))
//		{
//			PhotonNetwork.JoinRandomRoom();
//		}
//		
//		
//		GUILayout.EndHorizontal();
//		
//		GUILayout.Space(15);
//		if (PhotonNetwork.GetRoomList().Length == 0)
//		{
//			GUILayout.Label("Currently no games are available.");
//			GUILayout.Label("Rooms will be listed here, when they become available.");
//		}
//		else
//		{
//			GUILayout.Label(PhotonNetwork.GetRoomList() + " currently available. Join either:");
//			
//			// Room listing: simply call GetRoomList: no need to fetch/poll whatever!
//			this.scrollPos = GUILayout.BeginScrollView(this.scrollPos);
//			foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList())
//			{
//				GUILayout.BeginHorizontal();
//				GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
//				if (GUILayout.Button("Join"))
//				{
//					PhotonNetwork.JoinRoom(roomInfo.name);
//				}
//				
//				GUILayout.EndHorizontal();
//			}
//			
//			GUILayout.EndScrollView();
//		}
//		
//		GUILayout.EndArea();
	}


	void Start()
	{
		//MasterServer.ipAddress = "127.0.0.1";
	}

	public static void CreateRoom(string roomName)
	{
		PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, null);
	}

	public static void JoinRoom(string roomName)
	{
		PhotonNetwork.JoinRoom(roomName);
	}

	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		//ModuleManager.instance.GotoModule(new VersusModule());
	}

	public void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
		ModuleManager.instance.GotoModule(new VersusModule());
	}

	public void OnPhotonPlayerConnected()
	{

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

	public static byte[] SerializeVector(object setobj)
	{
		MemoryStream ms = new MemoryStream();
		BinaryFormatter binFormat = new BinaryFormatter();
		binFormat.Serialize(ms, setobj);
		ms.Close();
		return ms.ToArray();
	}
	
	public static object DeserializeVector(byte[] bytes)
	{
		MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
		BinaryFormatter bf = new BinaryFormatter();
		return bf.Deserialize(ms);
	}
}
