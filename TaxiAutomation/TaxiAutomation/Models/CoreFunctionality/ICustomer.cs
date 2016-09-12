using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiAutomation.Models.CoreFunctionality
{
    interface ICustomer
    {
       Customer CreateCustomer(long mobileNo, string emailId, string firstName, string lastName);
         Customer EditCustomerById(int customerId, long mobileNo, string emailId, string firstName, string lastName);
         bool DeleteCustomerById(int customerId);

         Customer GetCustomerById(int customerId);

    }
}
