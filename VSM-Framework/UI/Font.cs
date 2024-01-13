using Il2CppTMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSMF.UI
{
	public class Font
	{
		public static TMP_FontAsset GetDefault()
		{
			return Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Where((f) => f.name == "Courier_HintedSmooth SDF8").First();
		}
	}
}
