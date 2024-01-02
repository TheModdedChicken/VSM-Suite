using Il2CppI2.Loc;
using Il2CppSteamworks;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VSO.lib.Scripts;
using VSO.lib.Utility;

namespace VSO.lib.UI
{
	internal class Online
	{
		public static GameObject Initialize ()
		{
			GameObject confirmButton = GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection/ConfirmButton");

			GameObject enableOnlineButton = UnityEngine.Object.Instantiate(confirmButton, GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection").transform);
			enableOnlineButton.SetActive(false);
			enableOnlineButton.name = "EnableOnlineButton";
			enableOnlineButton.transform.position = new Vector3(2.45f, 1.4f, -9f);
			enableOnlineButton.transform.localScale = new Vector3(1f, 1f, 1f);
			Button enableOnlineButtonComponent = enableOnlineButton.GetComponent<Button>();
			enableOnlineButtonComponent.GetComponentInChildren<Localize>().mTerm = "lang/multiplayer_enable_online";
			ObjectUtil.RemoveAllListeners(enableOnlineButtonComponent.onClick);

			GameObject disableOnlineButton = UnityEngine.Object.Instantiate(confirmButton, GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection").transform);
			disableOnlineButton.SetActive(false);
			disableOnlineButton.name = "DisableOnlineButton";
			disableOnlineButton.transform.position = new Vector3(2.45f, 1.4f, -9f);
			disableOnlineButton.transform.localScale = new Vector3(1f, 1f, 1f);
			Button disableOnlineButtonComponent = disableOnlineButton.GetComponent<Button>();
			disableOnlineButtonComponent.GetComponentInChildren<Localize>().mTerm = "lang/multiplayer_disable_online";
			ObjectUtil.RemoveAllListeners(disableOnlineButtonComponent.onClick);


			enableOnlineButtonComponent.onClick.AddListener((UnityAction)(() => {
				Melon<Mod>.Instance._server?.CreateLobby();
				enableOnlineButton.SetActive(false);
				disableOnlineButton.SetActive(true);
			}));

			UnityAction disableOnline = (UnityAction)(() =>
			{
				Melon<Mod>.Instance._server?.LeaveLobby();
				enableOnlineButton.SetActive(true);
				disableOnlineButton.SetActive(false);
			});

			disableOnlineButtonComponent.onClick.AddListener(disableOnline);
			GameObject.Find("UI/Canvas - App/Safe Area/View - MenuBanner/Banner/BackButton").GetComponent<Button>().onClick.AddListener(disableOnline);

			GameObject enterCoopButton = GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection/Panel - EnterCoop/EnterCoopButton");

			Button enterCoopButtonComponent = enterCoopButton.GetComponent<Button>();
			enterCoopButtonComponent.onClick.AddListener(
				(UnityAction)(() => { enableOnlineButton.SetActive(true); })
			);

			ObjectEvents objectEventsComponent = enterCoopButton.AddComponent<ObjectEvents>();
			objectEventsComponent.onEnable.AddListener(
				(UnityAction)(() => { enableOnlineButton.SetActive(false); })
			);


			return enableOnlineButton;
		}
	}
}
