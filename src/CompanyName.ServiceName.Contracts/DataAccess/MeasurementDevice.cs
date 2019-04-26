using System;

namespace CompanyName.ServiceName.Contracts.DataAccess
{
    public abstract class MeasurementDevice
    {
        /// <summary>
        /// Gets or sets Measurement device's number that should
        /// be unique for any devices the same of the same type.
        /// </summary>
        /// <value> Номер </value>
        public long Number { get; set; }

        /// <summary> Gets or sets Measurement device's verification date. </summary>
        /// <value> Дата поверки </value>
        public DateTime VerificationDate { get; set; }
    }
}
