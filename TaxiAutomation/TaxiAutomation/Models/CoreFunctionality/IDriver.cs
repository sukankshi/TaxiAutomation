using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAutomation.Models.CoreFunctionality
{
    public interface IDriver
    {
        IDriver CreateDriver(string licenceNo, string firstName, string lastName, int? age, string emailId, string contactNo, string password);
        bool DeleteDriver(int driverId);
        IDriver EditDriver(int driverId, string licenceNo, string firstName, string lastName);

        IDriver GetDriverById(int driverId);
        IDriver GetDriverByLicenceNo(string licenceNo);
    }
}
