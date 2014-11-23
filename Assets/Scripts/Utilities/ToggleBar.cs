using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleBar : MonoBehaviour 
{
	private List<UIToggle> toggleList = new List<UIToggle>();

	// Use this for initialization
	void Start () 
	{
		UIToggle[] togglearray = this.gameObject.GetComponentsInChildren<UIToggle>();
		toggleList = new List<UIToggle>(togglearray);
	}

	public void SetBarValue(int shownum, int activenum)
	{
		for(int i = 0; i < toggleList.Count; i++)
		{
			if(i < shownum) toggleList[i].gameObject.SetActive(true);
			else toggleList[i].gameObject.SetActive(false);
		}

		for(int j = 0; j < toggleList.Count; j++)
		{
			if(j < activenum) toggleList[j].value = true;
			else toggleList[j].value = false;
		}
	}
}
