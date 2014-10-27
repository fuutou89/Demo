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
		EventManager.instance.AddEventListener(NetworkManager.Instance, GameEvent.NETWORK_MASTER_SERVER_UP, _OnMasterServerUp);
		EventManager.instance.AddEventListener(NetworkManager.Instance, GameEvent.NETWORK_HOST_LIST_UPDATE, _OnHostListUpdate);
	}

	public override void OnRemove ()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, "TEST", _OnTestEvent);
		EventManager.instance.RemoveEventListener(NetworkManager.Instance, GameEvent.NETWORK_MASTER_SERVER_UP, _OnMasterServerUp);
		EventManager.instance.RemoveEventListener(NetworkManager.Instance, GameEvent.NETWORK_HOST_LIST_UPDATE, _OnHostListUpdate);
	}

	private void _OnMasterServerUp (params object[] args)
	{
		if(_View != null)
		{
			_View.pagehost.txtHostInfo.text = args[0] as string;
		}
	}

	private void _OnHostListUpdate (params object[] args)
	{
		if(_View != null)
		{
			string hostname = "";
			if(NetworkManager.Instance.hostList != null)
			{
				foreach(HostData hd in NetworkManager.Instance.hostList)
				{
					hostname += hd.gameName + "\n";
				}
				_View.pageJoin.txtHostName.text = hostname;
			}
		}
	}

	private void _OnTestEvent (params object[] args)
	{
		if(_View != null)
		{
			Debug.Log("_OnTestEvent\t");
			//_View.pagehost.gameObject.SetActive(true);
		}
	}
}
