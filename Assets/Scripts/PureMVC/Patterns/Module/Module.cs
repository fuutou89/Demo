#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Patterns
{
	public abstract class Module : IModule
	{
		public static string NAME = "Module";

		protected Action _initComplete;
		protected object _data;

		private string _moduleName;
		private IProxy _proxy;
		private IMediator _mediator;
		private IDictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

		public Module() : this (NAME)
		{
		}

		public Module(string moduleName)
		{
			_moduleName = moduleName;
		}

		public void Init()
		{
			Init(null, null);
		}

		public void Init(object data)
		{
			Init(data, null);
		}

		public void Init(object data, Action initComplete)
		{
			_initComplete = initComplete;
			_data = data;

			_Init();
		}

		protected abstract void _Start();

		protected virtual void _Init()
		{
			_Start();

			if(_initComplete != null)
			{
				_initComplete();
				_initComplete = null;
			}
		}

		public void Destory()
		{
			_Dispose();
			_Destory();
		}

		protected abstract void _Dispose();

		private void _Destory()
		{
			_initComplete = null;
			_data = null;
		}

		protected void _RegisterProxy(IProxy proxy)
		{
			if (proxy == null)
			{
				Log.Error("Module:_RegisterProxy - proxy is null");
				return;
			}
			if (_proxy != null)
			{
				Log.Error("Module:_RegisterProxy - _proxy already exist, Don't regist twice");
				return;
			}
			_proxy = proxy;
			_proxy.OnRegister();
		}
		
		protected void _RegisterMediator(IMediator mediator)
		{
			if (mediator == null)
			{
				Log.Error("Module:_RegisterMediator - mediator is null");
				return;
			}
			if (_mediator != null)
			{
				Log.Error("Module:_RegisterMediator - _mediator already exist, Don't regist twice");
				return;
			}
			_mediator = mediator;
			_mediator.OnRegister();
		}
		
		protected void _RegisterCommand(string notificationName, ICommand command)
		{
			if (_commands.ContainsKey(notificationName))
			{
				Log.Error("Module:_RegisterCommand - _commands already contains notification [" + notificationName + "]");
				return;
			}
			_commands.Add(notificationName, command);
		}
		
		protected void _RemoveProxy()
		{
			if (_proxy == null)
			{
				Log.Error("Module:_RemoveProxy - _proxy not exist");
				return;
			}
			_proxy.OnRemove();
			_proxy = null;
		}
		
		protected void _RemoveMediator()
		{
			if (_mediator == null)
			{
				Log.Error("Module:_RemoveMediator - _mediator not exist");
				return;
			}
			_mediator.OnRemove();
			_mediator = null;
		}
		
		protected void _RemoveCommand(string notificationName)
		{
			if (!_commands.ContainsKey(notificationName))
			{
				Log.Error("Module:_RemoveCommand - _commands do not contains notification [" + notificationName + "]");
				return;
			}
			_commands.Remove(notificationName);
		}
		
		public void Notify(INotification notification)
		{
			if (_mediator != null) _mediator.HandleNotification(notification);
			if (_commands.ContainsKey(notification.Name)) _commands[notification.Name].Execute(notification);
		}
		
		public IProxy proxy { get { return _proxy; } }
		public IMediator mediator { get { return _mediator; } }
		public string moduleName { get { return _moduleName; } }
	}
}
