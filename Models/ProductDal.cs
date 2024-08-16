using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DummyMVCwithoutEF.Models
{
    public class ProductDal
    {
        string cs = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        SqlConnection conn;

        public ProductDal() 
        {
            conn = new SqlConnection(cs);
            conn.Open();    
        }  //Adding Connection 
        public List<Product> GetAllProducts()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("exec sp_show", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<Product> product = new List<Product>();   
            foreach (DataRow dr in dt.Rows) 
            {
                product.Add
                    (
                    new Product
                    {
                        Id = int.Parse(dr["id"].ToString()),
                        pname = dr["pname"].ToString(),
                        pcat = dr["pcat"].ToString(),
                        price = double.Parse(dr["price"].ToString())
                    }
                    );
            }
            return product;

        }  //fetching data in list format


        public void AddProduct(Product p)
        {
            string q = $"exec sp_insert'{p.pname}','{p.pcat}',{p.price}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        } // Inserting data 
        public void DeleteProd(int id)
        {
            
            string q = $"exec sp_delete {id}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        } // Deleting Data



        public void EditProd(int id,string name,string cat,double price)
        {
            string q = $"exec sp_edit {id},'{name}','{cat}','{price}'";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        } // Updating Data
    }
}