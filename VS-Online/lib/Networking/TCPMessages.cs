using MelonLoader.TinyJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSO.lib.Networking
{
	public abstract class TCPMessage
	{
		public abstract TCPMessageType type
		{ get; }

		public class NULL : TCPMessage
		{
			public override TCPMessageType type => TCPMessageType.NULL;
		}

		public class PING : TCPMessage
		{
			public override TCPMessageType type => TCPMessageType.PING;
		}

		public class ID : TCPMessage
		{
			public override TCPMessageType type => TCPMessageType.ID;
			public readonly Guid id;

			public ID(Guid id)
			{
				this.id = id;
			}
		}

		public class CMD : TCPMessage
		{
			public override TCPMessageType type => TCPMessageType.CMD;

			public readonly Guid nonce = Guid.NewGuid();
			public readonly String cmd;
			public readonly Variant data;

			public CMD(string cmd, Variant data)
			{
				this.cmd = cmd;
				this.data = data;
			}
		}

		public class DATA : TCPMessage
		{
			public override TCPMessageType type => TCPMessageType.CMD;

			public readonly Guid nonce;
			public readonly Variant data;

			public DATA(Guid nonce, Variant data)
			{
				this.nonce = nonce;
				this.data = data;
			}
		}
	}

	public enum TCPMessageType
	{
		NULL,
		PING,
		ID,
		CMD,
		DATA
	}

	public class Lobby
	{
		public Guid client_id;
		public String client_name;

		public Lobby (Guid client_id, string client_name)
		{
			this.client_id = client_id;
			this.client_name = client_name;
		}
	}
}
