using UnityEngine;
using System.Collections;
using Core.Manager;

public class WaitState : FSMState
{
	public WaitState()
	{
		stateID = StateID.Wait;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		if(!PlayerManager.Instance.isWaiting)
		{
			PlayerManager.Instance.duelControl.SetTransition(Transition.GuestJoin);
		}
	}

	public override void Act (GameObject player, GameObject npc)
	{
	}

	public override void DoBeforeLeaving ()
	{
		EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_START);
	}
}

public class PreparePhase : FSMState
{
	public PreparePhase()
	{
		stateID = StateID.Prepare;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
		if(!PlayerManager.Instance.isChoosing)
		{
			CardSet masterset = PhotonNetwork.masterClient.GetPlayerCardSet();
			if(masterset.firstChoice == -1)
			{
				if(PhotonNetwork.player.isMasterClient) PlayerManager.Instance.duelControl.SetTransition(Transition.PrepareEnd_Lose);
				else PlayerManager.Instance.duelControl.SetTransition(Transition.PrepareEnd_Win);
			}
			else if(masterset.firstChoice == 1)
			{
				if(PhotonNetwork.player.isMasterClient) PlayerManager.Instance.duelControl.SetTransition(Transition.PrepareEnd_Win);
				else PlayerManager.Instance.duelControl.SetTransition(Transition.PrepareEnd_Lose);
			}
		}
	}

	public override void Act (GameObject player, GameObject npc)
	{

	}

	public override void DoBeforeLeaving ()
	{
		EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_PREPARE_END);
	}
}

public class AwakePhase : FSMState
{
	public AwakePhase()
	{
		stateID = StateID.Awake;
	}

	public override void Reason (GameObject player, GameObject npc)
	{
//		PlayArea pa = player.GetComponent<PlayArea>();
//		if(pa.PhaseEnd)
//		{
//			Debug.Log ("------ SetTransition(Transition.AwakePhaseEnd) ------");
//			npc.GetComponent<DuelControl>().SetTransition(Transition.AwakePhaseEnd);
//			pa.PhaseEnd = false;
//		}
	}

	public override void Act (GameObject player, GameObject npc)
	{
	}

	public override void DoBeforeEntering ()
	{
		EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_AWAKE_START);
//		VersusView vsview = PoolManager.Instance.GetView(Resconfig.VERSUS_VIEW) as VersusView;
//		PlayArea selfarea = vsview.areaSelf;
//		selfarea.txtPhase.text = "AwakePhase";
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
//		PlayArea pa = player.GetComponent<PlayArea>();
//		if(pa.TurnStart)
//		{
//			npc.GetComponent<DuelControl>().SetTransition(Transition.TurnStart);
//			pa.TurnStart = false;
//		}
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
	}

	public override void DoBeforeEntering ()
	{
		EventManager.instance.DispatchEvent(EventManager.instance, VersusNotes.VERSUS_DEFENCE_START);
	}
}