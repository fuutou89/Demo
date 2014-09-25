using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureMVC.Interfaces
{
	public interface IModule
	{
		void Init();
		void Init(object data);
		void Init(object data, Action initComplete);
		void Destory();

		void Notify(INotification notification);

		IProxy proxy { get; }
		IMediator mediator { get; }
		string moduleName { get; }
	}
}
