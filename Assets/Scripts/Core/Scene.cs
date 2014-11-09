using UnityEngine;
using System.Collections;

public class Scene : Singleton<Scene> 
{
	public Camera mCamera;
	public Transform uiLayer;

	void Start()
	{
		mCamera = Camera.main;
		//uiLayer = GameObject.FindGameObjectWithTag("UICamera").transform;
	}
}
