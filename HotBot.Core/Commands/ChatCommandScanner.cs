﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using HotBot.Core.Irc;
using HotBot.Core.Util;

namespace HotBot.Core.Commands
{
	public class ChatCommandScanner
	{
		public MessageBus Bus { get; }
		public CommandConfig Config { get; }

		public ChatCommandScanner(MessageBus bus, CommandConfig config)
		{
			if (bus == null)
			{
				throw new ArgumentNullException("bus");
			}
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			Bus = bus;
			Bus.Subscribe(this);
			Config = config;
		}

		[Subscribe]
		public void HandleMessage(ChatReceivedEvent message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (ShouldDecode(message))
			{
				var commandInfo = Decode(message);
				Bus.PublishSpecific(commandInfo);
			}
		}

		private bool ShouldDecode(ChatReceivedEvent message)
		{
			if (message.Message.Length > 0)
			{
				foreach (string prefix in Config.Prefixes)
				{
					if (message.Message.StartsWith(prefix))
					{
						return true;
					}
				}
			}
			return false;
		}

		private CommandEvent Decode(ChatReceivedEvent message)
		{
			string content = message.Message.Trim();
			content = RemovePrefix(content);
			string[] parts = content.SplitOnce(" ", "\t");
			string commandName = parts[0];
			string argumentText = parts[1];
			CommandEvent commandInfo = new CommandEvent(message.Channel, message.User, commandName, argumentText);
			return commandInfo;
		}

		private string RemovePrefix(string text)
		{
			foreach(string prefix in Config.Prefixes)
			{
				if (text.StartsWith(prefix))
				{
					return text.Substring(prefix.Length);
				}
			}
			throw new InvalidOperationException("string does not start with known prefix");
		}
	}
}