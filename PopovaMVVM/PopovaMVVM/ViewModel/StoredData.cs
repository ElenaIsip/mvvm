using System;
using System.Collections.Generic;
using PopovaMVVM.Model;

namespace PopovaMVVM.ViewModel
{
    public class StoredData
    {
        public List<Employee> Employees { get; set; }
        public List<Position> Positions { get; set; }
        public List<Department> Departments { get; set; }
        public List<AdditionalPayment> AdditionalPayments { get; set; }
        public List<Staffing> Staffing { get; set; }

       
    }
}