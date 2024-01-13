using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSMF.UI
{
	public class Object
	{
		public static void SetParent (GameObject parent, GameObject child, Vector3? localScale = null)
		{
			child.transform.parent = parent.transform;
			if (localScale != null) child.transform.localScale = (Vector3)localScale;
		}
	}
}
