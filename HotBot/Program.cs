﻿using HotBot.Core;
using HotBot.Core.Commands;
using HotBot.Core.Irc;
using HotBot.Core.Irc.Impl;
using HotBot.Core.Plugins;
using HotBot.Plugins.Lottery;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Linq;

namespace HotBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			UnityContainer container = CreateContainer();
			InitializeConfig(container);
			InitializeInstances(container);
		}

		private static UnityContainer CreateContainer()
		{
			var container = new UnityContainer();
			var section = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
			section.Configure(container);
			container.RegisterInstance(typeof(IUnityContainer), container, new ContainerControlledLifetimeManager());
			return container;
		}
		
		private static void InitializeConfig(UnityContainer container)
		{
			MasterConfig config = new MasterConfig();
			container.RegisterInstance(config, new ContainerControlledLifetimeManager());
			container.RegisterInstance<CommandManagerConfig>(config, new ExternallyControlledLifetimeManager());
		}

		private static void InitializeInstances(UnityContainer container)
		{
			container.Resolve<ChatCommandScanner>();
			var bot = container.Resolve<TwitchBot>();
			bot.Run();
		}
	}
}