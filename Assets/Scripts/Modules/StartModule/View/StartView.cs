using UnityEngine;
using System.Collections;
using Core.Manager;

public class StartView : BaseView 
{
	public PageStart pageStart;
	public PageHost pagehost;
	public PageJoin pageJoin;

	public override void _Init ()
	{
		PoolManager.Instance.AddView(StartNotes.START_VIEW, this);
	}
}
