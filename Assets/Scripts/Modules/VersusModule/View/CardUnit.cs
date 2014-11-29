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
	public string CARDNO { get { return cardno; }}
	public PlayArea.Side unitside;

	public int zonepos;

	// Use this for initialization
	void Start () 
	{
		PoolManager.GetComponent<UIEventListener>(btnCard).onClick = OnClickbtnCard;
		PoolManager.GetComponent<UIEventListener>(btnCard).onHover = OnHoverbtnCard;
		PoolManager.GetComponent<UIEventListener>(btnShift).onClick = OnClickbtnShift;
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
}
