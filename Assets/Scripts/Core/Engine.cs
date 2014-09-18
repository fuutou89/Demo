using UnityEngine;
using System.Collections;

/// <summary>
/// Everything start from here
/// </summary>
public class Engine : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// Load All Supporter
		this.gameObject.AddComponent<NetworkManager>();
		this.gameObject.AddComponent<PoolManager>();
	}
}
