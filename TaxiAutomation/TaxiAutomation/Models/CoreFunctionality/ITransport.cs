using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAutomation.Models.CoreFunctionality
{
    public interface ITransport
    {
        ITransport CreateTransport(string registrationNo, string vehicleNo, int driverId, TransportEnum transportType, Location location, bool available, bool booked);
        bool DeleteTransport(int transportId);
        ITransport EditTransport(int transportId, int driverId);

        ITransport GetTransportById(int transportId);
        ITransport GetTransportByVehicleNo(string vehicleNo);
        ITransport AssignTaxiToDriver(int taxiId, int driverId);
    }
}
