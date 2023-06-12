using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication3.Pages
{
    public class InvoiceInfo
    {
        public string id;
        public string customerid;
        public string articleid;
        public string datetime;
        public string price;
        public string paymentmethod;
    }
    public class InvoiceModel : PageModel
    {
        private readonly ILogger<InvoiceModel> _logger;
        public List<InvoiceInfo> listInvoice = new List<InvoiceInfo>();

        public InvoiceModel(ILogger<InvoiceModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            string connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM InvoiceInfo";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InvoiceInfo invoiceInfo = new InvoiceInfo();
                            invoiceInfo.id = "" + reader.GetInt32(0);
                            invoiceInfo.customerid = "" + reader.GetInt32(1);
                            invoiceInfo.articleid = "" + reader.GetInt32(2);
                            invoiceInfo.datetime = "" + reader.GetDateTime(3);
                            invoiceInfo.price = "" + reader.GetDouble(4);
                            invoiceInfo.paymentmethod = "" + reader.GetString(5);
                            listInvoice.Add(invoiceInfo);

                        }
                    }
                }




            }



        }

        public void CreateInvoice()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {

            string connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO InvoiceInfo " +
                    "(customerid, articleid,datetime,price,paymentmethod) VALUES " +
                    "(@customerid, @articleid, @datetime, @price,@paymentmethod );";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@customerid", Request.Form["customerid"].ToString());
                    command.Parameters.AddWithValue("@articleid", Request.Form["articleid"].ToString());
                    command.Parameters.AddWithValue("@datetime", Request.Form["datetime"].ToString());
                    command.Parameters.AddWithValue("@price", Request.Form["price"].ToString());
                    command.Parameters.AddWithValue("@paymentmethod", Request.Form["paymentmethod"].ToString());
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["idcustomer"]);


                    command.ExecuteNonQuery();
                }

            }
            OnGet();
        }
    }
}