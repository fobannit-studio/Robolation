using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Simulation.World;
namespace Simulation.Software
{
    class BuildingPreparation : Application
    {
        /// <summary>
        /// Builders, this operator control
        /// </summary>
        public IReadOnlyList<int> BuildersMacAddresses { get => buildersMacAddreses.AsReadOnly(); }
        public IReadOnlyList<int> TransportersMacAddresses { get => transportersMacAddresses.AsReadOnly(); }
        /// <summary>
        /// Contains buildings, for which this operator should assign builders
        /// </summary>
        public Stack<Building> BuildingsWithoutBuilders { get; private set; }
        /// <summary>
        /// Dictionary with buildings of builder tracking threads, to redirect received frames to appropriate threa.
        /// Key - mac of tracked builder
        /// Value - tracking application
        /// </summary>
        public Dictionary<int, BuilderTracking> BuilderTrackingApplications = new Dictionary<int, BuilderTracking>();
        /// <summary>
        /// Warehouses where this operator can send his transporters, so they will still be in his range
        /// </summary>
        public List<Warehouse> WarehousesInRange = new List<Warehouse>();
        /// <summary>
        /// Maximum number buildings, this operator can look after
        /// </summary>
        public int MaxAdministratedBuildingNumber { get; set; } = 5;
        /// <summary>
        /// indicates, what was number of subscribers during last update of registered builders;
        /// </summary>
        private int lastUpdateCapacity;
        private List<int> buildersMacAddreses = new List<int>();
        private List<int> transportersMacAddresses = new List<int>();
        private CommunicationBasedApplicationState assigningBuilders;
        private bool IsInMyRange(MonoBehaviour physicalObejct)
            => Radio.Range > Vector3.Distance(physicalObejct.transform.position, AttributedSoftware.Position);

        /// <summary>
        /// Assign buildings, that this operator should look after
        /// </summary>
        /// <param name="buildings">List of buildings</param>
        private void FindBuildings(ref List<Building> buildings)
        {
            var stack = BuildingsWithoutBuilders ?? new Stack<Building>();
            var canTake = MaxAdministratedBuildingNumber - stack.Count;
            List<Building> buildingsToAdministrate = buildings
                                                    .Where(x => IsInMyRange(x) && !x.isFinished)
                                                    .Take(canTake)
                                                    .ToList();
            foreach (var building in buildingsToAdministrate)
                stack.Push(building);
            buildings = buildings.Except(buildingsToAdministrate).ToList();
        }
        /// <summary>
        /// Select warehouses in operators range, so operator will know where he can send transporter, 
        /// so he will still be operated by him
        /// </summary>
        private void FindWarehouses() 
        {
            WarehousesInRange = Simulation.Warehouses
                                .Where(IsInMyRange)
                                .OrderBy(x => Vector3.Distance(x.transform.position, AttributedSoftware.Position))
                                .ToList();
        }
        private void IdentifySubsribers()
        {
            if (AttributedSoftware.RoutingTable.Count == lastUpdateCapacity) return;
            lastUpdateCapacity = AttributedSoftware.RoutingTable.Count;
            buildersMacAddreses.Clear();
            transportersMacAddresses.Clear();
            foreach(var record in AttributedSoftware.RoutingTable.Keys)
            {
                if (record.type == typeof(BuilderSoftware)) buildersMacAddreses.Add(record.mac);
                else if (record.type == typeof(TransporterSoftware)) transportersMacAddresses.Add(record.mac);
            }
        }
        public override void initStates()
        {
            assigningBuilders = new AssigningBuilders(this);
            currentState = assigningBuilders;
            UseScheduler = true;
            BuildingsWithoutBuilders = new Stack<Building>();
            FindBuildings(ref Simulation.NotAdministratedBuildings);
            FindWarehouses();
        }
        protected override void DoAction()
        {
            if (BuildingsWithoutBuilders.Count > 0)
                currentState.Send();
            else
                FindBuildings(ref Simulation.NotAdministratedBuildings);
            IdentifySubsribers();
        }
    }
}
