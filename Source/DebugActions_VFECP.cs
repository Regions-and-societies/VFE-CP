using System.Text;
using LudeonTK;
using RimWorld.Planet;
using RegionsAndSocieties.Integration;
using Verse;

namespace RegionsAndSocieties.VFECP
{
    /// <summary>
    /// Debug validation for the patch (per the workspace debug-command gate): dump every VFE
    /// moving base with its resolved kind, so parity against the old reflection path is verifiable
    /// headlessly via run_debug_action.
    /// </summary>
    public static class DebugActions_VFECP
    {
        [DebugAction("Regions and Societies", "R&S VFE-CP: world-object dump", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap | AllowedGameStates.PlayingOnWorld)]
        private static void VfeObjectDump()
        {
            var sb = new StringBuilder();
            sb.AppendLine("--- VFE-CP world-object dump ---");
            int count = 0;
            var objects = Find.WorldObjects != null ? Find.WorldObjects.AllWorldObjects : null;
            if (objects != null)
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    WorldObject obj = objects[i];
                    if (!(obj is VEF.Planet.MovingBase)) continue;

                    count++;
                    WorldObjectKind kind;
                    WorldObjectAdapterRegistry.TryClassify(obj, out kind);
                    sb.AppendLine($"  {obj.GetType().FullName,-40} tile={obj.Tile,-7} kind={kind,-10} faction={obj.Faction?.Name ?? "none"}");
                }
            }
            sb.AppendLine($"{count} VFE moving base(s). Other VFE modules ship no world objects (their bases are vanilla settlements).");
            Log.Message(sb.ToString());
        }
    }
}
