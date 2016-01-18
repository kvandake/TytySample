using System;
using CoCore.Base;
using CoIoC.Interface;
using CoIoC.Core;

namespace TytySample
{

	/// <summary>
	/// Контейнер для CoCore
	/// </summary>
	public class CoiocToEasyContainer : IIocContainer
	{

		IContainer container;

		public CoiocToEasyContainer(){
			container = new BasicContainer ();
		}


		#region IIocContainer implementation

		public void Register (Type type, string name = null, params object[] parameters)
		{
			throw new NotImplementedException ();
		}

		public void Register<TImplement> (Func<TImplement> lazyInit)
		{
			container.Register<TImplement> (lazyInit);
		}

		public T Resolve<T> (string name = null)
		{
			return container.Resolve<T> (name);
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			throw new NotImplementedException ();
		}

		#endregion




	}
}

