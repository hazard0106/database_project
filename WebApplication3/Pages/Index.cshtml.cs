using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
namespace WebApplication2.Pages
{

    public class CustomerInfo
    {
        public String id;
        public String name;
        public String phonenumber;

    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<CustomerInfo> listCustomer = new List<CustomerInfo>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM customerInfo";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerInfo customerInfo = new CustomerInfo();

                            customerInfo.id = "" + reader.GetInt32(0);
                            customerInfo.name = reader.GetString(1);
                            customerInfo.phonenumber = "" + reader.GetInt32(2);
                            listCustomer.Add(customerInfo);



                        }
                    }
                }



            }
        }
        public void CreateCustomer()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO customerInfo " +
                "(name, phone_number) VALUES " +
                    "(@name, @phone_number);";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", Request.Form["name"].ToString());
                    command.Parameters.AddWithValue("@phone_number", Request.Form["phonenumber"].ToString());
                    _logger.LogInformation("CreateCustomer method invoked." + Request.Form["name"].ToString());
                    command.ExecuteNonQuery();


                }






            }
            OnGet();
        }
    }

}