using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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
}
