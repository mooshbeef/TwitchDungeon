﻿using System;
using System.Linq;
using TwitchDungeon.DataStorage.Permissions;
using TwitchDungeon.Services.DataStorage;

namespace TwitchDungeon.Services.Commands
{
	public sealed class CommandInfo
	{
		/// <summary>
		/// Gets the user who is the main focus of the command
		/// </summary>
		public User User { get; }

		/// <summary>
		/// The channel the command is being executed in
		/// </summary>
		public Channel Channel { get; }

		/// <summary>
		/// Gets or sets the entity holding the permissions available for the command
		/// </summary>
		public Authorizer Authorizer { get; set; }
		
		/// <summary>
		/// The name of the command
		/// </summary>
		public string CommandName { get; }

		/// <summary>
		/// The string containing the arguments for the command
		/// </summary>
		public string ArgumentText { get; }


		public CommandInfo(Channel channel, User sender, string commandName, string argumentText) : this(channel, sender, sender, commandName, argumentText)
		{

		}

		public CommandInfo(Channel channel, User sender, User authorizer, string commandName, string argumentText)
		{
			Channel = channel;
			User = sender;
			Authorizer = authorizer;
			CommandName = commandName;
			ArgumentText = argumentText;
		}
	}
}