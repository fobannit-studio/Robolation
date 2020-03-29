
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


        public void GenerateLandscape()
        {
            voxelPlacer.Generate();
        }
        public void BeginBuildingPlacement()
        {
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
            RobotSelectionCanvas.gameObject.SetActive(false);
            RobotPlacementCanvas.gameObject.SetActive(true);
        }
    }
}
