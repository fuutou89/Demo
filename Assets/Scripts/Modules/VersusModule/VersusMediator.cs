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
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_ON_CARD, _OnHoverOnCard);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_OFF_CARD, _OnHoverOffCard);
		
	}

	public override void OnRemove ()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_ON_CARD, _OnHoverOnCard);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_OFF_CARD, _OnHoverOffCard);
		
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

	private void _OnHoverOnCard (params object[] args)
	{
		if(_View != null)
		{
			_View.cardDesPanel.gameObject.SetActive(true);
			CardUnit cu = args[0] as CardUnit;
			switch(cu.unitside)
			{
			case PlayArea.Side.UP:
				_View.cardDesPanel.gameObject.transform.localPosition = new Vector3(0, -200, 0);
				break;
			case PlayArea.Side.DOWN:
				_View.cardDesPanel.gameObject.transform.localPosition = new Vector3(0, 200, 0);
				break;
			}
			_View.cardDesPanel.UpdateCardDes(cu.CARDNO);
		}
	}

	private void _OnHoverOffCard (params object[] args)
	{
		if(_View != null)
		{
			_View.cardDesPanel.gameObject.SetActive(false);
		}
	}
}








