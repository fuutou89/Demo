using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Core.Manager;

public class StartMediator : Mediator 
{
	private StartView _View
	{
		get
		{
			return PoolManager.Instance.GetView(StartNotes.START_VIEW) as StartView;
		}
	}

	public override void OnRegister ()
	{
		EventManager.instance.AddEventListener(EventManager.instance, "TEST", _OnTestEvent);
	}

	public override void OnRemove ()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, "TEST", _OnTestEvent);
	}

	private void _OnTestEvent (params object[] args)
	{
		if(_View != null)
		{
			Debug.Log("_OnTestEvent\t");
			_View.pagehost.gameObject.SetActive(true);
		}
	}
}
