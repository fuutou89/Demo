using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using Core.Interface;

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

	class EventReference
	{
		private WeakReference _weakReference;
		private Delegate _listener;
		public Delegate listener
		{
			get
			{
				if(_listener != null)
				{
					return _listener;
				}
				else if(_weakReference != null)
				{
					return _weakReference.Target as Delegate;
				}
				return null;
			}
		}
		private bool _isOnce;
		public bool isOnce { get { return _isOnce; }}
		public EventReference(Delegate listener, bool useWeakReference, bool isOnce)
		{
			if(useWeakReference)
			{
				_weakReference = new WeakReference(listener);
			}
			else
			{
				_listener = listener;
			}
			_isOnce = isOnce;
		}
	}

	public class EventManager:ITickObject
	{
		public float weakReferenceGCDelay = 30f;
		private float _weakReferenceGCTime = 0.0f;
		public bool LimitQueueProcesing = false;
		public float QueueProcessTime = 0.0f;	
		private Hashtable _listenerTable = new Hashtable();
		private Queue _eventQueue = new Queue();
		private object lockObjcet = new object();

		private static EventManager _instance = null;
		public static EventManager instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new EventManager();
				}

				return _instance;
			}
		}

		public void Init()
		{
			Engine.AddTickObject(this);
		}

		private List<EventReference> _GetEventListenerList(object context, string eventName, bool createIfNotExist = false)
		{
			if(!_listenerTable.ContainsKey(context))
			{
				if(!createIfNotExist){ return null; }
				_listenerTable.Add(context, new Dictionary<string, List<EventReference>>());
			}
			IDictionary<string, List<EventReference>> dictionary = _listenerTable[context] as IDictionary<string, List<EventReference>>;
			if(!dictionary.ContainsKey(eventName))
			{
				if(!createIfNotExist){ return null; }
				dictionary[eventName] = new List<EventReference>();
			}
			return dictionary[eventName];
		}

		public static bool HasListener(object context, string eventName, Delegate listener)
		{
			return instance.HasEventListener(context, eventName, listener);
		}

		public bool HasEventListener(object context, string eventName, Delegate listener)
		{
			if(!_listenerTable.ContainsKey(context)) 
				return false;

			List<EventReference> list = _GetEventListenerList(context, eventName);
			if(list == null)
			{
				return false;
			}
			if(!list.Exists(l => l.listener == listener))
			{
				return false;
			}
			return true;
		}

		public static bool AddListener<T>(object context, string eventName, T Listener, bool useWeakReference = false, bool isOnce = false)
		{
		}

		public static 
	}
}

























