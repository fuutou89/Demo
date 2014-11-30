using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VersusView : BaseView 
{
	public UILabel txtWaiting;
	public UITexture texSelf;
	public UITexture texTarget; 
	public UILabel txtMaster;
	public UILabel txtGuest;
	public UIButton btnAddEnergy;
	private CardSet selfcardset = new CardSet();

	public PlayArea areaSelf;
	public PlayArea areaTarget;

	public CardDes cardDesPanel;

	public DuelControl duelControl;
	public GameObject FingreGuessing;
	public UISprite spFingerTarget;
	public UISprite spFingerSelf;
	public GameObject GuessOper;
	public List<UIButton> btnFinger = new List<UIButton>();

	public GameObject StartChoice;
	public UIButton btnFirst;
	public UIButton btnSecond;

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
			PlayerManager.Instance.isWaiting = false;
		}

		foreach(PhotonPlayer player in PhotonNetwork.playerList)
		{
			Debug.Log(player.name);
		}

		cardDesPanel.gameObject.SetActive(false);

		btnFinger.ForEach(e => {
			PoolManager.GetComponent<UIEventListener>(e).onClick = OnClickbtnFinger;
		});

		spFingerSelf.spriteName = "self";
		spFingerTarget.spriteName = "target";

		PoolManager.GetComponent<UIEventListener>(btnFirst).onClick = OnClickbtnFirst;
		PoolManager.GetComponent<UIEventListener>(btnSecond).onClick = OnClickbtnSecond;
	}

	void OnClickbtnFirst (GameObject go)
	{
		PlayerManager.Instance.FirstChoice(1);
	}
	
	void OnClickbtnSecond (GameObject go)
	{
		PlayerManager.Instance.FirstChoice(-1);
	}

	void OnClickbtnFinger (GameObject go)
	{
		int index = btnFinger.FindIndex(e => e.gameObject == go);
		if(index != -1)
		{
			PlayerManager.Instance.ChooseFinger(index + 1);
			spFingerSelf.spriteName = ((PlayerManager.FingerGuess)(index + 1)).ToString();
		}
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		txtGuest.text = newPlayer.name;

		PlayerManager.Instance.isWaiting = false;
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

	public void PlayerFingerAni(bool reset)
	{
		GuessOper.SetActive(false);
		spFingerTarget.spriteName = PlayerManager.Instance.otherguess.ToString();
		if(reset)
		{
			StartCoroutine(DelayRestartGuess());
		}
	}

	IEnumerator DelayRestartGuess()
	{
		yield return new WaitForSeconds(2f);
		GuessOper.SetActive(true);
	}
}
