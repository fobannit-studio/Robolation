namespace Simulation.World
{
    public class Environment
    {
        public Environment(float humidity, float maxHumidity, float temperature)
        {
            _actualHumidity = humidity;
            _maxHumidity = maxHumidity;
            Temperature = temperature;
        }
        public float Humidity {
            get => _actualHumidity / _maxHumidity;
            set => _actualHumidity = value;
        }

        public float Temperature;
        private float _maxHumidity;
        float _actualHumidity;
    }
}