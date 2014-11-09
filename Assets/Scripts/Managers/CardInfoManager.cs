using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;

public class CardInfoManager : Singleton<CardInfoManager>  
{
	private cfgcardDataEntity _cardcfgEntity;

	public List<cfgcard> cardcfglist
	{
		get { return _cardcfgEntity.data; }
	}

	void Start()
	{
		_cardcfgEntity = Resources.Load(Resconfig.CONFIG_CARD) as cfgcardDataEntity;
		if(_cardcfgEntity == null)
		{
			Debug.LogError("Exception: Lost CardCfg Entity !!");
		}
	}

	public cfgcard GetCardConfigByNo(string no)
	{
		return _cardcfgEntity.data.Find(e => e.no == no);
	}

	public List<string> GetMasterCardList()
	{
		List<string> cardlist = new List<string>();
		cardlist.Add("B1-001");
		cardlist.Add("B1-002");
		cardlist.Add("B1-003");
		cardlist.Add("B1-004");
		return cardlist;
	}

	public List<string> GetGuestCardList()
	{
		List<string> cardlist = new List<string>();
		cardlist.Add("B2-001");
		cardlist.Add("B2-002");
		cardlist.Add("B2-003");
		cardlist.Add("B2-004");
		return cardlist;
	}
}
