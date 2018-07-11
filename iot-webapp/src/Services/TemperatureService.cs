namespace iot_webapp.Services {
    public class TemperatureService : ITemperatureService {
        public double Temperature { get; private set; }

        public TemperatureService() {
            Temperature = 25.0;
        }
        
        public void SetTemperature(double temperature) {
            if (temperature < 10 || temperature > 40) {
                //Do not accept temperature values outside
                //reasonable values for the season 10ºC to 40ºC
                return;
            }
            Temperature = temperature;
        }
        
    }
}