using UnityEngine;
using System.Collections;

public class BaseView : MonoBehaviour 
{
	public virtual void _Init()
	{}

	// Use this for initialization
	void Start () 
	{
		_Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
