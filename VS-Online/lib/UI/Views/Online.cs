using Il2CppI2.Loc;
using Il2CppTMPro;
using Il2CppVampireSurvivors.App.Tools;
using Il2CppVampireSurvivors.UI;
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

namespace VSO.lib.UI.Views
{
    internal class Online
    {
		static MelonLogger.Instance _logger = Melon<Mod>.Logger;

		public static void InitializeMainMenu()
        {
            // Create lobby options container
			GameObject lobbyOptions_container = new GameObject("LobbyOptions");
			VSMF.UI.Object.SetParent(GameObject.Find("UI/Canvas - App/Safe Area/View - CharacterSelection"), lobbyOptions_container, new Vector3(1, 1, 1));
			lobbyOptions_container.transform.localPosition = new Vector3(610, 425, 0);
			CreateLobbyOptions(lobbyOptions_container);

			// Create lobby hosting container
			/*GameObject hostingOptions_container = new GameObject("HostingOptions");
			VSMF.UI.Object.SetParent(GameObject.Find("UI/Canvas - App/Safe Area/View - CharacterSelection"), hostingOptions_container, new Vector3(1, 1, 1));
			hostingOptions_container.transform.localPosition = new Vector3(610, 425, 0);
			CreateLobbyOptions(hostingOptions_container);*/
		}

		static void CreateLobbyOptions (GameObject container)
		{
			// Create host lobby button
			GameObject hostLobby = VSMF.UI.Button.CreateNormal("Host Lobby", VSMF.UI.ButtonType.Normal, 300, 100);
			VSMF.UI.Object.SetParent(container, hostLobby, new Vector3(1, 1, 1));
			hostLobby.name = "HostLobbyButton";
			hostLobby.SetActive(false);
			hostLobby.transform.localPosition = new Vector3(90, -25, 0);
			TextMeshProUGUI hostLobby_text = hostLobby.GetComponentInChildren<TextMeshProUGUI>();
			hostLobby_text.fontSizeMax = 20;
			Button hostLobby_button = hostLobby.GetComponent<Button>();

			// Create close lobby button
			GameObject closeLobby = VSMF.UI.Button.CreateNormal("Close Lobby", VSMF.UI.ButtonType.Normal, 300, 100);
			VSMF.UI.Object.SetParent(container, closeLobby, new Vector3(1, 1, 1));
			closeLobby.name = "CloseLobbyButton";
			closeLobby.SetActive(false);
			closeLobby.transform.localPosition = new Vector3(90, -25, 0);
			TextMeshProUGUI closeLobby_text = closeLobby.GetComponentInChildren<TextMeshProUGUI>();
			closeLobby_text.fontSizeMax = 20;
			Button closeLobby_button = closeLobby.GetComponent<Button>();

			// Add event listeners
			hostLobby_button.onClick.AddListener((UnityAction)(() =>
			{
				Melon<Mod>.Instance._client?.CreateLobby();
				hostLobby.SetActive(false);
				closeLobby.SetActive(true);
			}));

			UnityAction closeLobbyAction = (UnityAction)(() =>
			{
				Melon<Mod>.Instance._client?.LeaveLobby();
				hostLobby.SetActive(true);
				closeLobby.SetActive(false);
			});

			closeLobby_button.onClick.AddListener(closeLobbyAction);
			GameObject.Find("UI/Canvas - App/Safe Area/View - MenuBanner/Banner/BackButton").GetComponent<Button>().onClick.AddListener(closeLobbyAction);

			GameObject enterCoop = GameObject.Find("/UI/Canvas - App/Safe Area/View - CharacterSelection/Panel - EnterCoop/EnterCoopButton");

			Button enterCoop_button = enterCoop.GetComponent<Button>();
			enterCoop_button.onClick.AddListener(
				(UnityAction)(() => { hostLobby.SetActive(true); })
			);

			ObjectEvents objectEventsComponent = enterCoop.AddComponent<ObjectEvents>();
			objectEventsComponent.onEnable.AddListener(
				(UnityAction)(() => { hostLobby.SetActive(false); })
			);
		}
		
		static void CreateHostingOptions (GameObject container)
		{
			// Create host lobby button
			GameObject hostLobby = VSMF.UI.Button.CreateNormal("Start Hosting", VSMF.UI.ButtonType.Confirm, 300, 100);
			VSMF.UI.Object.SetParent(container, hostLobby, new Vector3(1, 1, 1));
			hostLobby.name = "HostLobbyButton";
			hostLobby.SetActive(false);
			hostLobby.transform.localPosition = new Vector3(90, -25, 0);
			TextMeshProUGUI hostLobby_text = hostLobby.GetComponentInChildren<TextMeshProUGUI>();
			hostLobby_text.fontSizeMax = 20;
			Button hostLobby_button = hostLobby.GetComponent<Button>();
		}
    }
}
