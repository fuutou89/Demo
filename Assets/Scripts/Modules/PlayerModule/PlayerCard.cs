using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Config;
using Core.Manager;

[Serializable]
public class BattleCard
{
	public string cardno = "";
	public int pos;
	public bool status = true;
	public int power = 0;
	public int guard = 0;

	public BattleCard()
	{}

	public BattleCard(string no, bool awake)
	{
		cardno  = no;
		cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(no);
		power = cfg.power;
		guard = cfg.guard;
		status = awake;
	}
}

[Serializable]
public class CardSet
{
	// Data for Prepare Game
	public int finger = 0;
	public int firstChoice = 0;

	public bool ownTurn = false;
	public int phase = 0;
	public List<BattleCard> cardlist = new List<BattleCard>();
	public List<string> setlist = new List<string>(){"","","","",""};
	public List<string> showlist = new List<string>();
	public int energy = 0;
	public int energyMax = 0;
	public int damage = 0;
	public int deck = 0;
	public int discard = 0;
	public int inhand = 0;

	public CardSet()
	{
		for(int i = 0; i < 5; i++)
		{
			BattleCard status = new BattleCard();
			status.pos = i;
			cardlist.Add(status);
		}
	}
}

public class PlayerCard : MonoBehaviour 
{
	public const string PlayerCardProp = "playercard";

	void Start()
	{
		PhotonPeer.RegisterType(typeof(CardSet), (byte)'Z', NetworkManager.SerializeVector, NetworkManager.DeserializeVector);
		PhotonPeer.RegisterType(typeof(BattleCard), (byte)'Y', NetworkManager.SerializeVector, NetworkManager.DeserializeVector);
	}

	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		EventManager.instance.DispatchEvent(EventManager.instance, GameEvent.PLAYER_CARD_UPDATE);
	}
}

static class PlayerCardExtensions
{
	public static CardSet GetPlayerCardSet(this PhotonPlayer player)
	{
		object cardset;
		if(player.customProperties.TryGetValue(PlayerCard.PlayerCardProp, out cardset))
		{
			return (CardSet)cardset;
		}

		return null;
	}

	public static void UpdateCardSet(this PhotonPlayer player, CardSet cardset)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("JoinTeam was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
		}

		PhotonNetwork.player.SetCustomProperties(new Hashtable() {{PlayerCard.PlayerCardProp, cardset}});

//		CardSet currentSet = PhotonNetwork.player.GetPlayerCardSet();
//		if(currentSet != cardset)
//		{
//			PhotonNetwork.player.SetCustomProperties(new Hashtable() {{PlayerCard.PlayerCardProp, cardset}});
//		}
	}
}