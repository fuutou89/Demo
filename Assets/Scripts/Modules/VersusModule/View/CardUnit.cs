using UnityEngine;
using System.Collections;
using Config;

public class CardUnit : MonoBehaviour 
{
	public UITexture texCard;
	public UIButton btnCard;
	private string cardno;

	// Use this for initialization
	void Start () 
	{
		PoolManager.GetComponent<UIEventListener>(btnCard).onClick = OnClickbtnCard;
		PoolManager.GetComponent<UIEventListener>(btnCard).onHover = OnHoverbtnCard;
	}

	void OnHoverbtnCard (GameObject go, bool state)
	{
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
				string[] namepre = cfg.no.Split('-');
				string impagepath = Resconfig.RES_CARD_IMAGE + namepre[0] + "/" + cfg.img;
				Texture2D tex = Resources.Load(impagepath) as Texture2D;
				texCard.mainTexture = tex;
			}
			else
			{
				texCard.mainTexture = null;
			}
		}
	}
}
