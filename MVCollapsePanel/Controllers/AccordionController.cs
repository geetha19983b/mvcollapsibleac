using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;
using System.Data.SqlClient;
using MVCollapsePanel.Models;

namespace MVCollapsePanel.Controllers
{
    public class AccordionController : Controller
    {
        // GET: Accordion
        public ActionResult BSIndex()
        {
            List<AccordionModel> items = new List<AccordionModel>();
            items = GetItems();

            return View(items);
        }
        public ActionResult Index()
        {
            List<AccordionModel> items = new List<AccordionModel>();
            items = GetItems();

            return View(items);
        }
        [HttpPost]
        public ActionResult DisplayEmployees(string id)
        {

            return Content("From controller " + id);
        }
        public List<AccordionModel> GetItems()
        {
            List<AccordionModel> items = new List<AccordionModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Id,Title, Content from AccordianContent";
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new AccordionModel
                            {
                                Id = sdr["Id"].ToString(),
                                Title = sdr["Title"].ToString(),
                                Content = sdr["Content"].ToString()
                            });
                        }
                    }

                    con.Close();
                }
            }
            return items;
        }
    }
}