using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileJson.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentType { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
