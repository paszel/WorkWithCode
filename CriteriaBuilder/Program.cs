using System;

namespace CriteriaBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var criteriaSql = EmployeeCriteria.Create()
                .SuperiorId(1)
                .Age(15)
                .DepartmentId(12)
                .RegionId(123)
                .Get();

            Console.WriteLine(criteriaSql);
            Console.ReadKey();
        }
    }

    public class EmployeeCriteria
    {
        private string _sql;

        private EmployeeCriteria()
        {
            _sql = "SELECT * FROM EMPLOYEES WHERE 1=1";
        }

        public static EmployeeCriteria Create()
        {
            return new EmployeeCriteria();
        }

        public EmployeeCriteria SuperiorId(int superiorId)
        {
            _sql = string.Concat(_sql, $" AND SuperiorId = {superiorId}");
            return this;
        }

        public EmployeeCriteria Age(int age)
        {
            _sql = string.Concat(_sql, $" AND Age = {age}");
            return this;
        }

        public EmployeeCriteria DepartmentId(int departmentId)
        {
            _sql = string.Concat(_sql, $" AND DepartmentId = {departmentId}");
            return this;
        }

        public EmployeeCriteria RegionId(int regionId)
        {
            _sql = string.Concat(_sql, $" AND RegionId = {regionId}");
            return this;
        }

        public string Get()
        {
            return _sql;
        }
    }
}
