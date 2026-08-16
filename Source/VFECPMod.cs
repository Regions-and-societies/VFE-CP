using RegionsAndSocieties.Integration;
using Verse;

namespace RegionsAndSocieties.VFECP
{
    /// <summary>
    /// Mod entry. Loads after Regions and Societies core (whose constructor initialises the adapter
    /// registry); registering here merges the typed adapter into the priority-ordered set. Core
    /// carries no VFE knowledge of its own — this registration is the whole integration.
    /// </summary>
    public class VFECPMod : Mod
    {
        public VFECPMod(ModContentPack content) : base(content)
        {
            WorldObjectAdapterRegistry.Register(new VfeAdapter());
            Log.Message("[RegionsAndSocieties.VFECP] Registered the Vanilla Factions Expanded adapter (priority 120).");
        }
    }
}
