using UnityEngine;
using System.Collections;

public class DuelControl : MonoBehaviour 
{
	public PlayArea duelArea;
	private FSMSystem fsm;

	public void SetTransition(Transition t) {fsm.PerformTransition(t);}

	// Use this for initialization
	void Start () 
	{
		MakeFSM();
	}

	public void FixedUpdate()
	{
		fsm.CurrentState.Reason(duelArea.gameObject, gameObject);
		fsm.CurrentState.Act(duelArea.gameObject, gameObject);
	}

	private void MakeFSM()
	{
		AwakePhase phase1 = new AwakePhase();
		phase1.AddTransition(Transition.AwakePhaseEnd, StateID.Draw);

		DrawPhase phase2 = new DrawPhase();
		phase2.AddTransition(Transition.DrawPhaseEnd, StateID.Energy);

		EnergyPhase phase3 = new EnergyPhase();
		phase3.AddTransition(Transition.EnergyPhaseEnd, StateID.Main);

		MainPhase phase4 = new MainPhase();
		phase4.AddTransition(Transition.MainPhaseEnd, StateID.Attack);

		AttackPhase phase5 = new AttackPhase();
		phase5.AddTransition(Transition.AttackPhaseEnd, StateID.End);

		EndPhase phase6 = new EndPhase();
		phase6.AddTransition(Transition.TrunEnd, StateID.Defence);

		DefenceState defstate = new DefenceState();
		defstate.AddTransition(Transition.TurnStart, StateID.Awake);

		fsm = new FSMSystem();
		fsm.AddState(defstate);
		fsm.AddState(phase1);
		fsm.AddState(phase2);
		fsm.AddState(phase3);
		fsm.AddState(phase4);
		fsm.AddState(phase5);
		fsm.AddState(phase6);
	}
}
