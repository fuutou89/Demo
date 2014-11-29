using UnityEngine;
using System.Collections;

public class VersusView : BaseView 
{
	public UITexture texSelf;
	public UITexture texTarget; 
	public UILabel txtMaster;
	public UILabel txtGuest;
	public UIButton btnAddEnergy;
	private CardSet selfcardset = new CardSet();

	public PlayArea areaSelf;
	public PlayArea areaTarget;

	public CardDes cardDesPanel;

	public override void _Init ()
	{
		PoolManager.GetComponent<UIEventListener>(btnAddEnergy).onClick = OnClickbtnAddEnergy;

		txtMaster.text = PhotonNetwork.masterClient.name;
		if(PhotonNetwork.masterClient != PhotonNetwork.player) 
		{
			txtGuest.text = PhotonNetwork.player.name;
			Texture temptex = texSelf.mainTexture;
			texSelf.mainTexture = texTarget.mainTexture;
			texTarget.mainTexture = temptex;
		}

		foreach(PhotonPlayer player in PhotonNetwork.playerList)
		{
			Debug.Log(player.name);
		}

		cardDesPanel.gameObject.SetActive(false);
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		txtGuest.text = newPlayer.name;
	}

	void OnPhotonPlayerDisconnected(PhotonPlayer newPlayer)
	{
		if(PhotonNetwork.masterClient == PhotonNetwork.player)
			txtGuest.text = "";
	}

	void OnClickbtnAddEnergy (GameObject go)
	{
		selfcardset.energy += 1;
		CardSet cardset = new CardSet();
		cardset = selfcardset;

		if(PhotonNetwork.masterClient != PhotonNetwork.player) cardset.setlist = CardInfoManager.Instance.GetGuestCardList();
		else cardset.setlist = CardInfoManager.Instance.GetMasterCardList();

		PhotonNetwork.player.UpdateCardSet(cardset);
	}
}
