using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace InternetDataMine.Models
{
    public class ProcessLogInfoModel
    {

        
        /// <summary>
        /// 日志ID
        /// </summary>
        public int ProcessID { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string ProcessContent { get; set; }

        /// <summary>
        /// 操作日期
        /// </summary>
        public string ProcessDateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }



        /// <summary>
        /// 插入日志
        /// </summary>
        public void ProcessLogInfo_Insert()
        {
            DataBLL bll = new DataBLL();
             bll.ProcesslogInfo_Insert(this);
            
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="BeingTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        public string ProcessLogInfoQuery(string BeingTime, string EndTime,string ProcessUserName)
        {
            DataBLL bll = new DataBLL();
            DataTable dt = bll.ProcessLogInfo_Query(BeingTime, EndTime, ProcessUserName);
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return "{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":" +
                JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter).Replace("shine998", "<br>") + "}";

        }

    }
}