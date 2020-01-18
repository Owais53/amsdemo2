using amsdemo.Infrastructure;
using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace amsdemo.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly SqlContext context = new SqlContext();
        
        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        [CustomAuthorize("Recruitment Manager", "Admin")]
        public ActionResult Contact()
        {
            ViewBag.Title = "Your Content Page";
            return (ActionResult)this.View();
        }

        public ActionResult Index()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult CreateDepartments()
        {
            ViewBag.Title = "Create Departments";
            return View();
        }

        [CustomAuthorize("Store Manager")]
        [HttpPost]
        public ActionResult CreateDepartments(tblDepartment dep)
        {
            tblDepartment check = context.tblDepartments.Where(a => a.DepartmentName == dep.DepartmentName).FirstOrDefault<tblDepartment>();

            if (check == null)
            {
                if (ModelState.IsValid)
                {
                    context.tblDepartments.Add(dep);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Department Created";
                    return RedirectToAction("DepartmentList");
                }
            }
            else
            {
                TempData["ErrorMessage"] = ("" + dep.DepartmentName + " Department already exists");
            }

                return View(dep);
        }

        
        public ActionResult DepartmentList()
        {
            return View();
        }

        public ActionResult GetDepartment()
        {
            using (SqlContext sqlContext = new SqlContext())
            {

                var Data = (from user in context.tblDepartments
                            select new
                            {
                                user.DepartmentId,
                                user.DepartmentName

                            }).ToList();


                return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Save(int id = 0)
        {
            if (id == 0)
                return View(new tblDepartment());
            else
            {
                return View((context.tblDepartments.Where(x => x.DepartmentId == id)).FirstOrDefault<tblDepartment>());
            }

        }

        [HttpPost]
        public ActionResult Save(tblDepartment dep)
        {
            if (dep.DepartmentId == 0)
            {
                context.tblDepartments.Add(dep);
                context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                context.Entry(dep).State = EntityState.Modified;
                context.SaveChanges();
                TempData["UpdateMessage"] = "Updated Successfully";
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteDepartment(int id)
        {
            using (SqlContext sqlContext = new SqlContext())
            {
                tblDepartment entity = sqlContext.tblDepartments.Where(x => x.DepartmentId == id).FirstOrDefault<tblDepartment>();
                sqlContext.tblDepartments.Remove(entity);
                sqlContext.SaveChanges();
                return Json(new { success = true, message = "Deleted Succesfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DefineStructure()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DefineStructure(StructureDetails viewModel)
        {
            var validate = context.tblStructuredetails
                 .Where(a => a.CompanyCode == viewModel.CompanyCode)
                 .Where(b => b.CityCode == viewModel.CityCode)
                 .Where(c => c.CityName == viewModel.CityName).FirstOrDefault();

            var validate1 = context.tblStructuredetails
                .Where(a => a.CompanyCode == viewModel.CompanyCode)
                .Where(b => b.CityCode == viewModel.CityCode).FirstOrDefault();

            var validate3 = context.tblStructuredetails
                .Where(a => a.CityCode == viewModel.CityCode)
                .Where(b => b.CityName == viewModel.CityName).FirstOrDefault();

            var check = context.tblStructuredetails.Where(x => x.CityCode == viewModel.CityCode).FirstOrDefault();
            var check1 = context.tblStructuredetails.Where(x => x.CityName == viewModel.CityName).FirstOrDefault();
            var check2 = context.tblStructuredetails.Where(x => x.CompanyCode == viewModel.CompanyCode).FirstOrDefault();

            if (validate == null && validate3 != null || validate1 == null && check1 == null || !context.tblStructuredetails.Any())
            {
                if (ModelState.IsValid)
                {

                    List<object> lst = new List<object>();
                    lst.Add(viewModel.CompanyCode);
                    lst.Add(viewModel.CompanyName);
                    lst.Add(viewModel.CityCode);
                    lst.Add(viewModel.CityName);
                    object[] item = lst.ToArray();
                    int output = context.Database.ExecuteSqlCommand("insert into tblStructuredetail(CompanyCode,CompanyName,CityCode,CityName) values(@p0,@p1,@p2,@p3)", item);
                    if (output > 0)
                    {
                       

                            RedirectToAction("StructureList", "Home");
                            TempData["SuccessMessage"] = "Structure Created";
                        
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to Add";
                }
            }
            else if (validate1 != null)
            {
                TempData["ErrorMessage"] = ("CompanyCode " + viewModel.CompanyCode + " with CityCode " + viewModel.CityCode + "Already Exists");
            }
            else if (validate3 != null)
            {
                TempData["ErrorMessage"] = ("City Code " + viewModel.CityCode + " with City " + viewModel.CityName + " already Exists");
            }
            else if(validate != null && validate1 != null && validate3 != null)
            {
                TempData["ErrorMessage"] = ("Company Code " + viewModel.CompanyCode + " with City Code " + viewModel.CityCode + "and" + viewModel.CityName + " Already Exists");
            }
            else 
            {
                TempData["ErrorMessage"] = ("City with Name " + viewModel.CityName + " already Exists in Company Code " + viewModel.CompanyCode + "");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DefineSLPurchOrg()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize("Admin")]
        public ActionResult DefineSlPurchOrg(tblOrganizationStructure obj)
        {
            var validate = context.tblStructuredetails
                       .Where(a => a.CompanyCode == obj.CompanyCode)
                       .Where(b => b.CityCode == obj.CityCode).FirstOrDefault();
            var validate1 = context.tblOrganizationStructures.Where(a => a.CompanyCode == obj.CompanyCode).Where(b => b.CityCode == obj.CityCode)
                      .Where(c => c.StorageLocation == obj.StorageLocation)
                      .Where(d => d.PurchaseOrganization == obj.PurchaseOrganization).FirstOrDefault();
            var validate2 = context.tblOrganizationStructures
                      .Where(a => a.CompanyCode == obj.CompanyCode)
                      .Where(b => b.CityCode == obj.CityCode).FirstOrDefault();
            var validate3 = context.tblOrganizationStructures
                      .Where(c => c.StorageLocation == obj.StorageLocation)
                      .Where(d => d.PurchaseOrganization == obj.PurchaseOrganization).FirstOrDefault();

            if(validate == null)
            {
                TempData["ErrorMessage"] = ("CompanyCode " + obj.CompanyCode + " with CityCode " + obj.CityCode + " Does not Exists");
            }

            else if(validate2 != null && validate3 != null)
            {
                TempData["ErrorMessage"] = (" Storage Location " + obj.StorageLocation + " with Purchase Organization " + obj.PurchaseOrganization + "Already Exists for Company Code " + obj.CompanyCode + " and City Code " + obj.CityCode + " ");
            }
            else if (validate != null && validate1 == null && validate3 == null && validate2 != null )
            {
                if (ModelState.IsValid)
                {
                    List<object> lst = new List<object>();
                    lst.Add(obj.CompanyCode);
                    lst.Add(obj.CityCode);
                    lst.Add(obj.StorageLocation);
                    lst.Add(obj.PurchaseOrganization);
                    object[] item = lst.ToArray();
                    int output1 = context.Database.ExecuteSqlCommand("insert into tblOrganizationStructure(CompanyCode,CityCode,StorageLocation,PurchaseOrganization) values(@p0,@p1,@p2,@p3)", item);
                    if (output1 > 0)
                    {
                        TempData["SuccessMessage"] = ("Storage Location " + obj.StorageLocation + " and Purchase Org " + obj.PurchaseOrganization + " Defined for Company Code " + obj.CompanyCode + " and City Code " + obj.CityCode + "");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to Add";
                }
            }
            else if (validate2 == null && validate3 != null || validate2 == null && validate3 == null)
            {
                if (ModelState.IsValid)
                {
                    List<object> lst = new List<object>();
                    lst.Add(obj.CompanyCode);
                    lst.Add(obj.CityCode);
                    lst.Add(obj.StorageLocation);
                    lst.Add(obj.PurchaseOrganization);
                    object[] item = lst.ToArray();
                    int output1 = context.Database.ExecuteSqlCommand("insert into tblOrganizationStructure(CompanyCode,CityCode,StorageLocation,PurchaseOrganization) values(@p0,@p1,@p2,@p3)", item);
                    if (output1 > 0)
                    {

                        TempData["SuccessMessage"] = ("Storage Location " + obj.StorageLocation + " and Purchase Org " + obj.PurchaseOrganization + " Defined for Company Code " + obj.CompanyCode + " and City Code " + obj.CityCode + "");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Unable to Add";
                }
            }
            else
            {
                TempData["ErrorMessage"] = (" Storage Location " + obj.StorageLocation + " with Purchase Organization " + obj.PurchaseOrganization + "Already Exists for Company Code " + obj.CompanyCode + " and City Code " + obj.CityCode + " ");
            }
            return View(obj);
        }
        public ActionResult StructureList()
        {
            List<tblStructuredetail> sd = context.tblStructuredetails.ToList();
         
            return View(sd);
        }
        public ActionResult UnAuthorized()
        {
            ViewBag.Title = "Un Authorized Page!";
            return View();
        }
    }
}