using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace ResetHeroes
{
    public static class ConsoleCommands
    {
		[CommandLineFunctionality.CommandLineArgumentFunction("resetMainHero", "campaign")]
		public static string ResetMainHero(List<string> strings)
		{
			bool flag = Campaign.Current == null;
			string result;
			if (flag)
			{
				result = "Campaign is null";
			}
			else
			{
				ResetHeroesFunctions.ResetMainHero();

				result = "done" + strings;
			}
			return result;
		}
	}
}
