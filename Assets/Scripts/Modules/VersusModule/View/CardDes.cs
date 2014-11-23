using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;

public class CardDes : MonoBehaviour 
{
	public GameObject effectPrefab;
	public UITexture texCard;
	public UILabel txtCardName;
	public UILabel txtCardDes;
	public UITable tableEffect;
	public UILabel txtDes;
	private List<GameObject> effectlist = new List<GameObject>();

	private string _cardno;
	public string CardNo { get { return _cardno; } }


	public void UpdateCardDes(string no)
	{
		if(_cardno != no)
		{
			_cardno = no;
			if(_cardno != "")
			{
				cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(_cardno);
				string[] namepre = cfg.no.Split('-');
				string impagepath = Resconfig.RES_CARD_IMAGE + namepre[0] + "/" + cfg.img;
				Texture2D tex = Resources.Load(impagepath) as Texture2D;
				texCard.mainTexture = tex;
				if(cfg.desc != "null") txtDes.text = cfg.desc;
				ShowCardEffect(CardInfoManager.Instance.CalCardEffectGroup(cfg));
				txtCardName.text = cfg.name;
				txtCardDes.text = CardInfoManager.Instance.FormatCardDes(cfg);
			}
		}
	}

	private void ShowCardEffect(List<CardEffect> effectgroup)
	{
		foreach(GameObject eu in effectlist)
		{
			Destroy(eu);
		}
		effectlist = new List<GameObject>();
		int count = 0;
		foreach(CardEffect effect in effectgroup)
		{
			if(effect.type != "")
			{
				GameObject go = NGUITools.AddChild(tableEffect.gameObject, effectPrefab);
				effectlist.Add(go);
				go.name = "effect" + count.ToString();
				CardEffectUnit efu = go.GetComponent<CardEffectUnit>();
				efu.UpdateEffect(effect);
				count ++;
			}
		}
		StartCoroutine(DelayRepos());
	}
	
	IEnumerator DelayRepos()
	{
		yield return new WaitForEndOfFrame();
		tableEffect.Reposition();
	}
}
