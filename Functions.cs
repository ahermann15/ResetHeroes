using System.Reflection;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

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

			hero.HeroDeveloper.UnspentAttributePoints = 18;
			hero.HeroDeveloper.UnspentFocusPoints = 12;

			for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
			{
				hero.SetAttributeValue(characterAttributesEnum, 0);
			}

			InformationManager.DisplayMessage(new InformationMessage(string.Format("Reset {0}", hero)));
		}



		public static void ReallocateHero(Hero hero)
		{
			int focusPoints = 0;
			foreach (SkillObject skill in DefaultSkills.GetAllSkills())
			{
				int focus = hero.HeroDeveloper.GetFocus(skill);
				if (focus > 0)
				{
					focusPoints += focus;
				}
			
			}
			hero.HeroDeveloper.UnspentFocusPoints += MBMath.ClampInt(focusPoints, 0, 999);
			typeof(HeroDeveloper).GetMethod("ClearFocuses", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(hero.HeroDeveloper, new object[0]);

			int attrPoints = 0;
			for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
			{
				int attributeValue = hero.GetAttributeValue(characterAttributesEnum);
				attrPoints += attributeValue;
				hero.SetAttributeValue(characterAttributesEnum, 0);
			}
			hero.HeroDeveloper.UnspentAttributePoints += MBMath.ClampInt(attrPoints, 0, 999);
			InformationManager.DisplayMessage(new InformationMessage(string.Format("{0} (Level {1}) is ready for reallocation", hero.Name,hero.Level)));
			InformationManager.DisplayMessage(new InformationMessage(string.Format("(Unallocated: {0} ATR | {1} FOCUS)", hero.HeroDeveloper.UnspentAttributePoints, hero.HeroDeveloper.UnspentFocusPoints)));

			hero.ClearPerks();
			typeof(HeroDeveloper).GetMethod("DiscoverOpenedPerks", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(hero.HeroDeveloper, new object[0]);
		}
	}
}
