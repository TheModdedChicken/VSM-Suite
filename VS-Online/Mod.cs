using MelonLoader;
using UnityEngine;
using VSO.lib.Networking;
using VSO.lib.UI.Views;

namespace VSO
{
    public class Mod : MelonMod
	{
		public Client? _client;

		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("VSOnline has been initialized!");
			_client = new GameObject("MainMenu").AddComponent<Client>();
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			if (sceneName == "MainMenu")
			{
				Online.InitializeMainMenu();
			}
		}
	}
}
