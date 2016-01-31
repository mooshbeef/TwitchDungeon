﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDungeon.Services.Irc
{
	public class IrcLogger : MessageHandler<IrcMessageReceived>
	{
		public IrcLogger(MessageBus bus)
		{
			bus.Subscribe(this);
		}
		
		public void HandleMessage(IrcMessageReceived ircMessage)
		{
			Console.WriteLine(ircMessage.Message);
		}
	}
}
