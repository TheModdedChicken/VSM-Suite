using MelonLoader;
using UnityEngine;
using VSO.lib.Networking;
using VSO.lib.UI;

namespace VSO
{
	public class Mod : MelonMod
	{
		public Server? _server;

		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("VSOnline has been initialized!");
			_server = new GameObject().AddComponent<Server>();
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			if (sceneName == "MainMenu")
			{
				Online.Initialize();
			}
		}
	}
}
