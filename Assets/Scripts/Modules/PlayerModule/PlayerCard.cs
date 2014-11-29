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
public class CardSet
{
	public List<string> setlist = new List<string>();
	public int energy = 0;
	public int damage = 0;
}

public class PlayerCard : MonoBehaviour 
{
	public const string PlayerCardProp = "playercard";

	void Start()
	{
		PhotonPeer.RegisterType(typeof(CardSet), (byte)'Z', NetworkManager.SerializeVector, NetworkManager.DeserializeVector);
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