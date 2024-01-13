using Il2CppSystem.Runtime.Remoting.Messaging;
using MelonLoader;
using MelonLoader.TinyJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VSO.lib.Networking
{
	internal class TCPClient
	{
		MelonLogger.Instance _logger = Melon<Mod>.Logger;

		public event Action<TCPMessage>? onMessage;
		public event Action? onDisconnect;

		TcpClient _client = new();
		NetworkStream? _stream;
		byte[]? _buffer;

		Lobby? _lobby;

		public TCPClient () { }

		public void Connect(string ip, int port)
		{
			_client.Connect(IPAddress.Parse(ip), port);
			_stream = _client.GetStream();

			// Start asynchronous operation to receive data from the server
			_buffer = new byte[_client.ReceiveBufferSize];
			_stream.BeginRead(_buffer, 0, _buffer.Length, ReceiveData, null);
		}

		public void Disconnect()
		{
			if (_client.Connected)
			{
				_client.Close();
				_stream = null;
				_buffer = null;
				_lobby = null;
			}
			else _logger.Warning($"Tried to disconnect from server, but no connection to a server was found.");
		}

		private void ReceiveData(IAsyncResult result)
		{
			if (_stream != null && _stream.Socket.Connected && _buffer != null)
			{
				int bytesRead = _stream.EndRead(result);
				if (bytesRead <= 0)
				{
					if (onDisconnect != null) onDisconnect.Invoke();
					return;
				}

				byte[] receivedBytes = new byte[bytesRead];
				Array.Copy(_buffer, receivedBytes, bytesRead);

				String data = System.Text.Encoding.UTF8.GetString(receivedBytes);
				_logger.Msg($"Incoming message from server: " + data);

				TCPMessage message;
				JSON.MakeInto(JSON.Load(data), out message);
				if (onMessage != null) onMessage.Invoke(message);

				// Continue the asynchronous operation to receive data
				_stream.BeginRead(_buffer, 0, _buffer.Length, ReceiveData, null);
			}
			else _logger.Warning($"Tried to handle an incoming message from server, but data stream is not available.");
		}

		public void SendData(TCPMessage message)
		{
			if (_stream != null && _stream.Socket.Connected)
			{
				String data = JSON.Dump(message); // Convert message into stringified JSON

				byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(data);
				_stream.Write(sendBytes, 0, sendBytes.Length);
				_stream.Flush();

				_logger.Msg($"Outgoing message to server: " + data);
			}
			else _logger.Warning($"Tried to handle an incoming message from server, but data stream is not available.");
		}
	}
}
