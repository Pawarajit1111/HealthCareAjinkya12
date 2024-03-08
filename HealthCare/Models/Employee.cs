namespace HealthCare.Models
{
    public class Employee
    {
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public double BasicSalary { get; set; }
      
        public double DearnessAllowance => BasicSalary * 0.4;
        public double ConveyanceAllowance => Math.Min(DearnessAllowance * 0.1, 250);
        public double HouseRentAllowance => Math.Max(BasicSalary * 0.25, 1500);
        public double GrossSalary => BasicSalary + DearnessAllowance + ConveyanceAllowance + HouseRentAllowance;
        public double PT => (GrossSalary <= 3000) ? 100 : ((GrossSalary <= 6000) ? 150 : 200);
        public double TotalSalary => BasicSalary + DearnessAllowance + ConveyanceAllowance + HouseRentAllowance - PT;
    }
}
