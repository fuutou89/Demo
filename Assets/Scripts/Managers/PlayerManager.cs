using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Manager;

public class PlayerManager : Singleton<PlayerManager> 
{
	public enum FingerGuess
	{
		None = 0,
		gu = 1,
		cyoki = 2,
		pa = 3,
		_LastNotUse
	}
	public FingerGuess selfguess;
	public FingerGuess otherguess;

	public bool isWaiting = true;
	public bool isChoosing = true;
	public DuelControl duelControl;

	void Start()
	{
		EventManager.instance.AddEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_AWAKE_START, _OnAwakePhaseStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);
	}

	void OnDestory()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);

		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_AWAKE_START, _OnAwakePhaseStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);
		
		
	}

	private void _OnDefenceStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		if(temp.energyMax == 0)
		{
			temp.energyMax = CalStartEnergy();
			temp.energy = temp.energyMax;
		}
		temp.phase = (int)StateID.Defence;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private void _OnAwakePhaseStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		temp.cardlist.ForEach(e => { e.status = true; });
		if(temp.energyMax == 0)
		{
			temp.energyMax = CalStartEnergy();
		}
		temp.energy = temp.energyMax;
		temp.phase = (int)StateID.Awake;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private int CalStartEnergy()
	{
		int startenergy = 0;
		CardSet masterset = PhotonNetwork.masterClient.GetPlayerCardSet();
		if(PhotonNetwork.player.isMasterClient)
		{
			if(masterset.firstChoice == 1) startenergy = 1;
			else startenergy= 2;
		}
		else
		{
			if(masterset.firstChoice == 1) startenergy = 2;
			else startenergy = 1;
		}
		return startenergy;
	}
	
	private void _OnPlayerCardUpdate (params object[] args)
	{
		//CheckFingerGuess();
		CardSet masterset = PhotonNetwork.masterClient.GetPlayerCardSet();
		if(masterset.firstChoice != 0)
		{
			isChoosing = false;
		}
	}

	private void CheckFingerGuess()
	{
		bool finish = true;
		foreach(PhotonPlayer player in PhotonNetwork.playerList)
		{
			CardSet set = player.GetPlayerCardSet();
			if(set.finger == (int)FingerGuess.None) finish = false;
		}
		//isGuessingFinger = finish;
	}

	public int FingleGuessResult()
	{
		selfguess = (FingerGuess)PhotonNetwork.player.GetPlayerCardSet().finger;
		if(PhotonNetwork.player == PhotonNetwork.masterClient)
		{
			List<PhotonPlayer> playerlist = new List<PhotonPlayer>(PhotonNetwork.playerList);
			PhotonPlayer guestPlayer = playerlist.Find(e => e.isMasterClient == false);
			otherguess = (FingerGuess)guestPlayer.GetPlayerCardSet().finger;
		}
		else
		{
			otherguess = (FingerGuess)PhotonNetwork.masterClient.GetPlayerCardSet().finger;
		}

		if(selfguess == otherguess)
		{
			return 0;
		}
		else if(selfguess == FingerGuess.gu)
		{
			if(otherguess == FingerGuess.cyoki) return 1;
			else return -1;
		}
		else if(selfguess == FingerGuess.cyoki)
		{
			if(otherguess == FingerGuess.pa) return 1;
			else return -1;
		}	
		else if(selfguess == FingerGuess.pa)
		{
			if(otherguess == FingerGuess.gu) return 1;
			else return -1;
		}

		return 0;
	}

	public CardSet playerCardSet
	{
		get
		{
			return PhotonNetwork.player.GetPlayerCardSet();
		}
	}

	public void InitCareSet()
	{
		PhotonNetwork.player.UpdateCardSet(new CardSet());
	}

	public void UpdateCardZone(int zonepos, string cardno)
	{
		CardSet temp = playerCardSet;
		if(temp == null) temp = new CardSet();
		//temp.setlist[zonepos] = cardno;
		BattleCard card = new BattleCard(cardno, true);
		temp.cardlist[zonepos] = card;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	public void UpdateCardStatus(int zonepos, bool status)
	{
		CardSet temp = playerCardSet;
		temp.cardlist[zonepos].status = status;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	public void AddShowCard(string cardno)
	{
		CardSet temp = playerCardSet;
		if(temp == null) temp = new CardSet();
		temp.showlist.Add(cardno);
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	public void ClearShowCard()
	{
		CardSet temp = playerCardSet;
		if(temp == null) temp = new CardSet();
		temp.showlist = new List<string>();
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	public void ChooseFinger(int finger)
	{
		CardSet temp = playerCardSet;
		temp.finger = finger;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	public void FirstChoice(int choice)
	{
		CardSet temp = playerCardSet;
		temp.firstChoice = choice;
		PhotonNetwork.player.UpdateCardSet(temp);
	}
}
