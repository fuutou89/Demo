using UnityEngine;
using System.Collections;
using PureMVC.Patterns;

public class InitalizeModule : Module 
{
	new public static string NAME = "InitalizeModule";
	public InitalizeModule():base(NAME){}
	
	protected override void _Start ()
	{
		Scene.Instance.uiLayer = GameObject.FindGameObjectWithTag("UICamera").transform;
		ModuleManager.instance.GotoModule(new StartModule());
		//ModuleManager.instance.AddAdditionalModule(new StartModule());
	}
	
	protected override void _Dispose ()
	{
	}
}
