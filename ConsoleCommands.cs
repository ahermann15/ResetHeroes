using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace ResetAndReallocateHeroes
{
	public static class ConsoleCommands
	{
		[CommandLineFunctionality.CommandLineArgumentFunction("reset_main_hero", "campaign")]
		public static string ResetMainHero(List<string> strings)
		{
			if (Campaign.Current == null)
			{
				return "Command can only be used in the campaign mode (not in the mainmenu).";
			}
			else
			{
				ResetAndReallocateHeroesFunctions.ResetHero(Hero.MainHero);
				return "Main hero successfully reset";
			}
		}

		[CommandLineFunctionality.CommandLineArgumentFunction("reset_companion", "campaign")]
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
						ResetAndReallocateHeroesFunctions.ResetHero(Hero.MainHero);
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
									ResetAndReallocateHeroesFunctions.ResetHero(hero);
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
								ResetAndReallocateHeroesFunctions.ResetHero(hero);
								return string.Format("Hero {0} successfully reset", hero);
							}
						}
					}
				}
			}
		}

		[CommandLineFunctionality.CommandLineArgumentFunction("reallocate_main_hero", "campaign")]
		public static string ReallocateMainHero(List<string> strings)
		{
			if (Campaign.Current == null)
			{
				return "Command can only be used in the campaign mode (not in the mainmenu).";
			}
			else
			{
				ResetAndReallocateHeroesFunctions.ReallocateHero(Hero.MainHero);
				return "Main hero is ready for reallocation";
			}
		}

		[CommandLineFunctionality.CommandLineArgumentFunction("reallocate_companion", "campaign")]
		public static string ReallocateCompanion(List<string> strings)
		{
			if (Campaign.Current == null)
			{
				return "Command can only be used in the campaign mode (not in the mainmenu).";
			}
			else
			{
				if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
				{
					return "Format is \"campaign.reallocateCompanion [Hero Name]\"";
				}
				else
				{
					string heroName = string.Join(" ", strings.ToArray()).Trim(new char[]
					{
					'"'
					});
					if (Hero.MainHero.Name.ToString().ToLower().Contains(heroName.ToLower()))
					{
						ResetAndReallocateHeroesFunctions.ReallocateHero(Hero.MainHero);
						return string.Format("Hero {0} is ready for reallocation", heroName);
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
									ResetAndReallocateHeroesFunctions.ReallocateHero(hero);
									return string.Format("Hero {0} is ready for reallocation", hero);
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
								ResetAndReallocateHeroesFunctions.ReallocateHero(hero);
								return string.Format("Hero {0} is ready for reallocation", hero);
							}
						}
					}
				}
			}
		}
	}
}
