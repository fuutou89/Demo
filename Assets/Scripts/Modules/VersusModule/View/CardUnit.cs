using UnityEngine;
using System.Collections;
using Config;
using Core.Manager;

public class CardUnit : MonoBehaviour 
{
	public UITexture texCard;
	public UIButton btnCard;
	public UIButton btnShift;
	public UIInput inputShift;
	private string cardno = "";
	public string CARDNO { get { return cardno; } }
	private BattleCard _card;
	public BattleCard CARD { get { return _card; } }

	private PlayArea.Side _unitside;
	public PlayArea.Side unitside { 
		get { return _unitside;} 
		set {
			_unitside = value;
			SetSideStyle();
		}
	}
	public UIButton btnFall;
	public UILabel txtFall;

	public int zonepos;

	// Use this for initialization
	void Start () 
	{
		if(btnCard != null) 
		{
			PoolManager.GetComponent<UIEventListener>(btnCard).onClick = OnClickbtnCard;
			PoolManager.GetComponent<UIEventListener>(btnCard).onHover = OnHoverbtnCard;
		}
		if(btnShift != null) PoolManager.GetComponent<UIEventListener>(btnShift).onClick = OnClickbtnShift;
		if(btnFall != null) PoolManager.GetComponent<UIEventListener>(btnFall).onClick = OnClickbtnFall;
	}

	void OnClickbtnFall (GameObject go)
	{
		PlayerManager.Instance.UpdateCardStatus(zonepos, !_card.status);
	}

	private void SetSideStyle()
	{
		if(unitside == PlayArea.Side.DOWN) return;

		if(btnShift != null) btnShift.gameObject.SetActive(false);
		if(inputShift != null) inputShift.gameObject.SetActive(false);
	}

	void OnClickbtnShift (GameObject go)
	{
		if(inputShift.value != "")
		{
			cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(inputShift.value);
			if(cfg != null)
			{
				PlayerManager.Instance.UpdateCardZone(zonepos, inputShift.value);
			}
		}
	}

	void OnHoverbtnCard (GameObject go, bool state)
	{
		if(state)
		{
			if(cardno != "")
			{
				EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_HOVER_ON_CARD, this);
			}
		}
		else
		{
			EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_HOVER_OFF_CARD);
		}
	}

	void OnClickbtnCard (GameObject go)
	{
	}

	public void UpdateUnit(string no)
	{
		if(cardno != no)
		{
			cardno = no;
			if(cardno != "")
			{
				cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(cardno);
				if(cfg != null)
				{
					string[] namepre = cfg.no.Split('-');
					string impagepath = Resconfig.RES_CARD_IMAGE + namepre[0] + "/" + cfg.img;
					Texture2D tex = Resources.Load(impagepath) as Texture2D;
					texCard.mainTexture = tex;
				}
			}
			else
			{
				texCard.mainTexture = null;
			}
		}
	}

	public void UpdateBattleUnit(BattleCard battlecard)
	{
		if(battlecard != null && battlecard.cardno != "")
		{
			_card = battlecard;
			UpdateUnit(_card.cardno);
			float targetAngle = 0;
			if(_card.status) 
			{
				if(txtFall != null) txtFall.text = "FALL";
				iTween.RotateTo(texCard.gameObject, new Vector3(0, 0, 0), 0.5f);
			}
			else
			{
				if(txtFall != null) txtFall.text = "WAKE";
				iTween.RotateTo(texCard.gameObject, new Vector3(0, 0, -90), 0.5f);
				
			}
		}

	}
}
