using Il2Cpp;
using Il2CppSteamworks;
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
	public class Server : MonoBehaviour
	{
		MelonLogger.Instance _logger = Melon<Mod>.Logger;

		CSteamID _lobby_id = CSteamID.Nil;

		public Server (IntPtr ptr) : base(ptr)
		{
			if (SteamManager.Initialized)
			{
				string displayName = SteamFriends.GetPersonaName();
				_logger.Msg($"Logged in as '{displayName}'");
			}
		}

		public void CreateLobby ()
		{
			SteamAPICall_t handle = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
			_lobby_id = (CSteamID)(ulong)handle;
			_logger.Msg($"Created lobby with ID '{_lobby_id}'");
		}

		public void LeaveLobby ()
		{
			if (_lobby_id != CSteamID.Nil) 
			{
				SteamMatchmaking.LeaveLobby(_lobby_id);
				_logger.Msg($"Left lobby with ID '{_lobby_id}'");
				_lobby_id = CSteamID.Nil;
			}
			else _logger.Msg("Tried to leave lobby but client isn't currently in one.");
		}
	}
}
