using Dapper;
using Demo20230222.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;


namespace Demo20230222.Services
{
    public class EmpMthods
    {
        // 使用Dapper建立連線
        public readonly string connStr = ConfigurationManager.ConnectionStrings["DemoDb"].ConnectionString;
        //查全部或單一資料
        public (List<EmpData>, MessageInfo) GetEmpData(string EmpNO)
        {
            if (string.IsNullOrWhiteSpace(EmpNO))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        var result = conn.Query<EmpData>("SELECT * FROM EmpData").ToList();
                        if (result.Count == 0)
                        {
                            return (new List<EmpData>(), new MessageInfo { IsSuccess = false, Msg = "無資料，請先新增資料。" });
                        }
                        return (result, new MessageInfo { IsSuccess = true, Msg = "" });
                    }
                    catch (Exception ex)
                    {
                        return (new List<EmpData>(), new MessageInfo { IsSuccess = false, Msg = ex.ToString() });
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    var reult = conn.Query<EmpData>("SELECT * FROM EmpData WHERE EmpNo = @EmpNo", new { EmpNO = EmpNO }).ToList();
                    if (reult.Count == 0)
                    {
                        return (new List<EmpData>(), new MessageInfo { IsSuccess = false, Msg = "查無資料。" });
                    }
                    return (reult, new MessageInfo { IsSuccess = true, Msg = "" });
                }
                catch (Exception ex)
                {
                    return (new List<EmpData>(), new MessageInfo { IsSuccess = false, Msg = ex.ToString() });
                }
            }
        }
        //// 編輯
        //public MessageInfo EditEmpData(EmpData empData)
        //{
        //    string strSQL = "UPDATE EmpData SET Name = @Name, Ext = @Ext WHERE EmpNo = @EmpNo";
        //    using (SqlConnection conn = new SqlConnection(connStr))
        //    {
        //        try
        //        {
        //            var result = conn.Execute(strSQL, new { Name = empData.Name, Ext = empData.Ext, EmpNo = empData.EmpNo });
        //            if (result == 1)
        //            {
        //                return new MessageInfo { IsSuccess = true, Msg = "" };
        //            }
        //            return new MessageInfo { Msg = "修改失敗", IsSuccess = false };
        //        }
        //        catch (Exception ex)
        //        {
        //            return new MessageInfo { IsSuccess = false, Msg = ex.ToString() };
        //        }

        //    }
        //}
    }
}

