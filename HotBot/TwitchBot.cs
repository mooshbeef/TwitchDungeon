﻿using HotBot.Core;
using HotBot.Core.Irc;
using HotBot.Core.Plugins;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace HotBot
{
	//http://tmi.twitch.tv/group/user/maritaria/chatters
	public class TwitchBot
	{
		public static readonly string Hostname = "irc.twitch.tv";
		public static readonly UInt16 Port = 6667;

		private object _consoleLock = new object();
		[Dependency]
		public MessageBus Bus { get; set; }

		[Dependency]
		public MasterConfig MasterConfig { get; set; }

		[Dependency]
		public PluginManager PluginManager { get; set; }

		[Dependency]
		public TwitchConnector ChatConnector { get; set; }
		
		[Dependency]
		public TwitchApi TwitchApi { get; set; }

		public Channel PrimaryChannel { get; } = new Channel("maritaria");

		public TwitchBot()
		{
		}

		public void Run()
		{
			JoinPrimaryChannel();
			Bus.Subscribe(this);
			PluginManager.LoadAll();
		}

		private void JoinPrimaryChannel()
		{
			ChatConnector.DefaultCredentials = new Credentials { AuthKey = MasterConfig.AuthKey, Username = MasterConfig.Username };
			var channel = ChatConnector.GetConnection(PrimaryChannel);
			channel.Join();
			channel.Say(@"/me is now online");
			ChatConnector.WhisperServer.WhisperReceived += WhisperServer_WhisperReceived;
		}

		private void WhisperServer_WhisperReceived(object sender, WhisperEventArgs e)
		{
			Console.WriteLine($"Whisper> {e.Sender.Name} {e.Message}");
		}
	}
}