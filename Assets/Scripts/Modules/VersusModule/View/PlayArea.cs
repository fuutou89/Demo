using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Config;

public class PlayArea : MonoBehaviour 
{
	//public List<UITexture> texProgressList = new List<UITexture>();

	public bool TurnStart = false;
	public bool PhaseEnd = false;
	public UILabel txtPhase;
	//public ToggleBar togglebar;
	public ToggleBar togglebarEnergy;
	public ToggleBar togglebarDamage;

	public enum Side
	{
		None,
		UP,
		DOWN,
		LastNotUse
	}

	public List<CardUnit> cardProgressList = new List<CardUnit>();
	public CardUnit cardAction;
	public Side _areaSide;
	private List<CardUnit> showUnitList = new List<CardUnit>();
	public GameObject ShowZoneTable;
	public UIButton btnAddShowCard;
	public UIButton btnClearShowCard;
	public UIInput inputShowCard;

	public UILabel txtDeck;
	public UILabel txtInhand;
	public UILabel txtDiscard;

	public UIButton btnEndPhase;
	public UILabel txtEndPhase;

	private CardSet _tempset;
	public CardSet Tempset { get { return _tempset; } }

	public UIButton btnHit;
	public UIButton btnHeal;
	public UIButton btnCost;
	public UIButton btnCharge;

	// Use this for initialization
	void Start () 
	{
		PoolManager.GetComponent<UIEventListener>(btnAddShowCard).onClick = OnClickbtnAddShowCard;
		PoolManager.GetComponent<UIEventListener>(btnClearShowCard).onClick = OnClickbtnClearShowCard;
		if(btnEndPhase != null)
			PoolManager.GetComponent<UIEventListener>(btnEndPhase).onClick = OnClickbtnEndPhase;

		cardProgressList.ForEach(e => { 
			e.unitside = _areaSide;
		});
		cardAction.unitside = _areaSide;

		showUnitList = new List<CardUnit>(ShowZoneTable.GetComponentsInChildren<CardUnit>());
		showUnitList.ForEach(e => { e.unitside = _areaSide; });
		for(int i = 0; i < showUnitList.Count; i++)
		{
			showUnitList[i].texCard.depth = i + 2;
			UISprite buttonsprite = showUnitList[i].btnCard.gameObject.GetComponent<UISprite>();
			buttonsprite.depth = i + 1;
			showUnitList[i].gameObject.SetActive(false);

		}
		SetAreaStyle();

		if(btnHit != null) PoolManager.GetComponent<UIEventListener>(btnHit).onClick = OnClickbtnHit;
		if(btnHeal != null) PoolManager.GetComponent<UIEventListener>(btnHeal).onClick = OnClickbtnHeal;
		if(btnCost != null) PoolManager.GetComponent<UIEventListener>(btnCost).onClick = OnClickbtnCost;
		if(btnCharge != null) PoolManager.GetComponent<UIEventListener>(btnCharge).onClick = OnClickbtnCharge;
	}

	void OnClickbtnCharge (GameObject go)
	{
		PlayerManager.Instance.Charge();
	}

	void OnClickbtnCost (GameObject go)
	{
		PlayerManager.Instance.Cost();
	}

	void OnClickbtnHit (GameObject go)
	{
		PlayerManager.Instance.Damage();
	}

	void OnClickbtnHeal (GameObject go)
	{
		PlayerManager.Instance.Heal();
	}

	void OnClickbtnEndPhase (GameObject go)
	{
		PlayerManager.Instance.isPhaseEnd = true;
	}

	void SetAreaStyle()
	{
		if(_areaSide == Side.DOWN) return;

		btnAddShowCard.gameObject.SetActive(false);
		btnClearShowCard.gameObject.SetActive(false);
		inputShowCard.gameObject.SetActive(false);
	}

	void OnClickbtnAddShowCard (GameObject go)
	{
		if(inputShowCard.value != "")
		{
			cfgcard cfg = CardInfoManager.Instance.GetCardConfigByNo(inputShowCard.value);
			if(cfg != null)
			{
				PlayerManager.Instance.AddShowCard(inputShowCard.value);
			}
		}
	}

	void OnClickbtnClearShowCard (GameObject go)
	{
		PlayerManager.Instance.ClearShowCard();
	}


	public void UpdateArea(CardSet set)
	{
		_tempset = set;

		// Update Progress
		for(int i = 0; i < cardProgressList.Count; i++)
		{
			//cardProgressList[i].UpdateUnit(set.setlist[i]);
			cardProgressList[i].UpdateBattleUnit(set.cardlist[i]);
		}

		// Update Action Zone
		//cardAction.UpdateUnit(set.setlist[4]);
		cardAction.UpdateBattleUnit(set.cardlist[4]);

		// Update Show Zone
		showUnitList.ForEach(e => { e.gameObject.SetActive(false); });

		for(int j = 0; j < showUnitList.Count; j++)
		{
			if(j < set.showlist.Count) 
			{
				showUnitList[j].gameObject.SetActive(true);
				showUnitList[j].UpdateUnit(set.showlist[j]);
			}
			else
			{
				showUnitList[j].gameObject.SetActive(false);
			}
		}

		// Update Energy
		togglebarEnergy.SetBarValue(set.energyMax, set.energy);

		// Update Damage
		togglebarDamage.SetBarValue(8, set.damage);

		// Update Phase Title
		txtPhase.text = "Phase" + ((StateID)set.phase).ToString();
		if(txtEndPhase != null) txtEndPhase.text = ((StateID)set.phase).ToString() + "Phase\nEnd"; 

		// Update Other
		txtDeck.text = set.deck.ToString();
		txtInhand.text = set.inhand.ToString();
		txtDiscard.text = set.discard.ToString();
	}
}
