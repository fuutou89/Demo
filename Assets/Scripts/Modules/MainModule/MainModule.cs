using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class MainModule : Module 
{
	new public static string NAME = "InitalizeModule";
	public MainModule():base(NAME){}

	protected override void _Start ()
	{
	}
	
	protected override void _Dispose ()
	{
	}
}
