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
			if (Campaign.Current == null)
			{
				return "Command can only be used in the campaign mode (not in the mainmenu).";
			}
			else
			{
				ResetHeroesFunctions.ResetHero(Hero.MainHero);
				return "Main hero successfully reset";
			}
		}

		[CommandLineFunctionality.CommandLineArgumentFunction("resetCompanion", "campaign")]
		public static string ResetCompanion(List<string> strings)
		{
			if (Campaign.Current == null)
			{
				return "Command can only be used in the campaign mode (not in the mainmenu).";
			}
			else
			{
				if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
				{
					return "Format is \"campaign.resetCompanion [Hero Name]\"";
				}
				else
				{
					string heroName = string.Join(" ", strings.ToArray()).Trim(new char[]
					{
					'"'
					});
					if (Hero.MainHero.Name.ToString().ToLower().Contains(heroName.ToLower()))
					{
						ResetHeroesFunctions.ResetHero(Hero.MainHero);
						return string.Format("Hero {0} successfully reset", heroName);
					}
					else
					{
						Hero hero = Hero.MainHero.Clan.Companions.FirstOrDefault((Hero h) => h.Name.ToString().ToLower().Contains(heroName.ToLower()));
						if (hero == null)
						{
							hero = Hero.MainHero.Clan.Heroes.FirstOrDefault((Hero f) => f.Name.ToString().ToLower().Contains(heroName.ToLower()));
							if (hero == null)
							{
								return "No matching companion or family member found";
							}
							else
							{
								if (hero.PartyBelongedTo == null || !hero.PartyBelongedTo.IsMainParty)
								{
									return "Hero must be in current party";
								}
								else
								{
									ResetHeroesFunctions.ResetHero(hero);
									return string.Format("Hero {0} successfully reset", hero);
								}
							}
						}
						else
						{
							if (!Hero.MainHero.CompanionsInParty.Contains(hero))
							{
								return "Hero must be in current party";
							}
							else
							{
								ResetHeroesFunctions.ResetHero(hero);
								return string.Format("Hero {0} successfully reset", hero);
							}
						}
					}
				}
			}
		}
	}
}
