using System.Reflection;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace ResetAndReallocateHeroes
{
    public static class ResetAndReallocateHeroesFunctions
	{
		public static void ResetHero(Hero hero)
		{
			hero.HeroDeveloper.ClearHero();

			PropertyInfo propertyInfo = typeof(HeroDeveloper).GetProperty("TotalXp");
			propertyInfo.SetValue(hero.HeroDeveloper, 0, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

			HeroHelper.DetermineInitialLevel(hero);

			hero.HeroDeveloper.UnspentAttributePoints = CalcUnspentAttr(hero.Level);
			hero.HeroDeveloper.UnspentFocusPoints = CalcUnspentFocus(hero.Level);

			for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
			{
				hero.SetAttributeValue(characterAttributesEnum, 0);
			}

			InformationManager.DisplayMessage(new InformationMessage(string.Format("Reset {0}", hero)));
		}



		public static void ReallocateHero(Hero hero)
		{
			hero.HeroDeveloper.UnspentFocusPoints = CalcUnspentFocus(hero.Level);
			typeof(HeroDeveloper).GetMethod("ClearFocuses", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(hero.HeroDeveloper, new object[0]);

			for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
			{
				hero.SetAttributeValue(characterAttributesEnum, 0);
			}

			hero.HeroDeveloper.UnspentAttributePoints = CalcUnspentAttr(hero.Level);
			InformationManager.DisplayMessage(new InformationMessage(string.Format("{0} (Level {1}) is ready for reallocation", hero.Name,hero.Level)));
			InformationManager.DisplayMessage(new InformationMessage(string.Format("(Unallocated: {0} ATR | {1} FOCUS)", hero.HeroDeveloper.UnspentAttributePoints, hero.HeroDeveloper.UnspentFocusPoints)));

			hero.ClearPerks();
			typeof(HeroDeveloper).GetMethod("DiscoverOpenedPerks", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(hero.HeroDeveloper, new object[0]);
		}



		private static int CalcUnspentAttr(int lvl)
		{
			return 18 + (int)(lvl / Ref_DefaultCharacterDevelopmentModel.LvlPerAttr);
		}



		private static int CalcUnspentFocus(int lvl)
		{
			return 12 + ( (lvl - 1) * Ref_DefaultCharacterDevelopmentModel.FocusPerLvl);
		}
	}
}
