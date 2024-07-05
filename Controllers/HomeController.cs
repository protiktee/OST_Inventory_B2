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
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = Equipment.dtEquipment();
            
            ViewBag.dataTable= MVCViewToolKit.DynamicTableMaker.MakeTableDynamic(dataTable);
            return View();
        }
         
    }
}