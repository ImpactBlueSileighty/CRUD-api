using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data; 
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UniCardApi.Models;

namespace UniCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RequestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Request";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniCardRequestCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Request req)
        {
            string query = @"insert into dbo.Request
                            values (@Name, @Phone, @Email)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniCardRequestCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", req.Name);
                    myCommand.Parameters.AddWithValue("@Phone", req.Phone);
                    myCommand.Parameters.AddWithValue("@Email", req.Email);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Request
                            where Id = @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniCardRequestCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Request req)
        {
            string query = @"update dbo.Request
                            set Name = @Name, Phone = @Phone, Email = @Email
                            where Id = @Id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UniCardRequestCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", req.Name);
                    myCommand.Parameters.AddWithValue("@Phone", req.Phone);
                    myCommand.Parameters.AddWithValue("@Email", req.Email);
                    myCommand.Parameters.AddWithValue("@Id", req.Id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update Successfully");
        }
    }
}
