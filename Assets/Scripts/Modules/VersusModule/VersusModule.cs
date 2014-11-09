using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class VersusModule : Module 
{
	new public static string NAME = "VersusModule";
	public VersusModule():base(NAME){}
	
	protected override void _Start ()
	{
		_RegisterMediator(new VersusMediator());
	}
	
	protected override void _Dispose ()
	{
		_RemoveMediator();
	}
}
