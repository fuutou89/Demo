using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;

public class PlayArea : MonoBehaviour 
{
	//public List<UITexture> texProgressList = new List<UITexture>();

	public List<CardUnit> cardProgressList = new List<CardUnit>();
	public CardUnit cardAction;

	// Use this for initialization
	void Start () {
	
	}

	public void UpdateArea(CardSet set)
	{
		for(int i = 0; i < set.progressList.Count; i++)
		{
			cardProgressList[i].UpdateUnit(set.progressList[i]);
//			cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(set.progressList[i]);
//			string[] namepre = cfg.no.Split('-');
//			string impagepath = Resconfig.RES_CARD_IMAGE + namepre[0] + "/" + cfg.img;
//			Texture2D tex = Resources.Load(impagepath) as Texture2D;
//			texProgressList[i].mainTexture = tex;
		}
	}
}
