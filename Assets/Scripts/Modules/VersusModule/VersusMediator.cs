using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Core.Manager;

public class VersusMediator : Mediator
{
	public VersusView _View
	{
		get
		{
			return PoolManager.Instance.GetView(Resconfig.VERSUS_VIEW) as VersusView;
		}
	}

	public override void OnRegister ()
	{
		PoolManager.Instance.AddView(Resconfig.VERSUS_VIEW);

		EventManager.instance.AddEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);
	}

	public override void OnRemove ()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);	
	}

	private void _OnPlayerCardUpdate (params object[] args)
	{
		foreach(PhotonPlayer player in PhotonNetwork.playerList)
		{
			if(player != PhotonNetwork.player)
			{
				_View.areaTarget.UpdateArea(player.GetPlayerCardSet());
			}
			else
			{
				_View.areaSelf.UpdateArea(player.GetPlayerCardSet());
			}
		}
	}
}