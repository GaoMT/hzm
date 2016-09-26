using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InternetDataMine.Models;
using InternetDataMine.Models.DataService;

namespace InternetDataMine.Controllers
{
    public class WorkShiftController : Controller
    {
        DataBLL bll = new DataBLL();
        //
        // GET: /WorkShift/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 值班交接班查询
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkShiftQuery()
        {
            return View();
        }

        public ActionResult WorkShiftManage()
        {
            return View();
        }

        public void WorkShift_Delete(string Condition)
        {
            if (bll.WorkShift_Delete(" RowID in (" + Condition + ")"))
            {
                Response.Write("<span style='font-color:green'>删除交接班信息成功！</span>");
            }
            else
            {
                Response.Write("<span style='font-color:red'>删除交接班信息失败！</span>");
            }
            Response.End();
        }



        public void WorkShift_Query(string MineCode, string StartTime, string EndTime, string WorkShiftCategory)
        {
            DataTable dt = bll.WorkShift_Query(string.Format(" wi.MineCode like '%{0}%' and wi.NextWorkTime>='{1}' and wi.NextWorkTime<='{2}' and wi.WorkShiftCategory={3}", MineCode, StartTime, EndTime, WorkShiftCategory));

            //在对DATATABLE进行序列化的时候，规范日期格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            Response.Write("{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":" + JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter).Replace("shine998", "<br>") + "}");
            Response.End();
        }

        public void WorkShift_Save(string RowID, string MineCode, string Dept, string PreWorkTime, string PreWorkContent, string PreWorkShift, string PreWorkPersonName, string NextWorkTime, string WorkShiftCategory, string NextWorkContent, string NextWorkShift, string NextWorkPersonName, string Remark, string Insert)
        {
            WorkShiftModel model = new WorkShiftModel() { RowID = int.Parse(RowID), MineCode = MineCode, Dept = Dept, PreWorkTime = PreWorkTime, PreWorkContent = PreWorkContent, PreWorkShift = PreWorkShift, PreWorkPersonName = PreWorkPersonName, NextWorkTime = NextWorkTime, NextWorkContent = NextWorkContent, NextWorkShift = NextWorkShift, NextWorkPersonName = NextWorkPersonName, Remark = Remark, WorkShiftCategory = int.Parse(WorkShiftCategory) };
            if(Insert=="1")
            {
                if(bll.WorkShift_Insert(model))
                {
                    Response.Write("交接班记录保存成功！");
                }
                else
                {
                    Response.Write("交接班记录保存失败！");
                }
            }
            else
            {
                if (bll.WorkShift_Update(model))
                {
                    Response.Write("交接班记录保存成功！");
                }
                else
                {
                    Response.Write("交接班记录保存失败！");
                }
            }
            Response.End();
        }
    }
}
