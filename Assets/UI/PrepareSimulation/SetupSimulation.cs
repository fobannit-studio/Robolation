
using UnityEngine;
using Simulation.World;
using UnityEngine.UI;
namespace Simulation.UI
{


    public class SetupSimulation : MonoBehaviour
    {
        [SerializeField]
        private VoxelPlacer voxelPlacer;
        [SerializeField]
        private Canvas LandscapeCanvas;

        [SerializeField]
        private Canvas PlacementCanvas;

        [SerializeField]
        private Canvas WarehousePlacerCanvas;

        [SerializeField]
        private Canvas RobotSelectionCanvas;

        [SerializeField]
        private Canvas RobotPlacementCanvas;

        [SerializeField]
        private Simulation simulation;
        public void GenerateLandscape()
        {
            voxelPlacer.Generate();
        }
        public void BeginBuildingPlacement()
        {
            voxelPlacer.RebuildNavmesh();
            PlacementCanvas.gameObject.SetActive(true);
            LandscapeCanvas.gameObject.SetActive(false);

        }
        public void BeginWarehousePlacement()
        {
            PlacementCanvas.gameObject.SetActive(false);
            WarehousePlacerCanvas.gameObject.SetActive(true);
        }
        public void BeginRobotSelection()
        {
           
            if (WarehousePlacerCanvas.GetComponent<WareHousePlacer>().NextStage())
            {
                WarehousePlacerCanvas.gameObject.SetActive(false);
                RobotSelectionCanvas.gameObject.SetActive(true);
            }
           
        }
        public void BeginRobotPlacement()
        {
            var selector = RobotSelectionCanvas.GetComponent<RobotSelector>();
            if (selector.Proceed())
            {
                RobotSelectionCanvas.gameObject.SetActive(false);

                RobotPlacementCanvas.GetComponent<RobotPlacer>().Init(selector.GetSelectedRobots());
                RobotPlacementCanvas.gameObject.SetActive(true);
            }
            
        }
        public void StartSimulation()
        {
            
           var  robots= RobotPlacementCanvas.GetComponent<RobotPlacer>().GetResult();
           var warehouses = WarehousePlacerCanvas.GetComponent<WareHousePlacer>().GetWarehouses();
           var buildings = PlacementCanvas.GetComponent<BuildingPlacer>().GetBuildings();
           simulation.Init(robots, buildings, warehouses);

        }
    }
}
