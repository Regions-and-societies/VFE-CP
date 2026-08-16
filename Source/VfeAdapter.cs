using RimWorld.Planet;
using RegionsAndSocieties.Integration;
using Verse;

namespace RegionsAndSocieties.VFECP
{
    /// <summary>
    /// Typed adapter for the Vanilla Factions Expanded family, bound against the 1.6 framework
    /// assembly (<c>VEF.dll</c> — the rename from the old VFECore assembly is exactly the drift
    /// that used to break reflection profiles silently; here it is a compile-time fact).
    ///
    /// <para>One typed check replaces core's two string profiles. <c>VFEMedieval.MerchantGuild</c>
    /// derives from <c>VEF.Planet.MovingBase</c>, and the only reason Medieval 2 had a separate
    /// profile was that a string ExactType rule cannot match subclasses. The <c>is</c> check covers
    /// VFE Core, Medieval 2, and any future module's moving base in one rule — all of them are
    /// things that move, i.e. caravans, not territorial holdings.</para>
    ///
    /// <para>The rest of the VFE faction suite needs NO adapter — verified by reflection over the
    /// 1.6 assemblies (R&T #71): vfe.empire ships Honor types, classical nothing, deserters only
    /// SitePartWorkers, tribals only a ThingComp, insectoid2 only a SitePartWorker. Their faction
    /// bases are plain vanilla Settlement objects with modded defs, which the vanilla adapter
    /// already classifies. Recording that here rather than shipping speculative profiles is the
    /// standing #31/#33 discipline.</para>
    ///
    /// <para>Deliberately no namespace fallback: <c>VEF.Planet</c> contains nothing else this mod
    /// should claim, and an unmatched future VEF world object announces itself through core's
    /// unknown-object logging rather than being silently miscategorised.</para>
    /// </summary>
    public class VfeAdapter : WorldObjectAdapterBase
    {
        public override string AdapterId { get { return "vfe"; } }

        public override string DisplayName { get { return "Vanilla Factions Expanded"; } }

        /// <summary>Same slot as core's old reflection profile (120; the 121 Medieval slot is subsumed).</summary>
        public override int Priority { get { return 120; } }

        private static readonly bool Active = ModsConfig.IsActive("OskarPotocki.VanillaFactionsExpanded.Core");

        public override bool IsActive { get { return Active; } }

        public override bool TryClassify(WorldObject obj, out WorldObjectKind kind)
        {
            kind = WorldObjectKind.Unknown;
            if (obj is VEF.Planet.MovingBase)
            {
                kind = WorldObjectKind.Caravan;
                return true;
            }
            return false;
        }
    }
}
