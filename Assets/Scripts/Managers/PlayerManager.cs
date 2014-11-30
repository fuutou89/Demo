using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Manager;
using Config;

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
	public bool isPhaseEnd = false;

	public bool WaitingStart = false;
	public bool isTrunStart = false;

	public DuelControl duelControl;

	void Start()
	{
		EventManager.instance.AddEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);

		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_AWAKE_START, _OnAwakePhaseStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_DRAW_START, _OnDrawStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_ENERGY_START, _OnEnergyStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_MAIN_START, _OnMainStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_ATTACK_START, _OnAttackStart);
		EventManager.instance.AddEventListener(EventManager.instance, VersusNotes.VERSUS_END_START, _OnEndStart);
	}

	void OnDestory()
	{
		EventManager.instance.RemoveEventListener(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE, _OnPlayerCardUpdate);

		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_AWAKE_START, _OnAwakePhaseStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START, _OnDefenceStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_DRAW_START, _OnDrawStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_ENERGY_START, _OnEnergyStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_MAIN_START, _OnMainStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_ATTACK_START, _OnAttackStart);
		EventManager.instance.RemoveEventListener(EventManager.instance, VersusNotes.VERSUS_END_START, _OnEndStart);
	}

	private void _OnEndStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		//Discard Boost
		int boostcount = 0;
		for(int i = 0; i < temp.cardlist.Count; i++)
		{
			cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(temp.cardlist[i].cardno);
			if(cfg != null)
			{
				if(cfg.boost == "boost") 
				{
					boostcount++;
					temp.cardlist[i] = new BattleCard();
				}
			}
		}
		temp.discard += boostcount;

		//Discard InHand
		if(temp.inhand >= 8)
		{
			temp.discard += (temp.inhand - 7);
			temp.inhand = 7;
		}

		//Discard AC Card
		if(temp.cardlist[4].cardno != "")
		{
			temp.cardlist[4] = new BattleCard();
			temp.discard += 1;
		}

		temp.phase = (int)StateID.End;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private void _OnAttackStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		temp.phase = (int)StateID.Attack;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private void _OnMainStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		temp.phase = (int)StateID.Main;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private void _OnEnergyStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		if(temp.energy < 8)
		{
			if(temp.deck < 1)
			{
				temp.deck = temp.discard - 1;
				temp.discard = 0;
			}
			temp.deck -= 1;
			temp.energyMax = temp.energyMax + 1;
			temp.energy = temp.energyMax;
		}
		temp.phase = (int)StateID.Energy;
		PhotonNetwork.player.UpdateCardSet(temp);

		StartCoroutine(SwitchPhase());
	}

	private void _OnDrawStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		if(temp.deck < 2)
		{
			temp.deck = temp.discard - 1;
			temp.discard = 0;
		}
		temp.deck -= 2;
		temp.inhand += 2;
		temp.phase = (int)StateID.Draw;
		PhotonNetwork.player.UpdateCardSet(temp);

		StartCoroutine(SwitchPhase());
	}

	private void _OnDefenceStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		if(temp.energyMax == 0)
		{
			temp.energyMax = CalStartEnergy();
			temp.energy = temp.energyMax;
			temp.deck = temp.deck - CalStartEnergy();
		}
		temp.phase = (int)StateID.Defence;
		temp.ownTurn = false;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

	private void _OnAwakePhaseStart (params object[] args)
	{
		CardSet temp = playerCardSet;
		temp.cardlist.ForEach(e => { e.status = true; });
		if(temp.energyMax == 0)
		{
			temp.energyMax = CalStartEnergy();
			temp.deck = temp.deck - CalStartEnergy();
		}
		temp.energy = temp.energyMax;
		temp.phase = (int)StateID.Awake;
		temp.ownTurn = true;
		PhotonNetwork.player.UpdateCardSet(temp);

		StartCoroutine(SwitchPhase());
	}

	IEnumerator SwitchPhase()
	{
		yield return new WaitForSeconds(1f);
		isPhaseEnd = true;
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
		// Check Prepare Status
		CardSet masterset = PhotonNetwork.masterClient.GetPlayerCardSet();
		if(masterset.firstChoice != 0)
		{
			isChoosing = false;
		}

		List<PhotonPlayer> playerlist = new List<PhotonPlayer>(PhotonNetwork.playerList);
		PhotonPlayer otherplayer = playerlist.Find(e => e != PhotonNetwork.player);
		if(otherplayer != null)
		{
			CardSet selfset = PhotonNetwork.player.GetPlayerCardSet();
			CardSet otherset = otherplayer.GetPlayerCardSet();
			if(WaitingStart)
			{
				if(otherset.phase == (int)StateID.Defence && selfset.ownTurn == false)
				{
					isTrunStart = true;
					WaitingStart = false;
				}
			}

			if(otherset.ownTurn == true && selfset.phase == (int)StateID.Defence && otherset.phase == (int)StateID.End)
			{
				WaitingStart = true;
			}
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
		CardSet initset = new CardSet();
		initset.inhand = 5;
		initset.deck = 45;
		PhotonNetwork.player.UpdateCardSet(initset);
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
