using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class StartModule :  Module
{
	new public static string NAME = "StartModule";
	public StartModule():base(NAME){}

	protected override void _Start ()
	{
		_RegisterMediator(new StartMediator());
	}

	protected override void _Dispose ()
	{
		_RemoveMediator();
	}
}
