using Dapper;
using Demo20230222.Models;
using Demo20230222.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace Demo20230222.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private readonly string _connectString =
    @"Server=(LocalDB)\MSSQLLocalDB;Database=Demo20230222;Trusted_Connection=True;";

        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _EmpData(string EmpNo = "")
        {
            EmpMthods empMthods = new EmpMthods();
            var result = empMthods.GetEmpData(EmpNo);
            ViewBag.MsgInfo = result.Item2;
            return PartialView(result.Item1);
        }

        //編輯 Edit
        public ActionResult Edit(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo();
            if (Session["EmpData"] == null)
            {
                return View(new EmpData());
            }

            EmpData result =
                ((List<EmpData>)Session["EmpData"]).Where(x => x.EmpNo == EmpNo).FirstOrDefault();

            return View(result);

        }
        //編輯Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpData employee)
        {
            var sql =
    @"
        UPDATE EmpData SET Name = @Name, Ext = @Ext WHERE EmpNo = @EmpNo";
            
            using (SqlConnection conn = new SqlConnection(_connectString))
            
            {
                try
                {
                    var result = conn.QueryFirstOrDefault<string>(sql, employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                    
                }
            }

        }
        //新增 Create
        public ActionResult Create()
        {
            return View();
        }
        //新增Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpData employee)
        {
           MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            var sql =
    @"
        INSERT INTO EmpData 
        (
            [EmpNo]
           ,[Name]
           ,[Ext]
        ) 
        VALUES 
        (
            @EmpNo
           ,@Name
           ,@Ext
      
        );";
            // @IDENTITY --> 該筆資料的 ID 拉回來方便後續檢查和使用
            using (SqlConnection conn = new SqlConnection(_connectString))
            {
                try
                {
                    var result = conn.QueryFirstOrDefault<string>(sql, employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index");
                }
            }
        }
        //刪除 Delete
        [HttpPost]    
        public JsonResult Delete(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Msg = "" };
            
            var sql =
               @"
            DELETE FROM EmpData
            WHERE EmpNo = @EmpNo
        ";

            var parameters = new DynamicParameters();
            parameters.Add("EmpNo", EmpNo);

            using (SqlConnection conn = new SqlConnection(_connectString))
            //using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.Execute(sql, parameters);
                return Json(messageInfo);

            }
           
            }
    }
    }
    
