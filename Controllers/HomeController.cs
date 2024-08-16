using OST_Inventory_B_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OST_Inventory_B_2.Controllers
{
    public class HomeController : Controller
    {
        /*public class Personal 
        {
            public string Name { get; set; }
            public string AGE { get; set; }
            public string Height { get; set; }
        }
        [HttpGet]
        public ActionResult TestAPI()
        {
            //return Json("API result", JsonRequestBehavior.AllowGet);

            //Personal personal = new Personal();
            //personal.Name = "ABC";
            //personal.AGE = "10";
            //personal.Height = "5.7";
            //return Json(personal, JsonRequestBehavior.AllowGet);

            List<Personal> lstPersonal = new List<Personal>();
            Personal personal = new Personal();
            personal.Name = "ABC";
            personal.AGE = "10";
            personal.Height = "5.7";
            lstPersonal.Add(personal);

            personal = new Personal();
            personal.Name = "xyz";
            personal.AGE = "10";
            personal.Height = "5.7";
            lstPersonal.Add(personal);

            personal = new Personal();
            personal.Name = "pqr";
            personal.AGE = "10";
            personal.Height = "5.7";
            lstPersonal.Add(personal);

            return Json(lstPersonal, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TestAPIPost(List<Personal> ListPersonal)
        {  
            return Json(ListPersonal, JsonRequestBehavior.AllowGet);
        }*/
        public ActionResult DashBoard()
        {
            //Session["sessionMsg"] = "";
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = Equipment.dtEquipment();
            
            ViewBag.dataTable= MVCViewToolKit.DynamicTableMaker.MakeTableDynamic(dataTable);
            return View();
        }
        public ActionResult Test()
        { 
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