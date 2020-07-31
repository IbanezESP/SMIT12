using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 大專.Models;
using 大專.Models.ViewModel;


namespace 大專.Controllers
{
    public class HomeController : Controller
    {
        private FurnitureEntities1 db = new FurnitureEntities1();

        public ActionResult MemberCentre(int? id)
        {
            if (id == null)
            {
                //return View("MemberCentre");
                return Redirect($"~/Home/ChangePassword/1");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }


            var data = from a in db.Members
                       join b in db.Orders on a.MemberID equals b.MemberID
                       join c in db.OrderDetails on b.OrderID equals c.OrderID
                       join d in db.Products on c.ProductId equals d.ProductID
                       where a.MemberID == id
                       select new EmpViewModel { member = a.ToString(), order = b.ToString(), orderDetail = c.ToString(), product = d.ToString() };
            Session["userID"] = id;
            Session["userImg"] = id + ".jpg";
            return View(member);
        }

        //[HttpGet]
        //public ActionResult ChangePassword(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Member member = db.Members.Find(id);
        //    if (member == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(member);
        //}
        //[HttpPost]
        //public ActionResult ChangePassword([Bind(Include = "Passwrod")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(member).State = EntityState.Modified;
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //        //db.SaveChanges();
        //        return RedirectToAction("MemberCentre");
        //    }
        //    return View(member);
        //}

        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return View("MemberCentre");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            Session["userID"] = id;
            Session["userImg"] = id + ".jpg";
            return View(member);
        }
        [HttpPost]
        public ActionResult ChangePassword(string password, string newPassword, string confirmNewPassword, int? id)
        {
            Member member = db.Members.Find(id);

            var login = db.Members.Where(u => u.MemberID == member.MemberID).FirstOrDefault();
            if (login.Passwrod == password)
            {
                if (confirmNewPassword == newPassword)
                {
                    //login.confirmNewPassword = confirmNewPassword;
                    //login.newPassword = newPassword;
                    login.Passwrod = confirmNewPassword;
                    db.Entry(login).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('密碼修改完成!');</script>";
                }
                else
                {
                    TempData["msg"] = "<script>alert('新密碼與確認密碼不符!');</script>";
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('目前密碼輸入錯誤!');</script>";
            }
            return Redirect($"~/Home/ChangePassword/{id}");
        }
        //[HttpGet]
        //public ActionResult ChangeProfile()
        //{
        //    var data = from b in db.Members where b.MemberID == 2 select b;
        //    List<Member> mList = data.ToList();
        //    return View(mList);
        //}

        [HttpGet]
        public ActionResult ChangeProfile(int? id)
        {
            if (id == null)
            {
                return View("MemberCentre");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            Session["userID"] = id;
            Session["userImg"] = id + ".jpg";
            return View(member);
        }
        //[HttpPost]
        //public ActionResult ChangeProfile(string userUID)
        //{
        //    var update = db.Members.Where(u => u.MemberID == 2).FirstOrDefault();
        //    update.Uid = userUID;
        //    db.Entry(update).State = EntityState.Modified;
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult ChangeUser(int? id)
        {
            if (id == null)
            {
                return View("MemberCentre");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            Session["userID"] = id;
            Session["userImg"] = id + ".jpg";
            return View(member);
        }
        [HttpPost]
        public ActionResult ChangeUser(string userName, string address, int? id)
        {
            Member member = db.Members.Find(id);

            var update = db.Members.Where(u => u.MemberID == member.MemberID).FirstOrDefault();
            if (userName != "" && address != "")
            {
                update.Username = userName;
                update.Address = address;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "<script>alert('個人資料修改完成!');</script>";
            }
            else if (userName == "" || address == "")
            {
                TempData["msg"] = "<script>alert('欄位不可為空!');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('欄位不可為空!');</script>";
            }
            return Redirect($"~/Home/ChangeUser/{id}");
        }


        public ActionResult UploadImg(int? id)
        {
            if (id == null)
            {
                return View("MemberCentre");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            Session["userID"] = id;
            Session["userImg"] = id + ".jpg";
            return View(member);
        }
        [HttpPost]
        public ActionResult UploadImg(HttpPostedFileBase file, int? id)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = file.FileName;
                    var filePath = Server.MapPath(string.Format("~/{0}", "img/member-img"));
                    file.SaveAs(Path.Combine(filePath, id.ToString() + ".jpg"));
                    TempData["msg"] = "<script>alert('上傳圖片成功!');</script>";
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('上傳圖片失敗!');</script>";
            }
            return Redirect($"~/Home/UploadImg/{id}");
        }
    }
}