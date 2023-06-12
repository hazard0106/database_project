using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication1.Pages
{
    public class ArticleInfo
    {
        public String id;
        public String name;
        public String price;
        public String datetime;
        
    }

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public List<ArticleInfo> listArticle = new List<ArticleInfo>();
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM ArticleInfo";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArticleInfo articleInfo = new ArticleInfo();

                            articleInfo.id = "" + reader.GetInt32(0);
                            articleInfo.name = reader.GetString(1);
                            articleInfo.price = "" + reader.GetDouble(2);
                            articleInfo.datetime = "" + reader.GetDateTime(3);
                           


                            listArticle.Add(articleInfo);



                        }


                    }



                }
            }
        }

        public void CreateArticle()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6\\SQLEXPRESS;Initial Catalog=databaseformahir;User ID=sa;Password=mahir2004";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO ArticleInfo " +
               "(name, price,datetime) VALUES " +
                   "(@name, @price,@datetime);";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", Request.Form["name"].ToString());
                    command.Parameters.AddWithValue("@price", Request.Form["price"].ToString());
                    command.Parameters.AddWithValue("@datetime", Request.Form["datetime"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["id"].ToString());
                    
                    command.ExecuteNonQuery();



                }

            }
            OnGet();
        }


    }
}