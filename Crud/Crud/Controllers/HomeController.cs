using Crud.DB_Connect;
using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            MytableEntities dbobj = new MytableEntities();

            List<EmpModel> obj = new List<EmpModel>();
            var res = dbobj.MyInfoes.ToList();

            foreach (var item in res)
            {
                obj.Add(new EmpModel
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    Mobile=item.Mobile,
                    City=item.City,
                });
            }



            return View(obj);
        }

        [HttpGet]

        public ActionResult Add()
        {
            return View();
        }
       


        [HttpPost]

        public ActionResult Add(EmpModel emobj)
        {
            MytableEntities dbobj = new MytableEntities();
            MyInfo tbl = new MyInfo();
            tbl.Id = emobj.Id;
            tbl.Name = emobj.Name;
            tbl.Email = emobj.Email;
            tbl.Mobile = emobj.Mobile;
            tbl.City = emobj.City;

            if (emobj.Id == 0)
            {
                dbobj.MyInfoes.Add(tbl);
                dbobj.SaveChanges();
            }
            else
            {
                dbobj.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                dbobj.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MytableEntities dbobj = new MytableEntities();
            var del = dbobj.MyInfoes.Where(a => a.Id == id).First();
            dbobj.MyInfoes.Remove(del);
            dbobj.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            MytableEntities dbobj = new MytableEntities();
            EmpModel emobj = new EmpModel();
            var edit = dbobj.MyInfoes.Where(a => a.Id == id).First();
            emobj.Id = edit.Id;
            emobj.Name = edit.Name;
            emobj.Email = edit.Email;
            emobj.Mobile = edit.Mobile;
            emobj.City = edit.City;

            ViewBag.id = edit.Id;
            return View("Add", emobj);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}