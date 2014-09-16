using UnityEngine;
using System.Collections;

public class StartView : MonoBehaviour 
{
	public NetworkManager networkManager;

	public void StartHost()
	{
		networkManager.StartServer();
	}
}
