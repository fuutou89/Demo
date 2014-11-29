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
	public ToggleBar togglebar;

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

	// Use this for initialization
	void Start () 
	{
		foreach(CardUnit cu in cardProgressList)
		{
			cu.unitside = _areaSide;
		}
		cardAction.unitside = _areaSide;
	}

	public void UpdateArea(CardSet set)
	{
		for(int i = 0; i < set.setlist.Count; i++)
		{
			cardProgressList[i].UpdateUnit(set.setlist[i]);
		}
	}
}
