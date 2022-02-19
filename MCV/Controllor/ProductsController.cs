using DOTNETEXAMproduct.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOTNETEXAMproduct.Controllers
{
    public class ProductsController : Controller
    {
        SqlConnection conn;
        public void CommonConn()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EXAM;Integrated Security=True";
            conn.Open();
                
        }
        // GET: Products
        public ActionResult Index()
        {
            CommonConn();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "ShowProducts";

            List<Product> list = new List<Product>();
            try
            {
                SqlDataReader readdata = comm.ExecuteReader();
                while(readdata.Read())
                {
                    list.Add(new Product { ProductId = (int)readdata["ProductId"], ProductName = readdata["ProductName"].ToString(), Rate = (decimal)readdata["Rate"], Description = readdata["Description"].ToString(), CategoryName = readdata["CategoryName"].ToString() });
                }
                readdata.Close();
                
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            conn.Close();
            return View(list);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            CommonConn();
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            //comm.CommandType = System.Data.CommandType.Text;
            //comm.CommandText = "select * from Product where ProductId=@ProductId";
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "FetchById";

            comm.Parameters.AddWithValue("@ProductId", id);

            Product pro = new Product();
            try
            {
                SqlDataReader readdata = comm.ExecuteReader();
                if(readdata.Read())
                {
                    pro = new Product { ProductId = (int)readdata["ProductId"], ProductName = readdata["ProductName"].ToString(), Rate = (decimal)readdata["Rate"], Description = readdata["Description"].ToString(), CategoryName = readdata["CategoryName"].ToString() };
                }
            }

            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            conn.Close();
            return View(pro);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            CommonConn();
            try
            {
                SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "EditProduct";

                comm.Parameters.AddWithValue("@ProductId", id);
                comm.Parameters.AddWithValue("@ProductName", pro.ProductName);
                comm.Parameters.AddWithValue("@Rate", pro.Rate);
                comm.Parameters.AddWithValue("@Description", pro.Description);
                comm.Parameters.AddWithValue("@CategoryName", pro.CategoryName);

                //comm.Parameters.AddWithValue("@ProductName", pro.ProductName,
                //"@Rate", pro.Rate,
                //"@Description", pro.Description,
                //"@CategoryName", pro.CategoryName;

                // TODO: Add update logic here
                comm.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
