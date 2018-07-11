namespace iot_webapp.Services {
    public interface ITemperatureService {
        double Temperature { get; }
        void SetTemperature(double temperature);
    }
}