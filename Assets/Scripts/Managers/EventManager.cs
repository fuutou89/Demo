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

	class Event
	{
		private object _context;
		public object context
		{
			get { return _context; }
		}
		private string _name;
		public string name
		{
			get { return _name; }
		}
		private object[] _data;
		public object[] data
		{
			get { return _data; }
		}
		public Event(object context, string name, object[] data)
		{
			_context = context;
			_name = name;
			_data = data;
		}
	}

}
