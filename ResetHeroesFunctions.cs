using System;
using System.Reflection;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace ResetHeroes
{
    public static class ResetHeroesFunctions
    {
		public static void ResetMainHero()
		{
			Hero hero = Hero.MainHero;
			hero.HeroDeveloper.ClearHero();

			PropertyInfo propertyInfo = typeof(HeroDeveloper).GetProperty("TotalXp");
			propertyInfo.SetValue(hero.HeroDeveloper, 1, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

			HeroHelper.DetermineInitialLevel(hero);

			hero.HeroDeveloper.UnspentAttributePoints = 18;
			hero.HeroDeveloper.UnspentFocusPoints = 12;

			for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
			{
				hero.SetAttributeValue(characterAttributesEnum, 0);
			}

			InformationManager.DisplayMessage(new InformationMessage(string.Format("Reset {0}", hero)));
		}
	}
}
