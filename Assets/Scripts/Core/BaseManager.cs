using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour 
{
	protected static BaseManager _instance;

	void Awake()
	{
		// Register the singleton
		if(_instance != null)
		{

		}

		_instance = this;
	}

	protected virtual void OnInstanceMultiple()
	{}
}
