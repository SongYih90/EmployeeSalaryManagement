using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalaryManagement
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

            var queryString = "SELECT * FROM Employees";
            var dbConnection = new SqlConnection(dbConnectionString);
            var dataAdapter = new SqlDataAdapter(queryString, dbConnection);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            GridEmployees.DataSource = ds.Tables[0];
            GridEmployees.DataBind();
        }

        protected string UploadButton_Click(Object sender, EventArgs e)
        {
            List<Employee> employeeDetails = new List<Employee>();
            int rowCount = 1;
            string errorMsg = string.Empty;

            if (UploadEmployees.HasFile)
            {
                try
                {
                    var sr = new StreamReader(UploadEmployees.FileName);

                    while (!sr.EndOfStream)
                    {
                        if(rowCount != 1)
                        {
                            var line = sr.ReadLine();
                            if(line[0] != '#') 
                            {
                                string[] values = line.Split(',');
                                if(values.Count() != 4)
                                {
                                    errorMsg += "Row " + rowCount + " does not have enough value." + Environment.NewLine;
                                }
                                else
                                {
                                    Employee emp = new Employee();
                                    emp.EmployeeID = values[0];
                                    emp.LoginName = values[1];
                                    emp.Name = values[2];
                                    if (Decimal.TryParse(values[3], out decimal result) && result >= 0)
                                        emp.Salary = Decimal.Parse(values[3]);
                                    else
                                        errorMsg += "Row " + rowCount + " does not have the correct value for salary." + Environment.NewLine;

                                    if (employeeDetails.FindAll(x => x.EmployeeID == emp.EmployeeID).Count() > 0)
                                        errorMsg += "Row " + rowCount + " Employee ID already exist." + Environment.NewLine;
                                    
                                    if (employeeDetails.FindAll(x => x.LoginName == emp.LoginName).Count() > 0)
                                        errorMsg += "Row " + rowCount + " Login Name already exist." + Environment.NewLine;

                                    if (emp.EmployeeID == string.Empty || emp.LoginName == string.Empty || emp.Name == string.Empty)
                                        errorMsg += "Row " + rowCount + " has empty data." + Environment.NewLine;

                                    employeeDetails.Add(emp);
                                }
                            }
                        }
                        rowCount++;
                    }
                }
                catch(Exception ex)
                {
                    errorMsg += ex.Message;
                }
            }

            return errorMsg;
        }
    }

    public class Employee
    {
        public string EmployeeID;
        public string LoginName;
        public string Name;
        public decimal Salary;
    }
}