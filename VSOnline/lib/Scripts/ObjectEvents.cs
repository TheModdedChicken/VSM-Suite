using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using UnityEngine.Events;
using Il2CppInterop.Runtime;

namespace VSOnline.lib.Scripts
{
	[RegisterTypeInIl2Cpp]
	public class ObjectEvents : MonoBehaviour
	{
		public ObjectEvents(IntPtr ptr) : base(ptr) {}

		public EnableEvent onEnable = new();
		public EnableEvent onDisable = new();

		void OnEnable ()
		{
			onEnable.Invoke();
		}

		void OnDisable()
		{
			onDisable.Invoke();
		}
	}

	public class EnableEvent : UnityEvent
	{
		public EnableEvent() { }
	}
}
