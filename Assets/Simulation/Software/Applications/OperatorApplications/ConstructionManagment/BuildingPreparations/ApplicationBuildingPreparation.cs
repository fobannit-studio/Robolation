using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Simulation.World;
namespace Simulation.Software
{
    class BuildingPreparation : Application
    {
        /// <summary>
        /// indicates, what was number of subscribers during last update of registered builders;
        /// </summary>
        private int lastUpdateCapacity;
        private List<int> buildersMacAddreses = new List<int>();
        public IReadOnlyList<int> BuildersMacAddresses { get => buildersMacAddreses.AsReadOnly(); }
        /// <summary>
        /// Contains of builder and application, that tracking that builder
        /// </summary>
        /// <summary>
        /// Contains buildings, that this operator should look after
        /// </summary>
        public Stack<Building> BuildingsWithoutBuilders { get; private set; }
        public Dictionary <int, BuilderTracking> BuilderTrackingApplications = new Dictionary<int, BuilderTracking>(); 
        private CommunicationBasedApplicationState assigningBuilders;
        private bool IsInMyRange(MonoBehaviour physicalObejct)
            => Radio.Range > Vector3.Distance(physicalObejct.transform.position, AttributedSoftware.Position);
        /// <summary>
        /// Maximum number buildings, this operator can look after
        /// </summary>
        public int MaxAdministratedBuildingNumber { get; set; } = 5;
        /// <summary>
        /// Assign buildings, that this operator should look after
        /// </summary>
        /// <param name="buildings">List of buildings</param>
        private void FindBuildings(ref List<Building> buildings)
        {
            var stack = BuildingsWithoutBuilders ?? new Stack<Building>();
            var canTake = MaxAdministratedBuildingNumber - stack.Count;
            List<Building> buildingsToAdministrate = buildings.Where(IsInMyRange).Take(canTake).ToList();
            foreach (var building in buildingsToAdministrate)
                stack.Push(building);
            buildings = buildings.Except(buildingsToAdministrate).ToList();
        }
        private void IdentifyBuilders()
        {
            if (AttributedSoftware.RoutingTable.Count == lastUpdateCapacity) return;
            lastUpdateCapacity = AttributedSoftware.RoutingTable.Count;
            buildersMacAddreses = AttributedSoftware.RoutingTable.Keys
                                  .Where(x => x.type == typeof(BuilderSoftware))
                                  .Select(x => x.mac).ToList();
        }
        public override void initStates()
        {
            assigningBuilders = new AssigningBuilders(this);
            currentState = assigningBuilders;
            UseScheduler = true;
            BuildingsWithoutBuilders = new Stack<Building>();
            FindBuildings(ref Simulation.NotAdministratedBuildings);
        }
        protected override void DoAction()
        {
            if (BuildingsWithoutBuilders.Count > 0)
                currentState.Send();
            IdentifyBuilders();
        }
    }
}
