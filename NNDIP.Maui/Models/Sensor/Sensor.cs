namespace NNDIP.Maui.Models.Sensor
{
    public class Sensor
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SensorType { get; set; }
        public DateTimeOffset DataTimestamp { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is not Sensor)
            {
                return false;
            }
            return Id == ((Sensor)obj).Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
