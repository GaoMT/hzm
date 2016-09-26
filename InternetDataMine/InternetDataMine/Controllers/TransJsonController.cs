using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetDataMine.Models;

namespace InternetDataMine.Controllers
{
    public class TransJsonController : Controller
    {
        //
        // GET: /TransJsonToTreeList/

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult TransJsonToTreeList(string DataType)
        //{
        //    TransJsonToTreeListModel model = new TransJsonToTreeListModel(DataType);
        //    return View(model);
        //}

        public void  TransJsonToTreeList(string SystemType, string DataType, string MineCode, string SensorNum, string SensorType, string DropListName, string ReportName, string startRow, string rows, string StartTime, string EndTime, string TypeName, string DropName, string Position)
        {
            try
            {
            int StartRow = 0;
            int Rows = 0;
            if (startRow != null && rows != null && startRow != "" && rows != "" && startRow != "NaN" && startRow != "NaN")
            {
                StartRow = int.Parse(startRow);
                Rows = int.Parse(rows);
            }
            if (DataType == "AQGZ")
            {
                string a = DropName;
            }
            TransJsonToTreeListModel model = new TransJsonToTreeListModel(SystemType, DataType, MineCode, SensorNum, SensorType, DropListName, ReportName, StartRow, Rows, StartTime, EndTime, TypeName, DropName, Position);

            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");    
                
            //数据处理并发送数据到前台
            Response.Write(model.GetDataJson);
            
            //通知前台，数据发送完毕
            Response.Flush();
            Response.End();
           
            }
            catch(Exception ex)
            {

            }
            finally
            {
               // Response.Close();
            }
        }
    }
}
