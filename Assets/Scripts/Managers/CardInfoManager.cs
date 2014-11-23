using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;
using Core.Manager;

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

	public List<CardEffect> CalCardEffectGroup(cfgcard cfg)
	{
		List<CardEffect> effectgroup = new List<CardEffect>();
		if(cfg != null)
		{
			effectgroup.Add(FormatCardEffect(cfg.effect1));
			effectgroup.Add(FormatCardEffect(cfg.effect2));
			effectgroup.Add(FormatCardEffect(cfg.effect3));
		}
		return effectgroup;
	}

	private CardEffect FormatCardEffect(string effect)
	{
		CardEffect cardeffect = new CardEffect();
		if(effect != "null")
		{
			if(effect.Contains("ji"))
			{
				cardeffect.type = "ji";
			}
			else if(effect.Contains("ki"))
			{
				cardeffect.type = "ki";
			}
			else if(effect.Contains("jyou"))
			{
				cardeffect.type = "jyou";
			}
			cardeffect.des = effect.Replace(cardeffect.type, "");
			if(cardeffect.des.Contains("fall")) cardeffect.des = cardeffect.des.Replace("fall", OTManager.instance.GetOT("CARD_DES_FALL"));
		}
		return cardeffect;
	}

	public string FormatCardDes(cfgcard cfg)
	{
		string carddes = "";
		carddes += OTManager.instance.GetOT("CARD_POWER", cfg.power.ToString()) + "\\";
		carddes += OTManager.instance.GetOT("CARD_GUARD", cfg.guard.ToString()) + "\n";
		carddes += OTManager.instance.GetOT("CARD_COLOR", cfg.color) + "\\";
		carddes += OTManager.instance.GetOT("CARD_NO", cfg.no) + "\\";
		carddes += OTManager.instance.GetOT("CARD_RARE", cfg.rare) + "\n";
		carddes += OTManager.instance.GetOT("CARD_TYPE", cfg.kind) + "\\";
		carddes += OTManager.instance.GetOT("CARD_LEVEL", cfg.level.ToString()) + "\\";
		if(cfg.frame != "null")
		{
			carddes += OTManager.instance.GetOT("CARD_FRAME", cfg.frame) + "\\";
		}
		carddes += OTManager.instance.GetOT("CARD_STRIKE", cfg.strike.ToString()) + "\\";
		carddes += OTManager.instance.GetOT("CARD_FEATURE", cfg.feature);
		return carddes;
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

public class CardEffect
{
	public string type = "";
	public string des = "";
}