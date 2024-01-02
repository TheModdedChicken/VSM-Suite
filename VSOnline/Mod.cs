using MelonLoader;
using UnityEngine;
using Cpp2ILInjected;
using UnityEngine.UI;
using Il2CppI2.Loc;
using VSOnline.lib;
using VSOnline.lib.Scripts;
using Il2CppSystem;
using Action = Il2CppSystem.Action;
using UnityEngine.Events;

namespace VSOnline
{
	public class Mod : MelonMod
	{
		public override void OnInitializeMelon()
		{
			LoggerInstance.Msg("VSOnline has been loaded!");
		}

		public override void OnSceneWasInitialized(int buildIndex, string sceneName)
		{
			if (sceneName == "MainMenu")
			{
				GameObject confirmButton = GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection/ConfirmButton");
				GameObject enableOnlineButton = UnityEngine.Object.Instantiate(confirmButton, GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection").transform);
				enableOnlineButton.SetActive(false);
				enableOnlineButton.name = "EnableOnlineButton";
				enableOnlineButton.transform.position = new Vector3(2.61f, 1.5f, -9f);
				enableOnlineButton.transform.localPosition = new Vector3(687f, 433.5526f, 0f);
				enableOnlineButton.transform.localScale = new Vector3(1f, 1f, 1f);
				enableOnlineButton.GetComponent<Button>().GetComponentInChildren<Localize>().mTerm = "lang/multiplayer_enable_online";

				GameObject enterCoopButton = GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection/Panel - EnterCoop/EnterCoopButton");
				ObjectEvents objectEventsComponent = enterCoopButton.AddComponent<ObjectEvents>();

				Button enterCoopButtonComponent = enterCoopButton.GetComponent<Button>();
				enterCoopButtonComponent.onClick.AddListener(
					(UnityAction)(() => { enableOnlineButton.SetActive(true); })
				);

				//ObjectEvents objectEvents = enterCoopButton.GetComponent<ObjectEvents>();
				objectEventsComponent.onEnable.AddListener(
					(UnityAction)(() => { enableOnlineButton.SetActive(false); })
				);
			}
		}
	}
}
