using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace Core.Manager
{
	//C# Unity event manager that uses string in a hashtabel over delegates and events in order to 
	//allow use of events without knowing where and when they're declared/defined

	public delegate void EventListener(params object[] args);


}
