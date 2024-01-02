using Harmony;
using MelonLoader;
using UnityEngine;

namespace VSMF
{
	public class Plugin : MelonPlugin
	{
		public override void OnInitializeMelon()
		{
			HarmonyInstance.PatchAll(MelonAssembly.Assembly);
		}
	}
}
