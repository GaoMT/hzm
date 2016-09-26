using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetDataMine.Models;

namespace InternetDataMine.Controllers
{
    public class ProcessLogController : Controller
    {

        /// <summary>
        /// 调出查询日志页面
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public ActionResult ProcessLogQuery()
        {
           
            return View();
        }

        /// <summary>
        /// 返回查询日志数据
        /// </summary>
        /// <param name="BeingTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        public void ProcessLogData(string BeginTime,string EndTime,string ProcessName)
        {
            ProcessLogInfoModel model = new ProcessLogInfoModel();
            
            string Result = model.ProcessLogInfoQuery(BeginTime,EndTime,ProcessName);
            Response.Write(Result);
            Response.End();
            
        }
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="ProcessLogName">操作人姓名</param>
        /// <param name="ProcessLogContent">操作内容</param>
        public void ProcessLogAdd(string ProcessLogName,string ProcessLogContent)
        {
            ProcessLogInfoModel model = new ProcessLogInfoModel() { ProcessName=ProcessLogName, ProcessContent=ProcessLogContent };
            model.ProcessLogInfo_Insert();
        }
    }
}
