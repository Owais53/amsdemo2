using amsdemo.DAL.Interface;
using amsdemo.DAL.Repository;
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
            IDepartmentRepository objdep = new DepartmentRepository();
            

            var check = objdep.GetAll().Where(a => a.DepartmentName == dep.DepartmentName).FirstOrDefault();

            if (check == null)
            {
                if (ModelState.IsValid)
                {
                    objdep.Add(dep);
                    objdep.Save();
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
            IDepartmentRepository objdep = new DepartmentRepository();

                var Data = (from user in objdep.GetAll()
                            select new
                            {
                                user.DepartmentId,
                                user.DepartmentName

                            }).ToList();


                return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
            

        }

        [HttpGet]
        public ActionResult Save(int id = 0)
        {
            IDepartmentRepository objdep = new DepartmentRepository();

            if (id == 0)
                return View(new tblDepartment());
            else
            {
                return View((objdep.GetAll().Where(x => x.DepartmentId == id)).FirstOrDefault<tblDepartment>());
            }

        }

        [HttpPost]
        public ActionResult Save(tblDepartment dep)
        {
            IDepartmentRepository objdep = new DepartmentRepository();

            if (dep.DepartmentId == 0)
            {
                objdep.Add(dep);
                objdep.Save();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                objdep.Update(dep);
                objdep.Save();
                TempData["UpdateMessage"] = "Updated Successfully";
                return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteDepartment(int id)
        {
                IDepartmentRepository objdep = new DepartmentRepository();
                objdep.Delete(id);

                objdep.Save();
                return Json(new { success = true, message = "Deleted Succesfully" }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult DefineStructure()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DefineStructure(StructureDetails viewModel)
        {
            IStructuredetailRepository objstruct = new StructuredetailRepository();


            var validate = objstruct.Getcondition(viewModel.CompanyCode,viewModel.CityCode,viewModel.CityName).FirstOrDefault();

            var validate1 = objstruct.Getcondition1(viewModel.CompanyCode,viewModel.CityCode).FirstOrDefault();

            var validate3 = objstruct.Getcondition2(viewModel.CityCode,viewModel.CityName).FirstOrDefault();

            var check = objstruct.Getcondition3(viewModel.CityCode).FirstOrDefault();

            var check1 = objstruct.Getcondition4(viewModel.CityName).FirstOrDefault();

            var check2 = objstruct.Getcondition5(viewModel.CompanyCode).FirstOrDefault();
                

            if (validate == null && validate3 != null || validate1 == null && check1 == null || !objstruct.GetAll().Any())
            {
                if (ModelState.IsValid)
                {

                    objstruct.Add(viewModel.CompanyCode,viewModel.CityCode,viewModel.CompanyName,viewModel.CityName);
                    objstruct.Save();
                    RedirectToAction("StructureList", "Home");
                    TempData["SuccessMessage"] = "Structure Created";


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
            IStructuredetailRepository objstruct = new StructuredetailRepository();

            var validate = objstruct.Getcondition1(obj.CompanyCode,Convert.ToInt32(obj.CityCode)).FirstOrDefault();

            var validate1 = objstruct.Getvalidation(obj.CompanyCode, Convert.ToInt32(obj.CityCode), obj.StorageLocation,obj.PurchaseOrganization).FirstOrDefault();
               
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