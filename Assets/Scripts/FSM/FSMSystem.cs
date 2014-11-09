using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
A Finite State Machine System based on Chapter 3.1 of Game Programming Gems 1 by Eric Dybsand

Written by Roberto Cezar Bianchini, July 2010
*/

/// <summary>
/// Place the labels for the Transitions in this enum.
/// Dont' change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
	NullTransition = 0, // Use this transition to represent a non-existing transition in your system
}

/// <summary>
/// Place the labels for the states in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum StateID
{
	NullStateID = 0, // Use this ID to represent a non-existing State in your system
}

/// <summary>
/// This class represents the States in the Finite State System.
/// Each state has a Dictionary with pairs (transiston - state) showing
/// which state the FSM should be if a transition is fired while this state
/// is the current state.
/// Method Reason is used to determine which transition should be fired.
/// </summary>
public abstract class FSMState
{
	protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
	protected StateID stateID;
	public StateID ID { get { return stateID; } }

	public void AddTransition(Transition trans, StateID id)
	{
		// Check if anyone of the args is invalid
		if(trans == Transition.NullTransition)
		{
			Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
			return;
		}

		if(id == StateID.NullStateID)
		{
			Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
			return;
		}

		// Since this is a Deterministic FSM,
		// check if the current transition was already inside the map
		if(map.ContainsKey(trans))
		{
			Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() + 
			               " Impossible to assign to another state");
			return;
		}

		map.Add(trans, id);
	}

	/// <summary>
	/// This method deletes a pair transition-state from this state's map.
	/// If the transition was not inside the state's map, an ERROR message is printed.
	/// </summary>
	public void DeleteTransition(Transition trans)
	{
		// Check for NullTransition
		if(trans == Transition.NullTransition)
		{
			Debug.LogError("FSMState ERROR: NullTransition is not allowed");
			return;
		}

		// Check if the pair is inside the map before deleting
		if(map.ContainsKey(trans))
		{
			map.Remove(trans);
			return;
		}
		Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() + 
		               " was not on the state's transition list");
	}

	/// <summary>
	/// This method returns the new state the FSM should be if
	/// this state receives a transition and 
	/// </summary>
	public StateID GetOutputState(Transition trans)
	{
		// Check if the map has this transition
		if(map.ContainsKey(trans))
		{
			return map[trans];
		}
		return StateID.NullStateID;
	}

	/// <summary>
	/// This method is used to set up the State condition before entering it
	/// It is called automatically by the FSMSystem class before assigning it
	/// to the current state
	/// </summary>
	public virtual void DoBeforeEntering() {}

	/// <summary>
	/// This method is used to make anything necessary, as reseting variables
	/// before the FSMSystem changes to another one. It is called automatically
	/// by the FSMSystem before changint to a new state
	/// </summary>
	public virtual void DoBeforeLeaving() {}

	/// <summary>
	/// This method decides if the state should transition to another on its list
	/// NPC is a reference to the object that is controlled by this class
	/// </summary>
	public abstract void Reason(GameObject player, GameObject npc);

	/// <summary>
	/// This method controls the behavior of the NPC in the game World.
	/// Every action, movement or communication the NPC does should be placed here
	/// NPC is a reference to the object that is controlled by this class
	/// </summary>
	public abstract void Act(GameObject player, GameObject npc);

} // class FSMState



public class FSMSystem  
{
	private List<FSMState> states;

	// The only way one can change the state of the FSM is by preforming a transition
	// Don't change the CurrentState directly
	private StateID currentStateID;
	public StateID CurrentStateID { get { return currentStateID; } }
	private FSMState currentState;
	public FSMState CurrentState { get { return currentState; } }

	public FSMSystem()
	{
		states = new List<FSMState>();
	}

	/// <summary>
	/// This method places new states inside the FSM,
	/// or prints an ERROR message if the state was already inside the List.
	/// First state added is also the initial state.
	/// </summary>
	public void AddState(FSMState s)
	{
		// Check for Null reference before deleting
		if(s == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
			return;
		}

		// First State inserted is also the Initial state,
		// the state the machine is in when the simulate begins
		if(states.Count == 0)
		{
			states.Add(s);
			currentState = s;
			currentStateID = s.ID;
			return;
		}

		// Add the state to the List if it's not inside it
		foreach(FSMState state in states)
		{
			if(state.ID == s.ID)
			{
				Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() + "because state has alreay been added");
				return;
			}
			states.Add(s);
		}
	}


	public void DeleteState(StateID id)
	{
		if(id == StateID.NullStateID)
		{

		}
	}
}





















