using UnityEngine;
using System.Collections;

public class AwakePhase : FSMState
{
	public AwakePhase()
	{
		stateID = StateID.Awake;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			Debug.Log ("------ SetTransition(Transition.AwakePhaseEnd) ------");
			npc.GetComponent<DuelControl>().SetTransition(Transition.AwakePhaseEnd);
			pa.PhaseEnd = false;
		}
	}

	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "AwakePhase";
	}

	public override void DoBeforeEntering ()
	{
		VersusView vsview = PoolManager.Instance.GetView(Resconfig.VERSUS_VIEW) as VersusView;
		PlayArea selfarea = vsview.areaSelf;
		selfarea.togglebar.SetBarValue(3,3);
	}
}

public class DrawPhase : FSMState
{
	public DrawPhase()
	{
		stateID = StateID.Draw;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			Debug.Log ("------ SetTransition(Transition.DrawPhaseEnd) ------");
			
			npc.GetComponent<DuelControl>().SetTransition(Transition.DrawPhaseEnd);
			pa.PhaseEnd = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "DrawPhase";
	}
}

public class EnergyPhase : FSMState
{
	public EnergyPhase()
	{
		stateID = StateID.Energy;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			npc.GetComponent<DuelControl>().SetTransition(Transition.EnergyPhaseEnd);
			pa.PhaseEnd = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "EnergyPhase";
	}
}

public class MainPhase : FSMState
{
	public MainPhase()
	{
		stateID = StateID.Main;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			npc.GetComponent<DuelControl>().SetTransition(Transition.MainPhaseEnd);
			pa.PhaseEnd = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "MainPhase";
	}
}

public class AttackPhase : FSMState
{
	public AttackPhase()
	{
		stateID = StateID.Attack;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			npc.GetComponent<DuelControl>().SetTransition(Transition.AttackPhaseEnd);
			pa.PhaseEnd = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "AttackPhase";
	}
}

public class EndPhase : FSMState
{
	public EndPhase()
	{
		stateID = StateID.End;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.PhaseEnd)
		{
			npc.GetComponent<DuelControl>().SetTransition(Transition.TrunEnd);
			pa.PhaseEnd = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "EndPhase";
	}
}

public class DefenceState : FSMState
{
	public DefenceState()
	{
		stateID = StateID.Defence;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		if(pa.TurnStart)
		{
			npc.GetComponent<DuelControl>().SetTransition(Transition.TurnStart);
			pa.TurnStart = false;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		PlayArea pa = player.GetComponent<PlayArea>();
		pa.txtPhase.text = "DefenceState";
	}
}