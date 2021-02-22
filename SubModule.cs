using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace ResetAndReallocateHeroes
{
    public class ResetAndReallocateHeroesSubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            Harmony harmony = new Harmony("ResetAndReallocateHeroes");
            harmony.PatchAll();

        }
    }
}
