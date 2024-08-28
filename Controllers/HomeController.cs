using OST_Inventory_B_2.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        }*/
        /*[HttpGet]
        public ActionResult TestAPI(int id)
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

            return Json(id.ToString(), JsonRequestBehavior.AllowGet);
        }*/
        /*[HttpPost]
        public ActionResult TestAPIPostForModel(Personal objPersonal)
        {
            return Json(objPersonal, JsonRequestBehavior.AllowGet);
        }*/
        /*[HttpPost]
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
        [HttpPost]
        public ActionResult SaveAssignment(FormCollection formCollection, string btnSubmit)
        {
            
            Session["sessionMsg"] = "";
            if (btnSubmit == "save")
            {
                int customerid= Convert.ToInt32(formCollection["ddlCustomerName"].ToString());
                int equipmentid = Convert.ToInt32(formCollection["ddlEquipmentName"].ToString());
                int assignCount = Convert.ToInt32(formCollection["txtItemCount"].ToString());

                DataTable dataTable = Equipment.dtEquipment();
                var equipmentStock = (from p in dataTable.AsEnumerable()
                                      where p.Field<Int32>("EquipmentId") == equipmentid
                                      select p.Field<Int32>("Stock")
                                    ).SingleOrDefault();


                if (assignCount <= Convert.ToInt32(equipmentStock) && assignCount > 0)
                {
                    string sdsd = formCollection["chkIsreleased"].ToString();
                    int IsReleased = 0;
                    if (formCollection["chkIsreleased"].ToString().ToLower() == "on")
                        IsReleased = 1;
                    int result = Equipment.AssignEquipment(customerid, equipmentid, assignCount, IsReleased);
                    if (result == 1)
                    {

                        Session["sessionMsg"] =( IsReleased == 1 ? "Item Released Successfully" : "Save Successfully");
                         
                    }
                }
                else
                {
                    if (assignCount == 0)
                        Session["sessionMsg"] = "Assignment must be > 0 ";
                    if (assignCount > Convert.ToInt32(equipmentStock))
                        Session["sessionMsg"] = "Assignment must be <= Stock";
                }
                
            }
            return RedirectToAction("DashBoard");
        }
    }
}