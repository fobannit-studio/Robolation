using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Simulation.UI
{
    public class Summary : MonoBehaviour
    {
        [SerializeField]
        private Text text;
        private Simulation simulation;

        public void Finished(Simulation simulation)
        {
            FindObjectOfType<SimulationHUD>().Deactivate();

            this.simulation = simulation;
            this.gameObject.SetActive(true);

            StringBuilder builder= new StringBuilder();
            builder.AppendLine("Time:"+ (simulation.PassedTime/24/10).ToString("0.0")+" days");
            builder.AppendLine("Robots used in building");
            List<string> used_robots = new List<string>();
            foreach (var robot in Simulation.Robots)
            {
                used_robots.Add(robot.GetType().Name.ToString());
            }
            foreach (var name in used_robots.Distinct())
            {
                builder.AppendLine($"{name}: {used_robots.Where(x => x == name).Count()}x");
            }
            builder.AppendLine("Buildings built");
            List<string> buildings = new List<string>();
            foreach (var item in Simulation.Buildings)
            {
                buildings.Add(item.Name);
            }
            foreach (var name in buildings.Distinct())
            {
                builder.AppendLine($"{name}: {buildings.Where(x => x == name).Count()}x");
            }
            text.text = builder.ToString();

        }
        public void SaveSummary()
        {

        }
        public void Repeat()
        {
            foreach (var item in Simulation.Robots)
            {
              
                Destroy(item.gameObject);
            }
            Simulation.Robots.Clear();
            this.gameObject.SetActive(false);
            FindObjectOfType<SetupSimulation>().BeginRobotSelection();
        }
        public void Next()
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
        
    } 
}
