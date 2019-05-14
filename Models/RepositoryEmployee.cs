using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDDatabaseTutorial.Models
{
    public class RepositoryEmployee
    {
        public static void EditExecutor(Employee employee, int DivId)
        {
            employee.DivisionId = DivId;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", employee.Id);
            param.Add("@Name", employee.Name);
            param.Add("@Address", employee.Address);
            param.Add("@PhoneNumber", employee.PhoneNumber);
            param.Add("@DivId", employee.DivisionId);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);
        }

        public static void Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeleteByID", param);
        }

        public static byte[] Download()
        {
            var employees = new List<Employee>(DapperORM.ReturnList<Employee>("EmployeeViewAll"));
            var fileContentText = new StringBuilder();
            fileContentText.Append("EMPLOYEE NAME");
            fileContentText.Append("\t\t\t");
            fileContentText.Append("ADDRESS");
            fileContentText.Append("\t\t\t");
            fileContentText.Append("PHONE NUMBER");
            fileContentText.Append("\t\t");
            fileContentText.Append("DIVISION NAME");
            fileContentText.Append("\r\n");
            foreach (var employee in employees)
            {
                fileContentText.Append(employee.Name);
                fileContentText.Append("\t\t");
                fileContentText.Append(employee.Address);
                fileContentText.Append("\t\t");
                fileContentText.Append(employee.PhoneNumber);
                fileContentText.Append("\t\t");
                fileContentText.Append(employee.DivisionName);
                fileContentText.Append("\r\n");
            }
            var fileContent = Encoding.ASCII.GetBytes(fileContentText.ToString());
            return (fileContent);
        }

        public static IEnumerable<Employee> List()
        {
            var List = DapperORM.ReturnList<Employee>("EmployeeViewAll");
            return List;
        }

        public static DynamicParameters EditList(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            return param;
        }
    }
}
