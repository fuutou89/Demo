using UnityEngine;
using System.Collections;

public class BattleCom : Photon.MonoBehaviour 
{
	[RPC]
	public void Damage(int damage, PhotonMessageInfo mi)
	{
		Debug.Log ("------ Damage ------" + damage);
	}

	[RPC]
	public void TurnStart(PhotonMessageInfo mi)
	{
		PlayerManager.Instance.isTrunStart = true;
	}
}
