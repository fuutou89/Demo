using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NotificationCenter : INotificationCenter
{
	private static INotificationCenter singleton;

//	private event EventHandler GameOver;
//	private event EventHandler ScoreAdd;

	private NotificationCenter() : base()
	{
//		eventTable["GameOver"] = GameOver;
//		eventTable["ScoreAdd"] = ScoreAdd;
	}

	public static INotificationCenter GetInstance()
	{
		if(singleton == null)
			singleton = new NotificationCenter();
		return singleton;
	}
}


/// <summary>
/// Abstract Class of NotificationCenter
/// </summary>
public abstract class INotificationCenter 
{
	protected Dictionary<string, EventHandler> eventTable;

	protected INotificationCenter()
	{
		eventTable = new Dictionary<string, EventHandler>();
	}

	// PostNotification -- Send out message with "name" and EventArgs "e" by "sender"
	public void PostNotification(string name)
	{
		this.PostNotification(name, null, EventArgs.Empty);
	}

	public void PostNotification(string name, object sender)
	{
		this.PostNotification(name, sender, EventArgs.Empty);
	}

	public void PostNotification(string name, object sender, EventArgs e)
	{
		if(eventTable[name] != null)
		{
			eventTable[name](sender, e);
		}
	}

	// Add or remove a EventHandler
	public void AddEventHandler(string name, EventHandler handler)
	{
		if(eventTable.ContainsKey(name))
		{
			eventTable[name] += handler;
		}
		else
		{
			eventTable.Add(name, handler);
		}
	}

	public void RemoveEventHandler(string name, EventHandler handler)
	{
		eventTable[name] -= handler;
	}
}
