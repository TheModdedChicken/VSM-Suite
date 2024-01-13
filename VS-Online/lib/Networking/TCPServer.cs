using MelonLoader;
using MelonLoader.TinyJSON;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace VSO.lib.Networking
{
	internal class TCPServer
	{
		MelonLogger.Instance _logger = Melon<Mod>.Logger;

		TcpListener? _listener;
		Dictionary<Guid, TCPConnection> _clients = new();

		public TCPServer () {}

		public void Listen (int port)
		{
			if (_listener == null)
			{
				_listener = new TcpListener(System.Net.IPAddress.Any, port);

				_listener.Start();

				_logger.Msg($"Listening for connections on {_listener.Server.LocalEndPoint}");

				// Start accepting client connections asynchronously
				_listener.BeginAcceptTcpClient(new AsyncCallback(HandleIncomingConnection), null);
			}
			else _logger.Warning("Tried to create a new listener, but a listener already exists. Close the current listener before creating a new one.");
		}

		public void DisconnectClient (Guid id)
		{
			if (_clients.ContainsKey(id))
			{
				_logger.Msg($"Closing connection with client '{id}'");
				_clients[id].client.Close();
				_clients.Remove(id);
			}
			else _logger.Warning($"Tried to disconnect client with ID '{id}', but client doesn't exist.");
		}

		public void Close ()
		{
			if (_listener != null)
			{
				_logger.Msg("Closing all connections...");
				foreach (var id in _clients.Keys) DisconnectClient(id);
				try { _listener.Stop(); } catch { }

				_listener = null;
			}
			else _logger.Warning("Tried to close listener, but a listener hasn't been opened.");
		}

		void HandleIncomingConnection(IAsyncResult result)
		{
			if (_listener != null)
			{
				_logger.Msg("Handling incoming connection...");
				TCPConnection connection = new(
					_listener.EndAcceptTcpClient(result),
					Guid.NewGuid()
				);

				connection.onDisconnect += (Guid id) => { DisconnectClient(id); };

				_clients.Add(connection.id, connection);

				_logger.Msg($"Client connected from '{connection.client.Client.RemoteEndPoint}' with ID '{connection.id}'");

				// Accept next client connection asynchronously
				_listener.BeginAcceptTcpClient(new AsyncCallback(HandleIncomingConnection), null);
			}
			else _logger.Warning("Tried to handle an incoming connection, but a listener was not found.");
		}
	}

	public class TCPConnection
	{
		MelonLogger.Instance _logger = Melon<Mod>.Logger;

		public Guid id { get { return _id; } }
		public TcpClient client { get { return _client; } }

		public event Action<Guid, TCPMessage>? onMessage;
		public event Action<Guid>? onDisconnect;

		Guid _id;
		TcpClient _client;
		NetworkStream _stream;
		byte[] _buffer;

		public TCPConnection (TcpClient client, Guid id)
		{
			_client = client;
			_id = id;
			_stream = _client.GetStream();
			_buffer = new byte[_client.ReceiveBufferSize];

			_stream.BeginRead(_buffer, 0, _buffer.Length, ReceiveData, null);

			SendData(new TCPMessage.ID(_id));
		}

		public void ReceiveData(IAsyncResult result)
		{
			if (_stream.Socket.Connected)
			{
				int bytesRead = _stream.EndRead(result);
				if (bytesRead <= 0)
				{
					if (onDisconnect != null) onDisconnect.Invoke(_id);
					return;
				}

				byte[] receivedBytes = new byte[bytesRead];
				Array.Copy(_buffer, receivedBytes, bytesRead);

				String data = System.Text.Encoding.UTF8.GetString(receivedBytes);
				_logger.Msg($"Incoming message from [{_id}]: " + data);

				TCPMessage message;
				JSON.MakeInto(JSON.Load(data), out message);

				if (onMessage != null) onMessage.Invoke(_id, message);

				// Continue the asynchronous operation to receive data
				_stream.BeginRead(_buffer, 0, _buffer.Length, ReceiveData, null);
			}
			else _logger.Warning($"Tried to handle an incoming message from [{_id}], but data stream is not available.");
		}

		public void SendData(TCPMessage message)
		{
			String data = JSON.Dump(message); // Convert message into stringified JSON

			byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(data);
			_stream.Write(sendBytes, 0, sendBytes.Length);
			_stream.Flush();

			_logger.Msg($"Outgoing message to [{_id}]: " + data);
		}
	}
}
