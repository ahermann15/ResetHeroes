using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace ResetAndReallocateHeroes
{
    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), MethodType.Constructor)]
    internal static class Ref_DefaultCharacterDevelopmentModel
    {
        public static void Postfix(DefaultCharacterDevelopmentModel __instance)
        {
            LvlPerAttr = __instance.LevelsPerAttributePoint;
            FocusPerLvl = __instance.FocusPointsPerLevel;
        }

        static public int LvlPerAttr { get; set; } = 0;
        static public int FocusPerLvl { get; set; } = 0;
    }
}
