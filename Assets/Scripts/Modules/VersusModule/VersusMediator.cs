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
		PlayerManager.Instance.InitCareSet();
		PlayerManager.Instance.duelControl = _View.duelControl;

		EventManager.instance.AddEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_ON_CARD, _OnHoverOnCard);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_OFF_CARD, _OnHoverOffCard);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_START, _OnVersusStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_PREPARE_END, _OnPrepareEnd);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_MAIN_START, _OnMainStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUE_TRUN_END, _OnTurnEnd);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_PREPARE_RESULT, _OnShowPrepareResult);
	}

	public override void OnRemove ()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_ON_CARD, _OnHoverOnCard);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_HOVER_OFF_CARD, _OnHoverOffCard);

		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_START, _OnVersusStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_PREPARE_END, _OnPrepareEnd);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_MAIN_START, _OnMainStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUE_TRUN_END, _OnTurnEnd);

		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_PREPARE_RESULT, _OnShowPrepareResult);
	}

	private void _OnTurnEnd (params object[] args)
	{
		if(_View != null)
		{

		}
	}

	private void _OnDefenceStart (params object[] args)
	{
		if(_View != null)
		{
			_View.areaSelf.btnEndPhase.gameObject.SetActive(false);
		}
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

	private void _OnVersusStart (params object[] args)
	{
		if(_View != null)
		{
			if(PhotonNetwork.masterClient == PhotonNetwork.player)
			{
				_View.txtWaiting.gameObject.SetActive(false);
				_View.StartChoice.SetActive(true);
			}
			else
			{
				_View.txtWaiting.text = OTManager.Instance.GetOT("PHASE_PREPARE_GUEST");
			}
		}
	}

	private void _OnShowPrepareResult (params object[] args)
	{
		if(_View != null)
		{
			int result = PlayerManager.Instance.FingleGuessResult();
			switch(result)
			{
			case 0:
				_View.PlayerFingerAni(true);
				break;
			case 1:
			case 2:
				_View.PlayerFingerAni(false);
				break;
			}
		}
	}

	private void _OnPrepareEnd (params object[] args)
	{
		if(_View != null)
		{
			_View.areaSelf.gameObject.SetActive(true);
			_View.areaTarget.gameObject.SetActive(true);
			_View.StartChoice.SetActive(false);
			_View.txtWaiting.gameObject.SetActive(false);
		}
	}

	private void _OnMainStart (params object[] args)
	{
		if(_View != null)
		{
			_View.areaSelf.btnEndPhase.gameObject.SetActive(true);
		}
	}
}








