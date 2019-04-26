namespace CompanyName.ServiceName.Contracts.DataAccess
{
    public abstract class Transformer : MeasurementDevice
    {
        /// <summary> Gets or sets transformer's transformation ratio. </summary>
        /// <value> Коэффициент трансформации </value>
        public double TransformationRatio { get; set; }
    }
}
