using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager> 
{
	public CardSet playerCardSet
	{
		get
		{
			return PhotonNetwork.player.GetPlayerCardSet();
		}
	}

	public void UpdateCardZone(int zonepos, string cardno)
	{
		CardSet temp = playerCardSet;
		temp.setlist[zonepos] = cardno;
		PhotonNetwork.player.UpdateCardSet(temp);
	}

}
