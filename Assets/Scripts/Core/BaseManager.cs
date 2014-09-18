using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour 
{
	public static BaseManager Instance;

	void Awake()
	{
		// Register the singleton
		if(Instance != null)
		{

		}

		Instance = this;
	}

	protected virtual void OnInstanceMultiple()
	{}
}
