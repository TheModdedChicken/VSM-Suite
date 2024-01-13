using Il2Cpp;
using Il2CppSteamworks;
using Il2CppSystem.Net;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VSO.lib.Networking
{
	[RegisterTypeInIl2Cpp]
	public class Client : MonoBehaviour
	{
		MelonLogger.Instance _logger = Melon<Mod>.Logger;
		TCPServer _server = new();
		TCPClient _client = new();

		public Client (IntPtr ptr) : base(ptr)
		{
			if (SteamManager.Initialized)
			{
				string displayName = SteamFriends.GetPersonaName();
				_logger.Msg($"Logged in as '{displayName}'");
			}
		}

		public void CreateLobby (int port = 10190)
		{
			_server.Listen(port);
		}

		public void LeaveLobby ()
		{
			_server.Close();
		}
	}
}
