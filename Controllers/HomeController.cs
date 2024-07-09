using OST_Inventory_B_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OST_Inventory_B_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult DashBoard()
        {
            //Session["sessionMsg"] = "";
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = Equipment.dtEquipment();
            
            ViewBag.dataTable= MVCViewToolKit.DynamicTableMaker.MakeTableDynamic(dataTable);
            return View();
        }

        [HttpPost]
        public ActionResult SaveEquipment(FormCollection formCollection,string btnSubmit) 
        {
            Session["sessionMsg"] = "";
            if (btnSubmit == "save")
            {
                string Name = formCollection["txtEquipmentName"].ToString();
                int Count = Convert.ToInt32(formCollection["txtEquipmentCount"].ToString());

                int result=Equipment.SaveEquipment(Name, Count);
                if (result == 1)
                {
                    Session["sessionMsg"] = "Save Successfully";
                }
            }
            return RedirectToAction("DashBoard"); 
        }
    }
}