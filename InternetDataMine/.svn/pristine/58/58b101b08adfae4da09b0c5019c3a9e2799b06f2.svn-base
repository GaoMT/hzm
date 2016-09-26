using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using InternetDataMine.Models;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace InternetDataMine.Controllers
{
    public class ChartController : Controller
    {
        //
        // GET: /Charts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MCDay(string PageName, string UserAbility, string MineCode, string Height)
        {
            LoadModel loadModel = new LoadModel();
            loadModel.Height = Height;

            loadModel.UserAbility = UserAbility;
            loadModel.UserMineCode = MineCode;

            return View();
        }

        /// <summary>
        /// 加载开关量查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult SwitchQantity()
        {
            return View();
        }


        /// <summary>
        /// 加载模拟量查询数据
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult AnalogQantity()
        {
            return View();
        }

        /// <summary>
        /// 返回模拟量数据
        /// </summary>
        /// <param name="mineCode"></param>
        /// <param name="sensorCodes"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        public void ReturnCurverDatas(string mineCode, string sensorCodes, string BeginTime, string EndTime)
        {


          DataBLL bll = new DataBLL();
          DataTableCollection dts = bll.ReturnCurverDatas(mineCode, sensorCodes, BeginTime, DateTime.Parse(BeginTime).AddDays(1).ToString("yyyy-MM-dd"));
            string json="[]";
            //在对DATATABLE进行序列化的时候，规范日期格式
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                if(dts!=null&&dts.Count>0)
                {
                    json = JsonConvert.SerializeObject(dts, Formatting.Indented, timeConverter);
                }
                Response.Write(json);

                Response.End();

        }

        public void ReturnSwitchDatas(string mineCode,string sensorCodes,string BeginTime,string EndTime)
        {
            DataBLL bll = new DataBLL();
            DataTableCollection dts = bll.ReturnSwitchDatas(mineCode, sensorCodes, BeginTime, DateTime.Parse(BeginTime).AddDays(1).ToString("yyyy-MM-dd"));
            string json = "[]";
            //在对DATATABLE进行序列化的时候，规范日期格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            if (dts != null && dts.Count > 0)
            {
                json = JsonConvert.SerializeObject(dts, Formatting.Indented, timeConverter);
            }
            Response.Write(json);

            Response.End();
        }

        public void AnalogQantityQuery(string MineCode,string SensorType,string SensorNum)
        {
            
        }

        /// <summary>
        /// 获取煤矿下拉列表信息
        /// </summary>
        public void GetMineInfoComboTree()
        {
            BaseInfoModel bim = new BaseInfoModel();
            bim.ReturnComboTreeMineInfo("");
        }
    }
}
