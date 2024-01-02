using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VSO.lib.Utility
{
	internal class ObjectUtil
	{
		public static void RemoveAllListeners (UnityEventBase objectEvent)
		{
			objectEvent.RemoveAllListeners();

			/*for (var i = 0; i < objectEvent.GetPersistentEventCount(); i++)
				objectEvent.SetPersistentListenerState(i, UnityEventCallState.Off);*/
		}
	}
}
