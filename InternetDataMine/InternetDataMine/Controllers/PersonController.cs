using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetDataMine.Models;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Converters;
using InternetDataMine.Models.DataService;

namespace InternetDataMine.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/
        public ActionResult Index()
        {
            PersonModel pmodel = new PersonModel() { Name = "张三", Age = 23, Remark = "备注" };

            return View(pmodel);
        }
        [HttpPost]
        public ActionResult DEY(string Name, int Age, string Remark)
        {
            PersonModel pmodel = new PersonModel();

            pmodel.Name = Name;
            pmodel.Age = Age;
            pmodel.Remark = Remark;

            return View(pmodel);

        }

        public ActionResult RealTimePersonInMineForMain()
        {
            return View();
        }

        public ActionResult DEY1(string Name, int Age, string Remark)
        {
            PersonModel pmodel = new PersonModel();

            pmodel.Name = Name;
            pmodel.Age = Age;
            pmodel.Remark = Remark;

            return View("DEY", pmodel);

        }

        public string Getjson()
        {
            PersonModel pmodel = new PersonModel();
            return pmodel.ReturnPersons();
        }
        public ActionResult CeTest()
        {
            //PersonModel pmodel = new PersonModel();
            //pmodel.Name = Name;
            //pmodel.Age = Age;
            //pmodel.Remark = Remark;
            return View();
        }

        public ActionResult SupcanTest()
        {
            PersonModel pmodel = new PersonModel();
            return View(pmodel);
        }

        public ActionResult RealTimePersonInMine()
        {
            return View();
        }

        /// <summary>
        /// 加载煤矿树形列表数据
        /// </summary>
        public void GetMineTreeListData(string mineCode)
        {

                BaseInfoModel model = new BaseInfoModel();
                string json = model.GetMineTreeList(mineCode);
                Response.Write(json);
                Response.End();
           
        }

        /// <summary>
        /// 获取人员实时轨迹
        /// </summary>
        /// <param name="MineCode">煤矿编号</param>
        /// <param name="JobCardCode">人员标示卡号</param>
        public void GetRTTrack(string MineCode, string JobCardCode)
        {
            DataBLL bll = new DataBLL();
            string ResponseText = "[]";
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            DataTable dt = bll.GetRTTrack(MineCode, JobCardCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                ResponseText = JsonConvert.SerializeObject(dt,Formatting.Indented,timeConverter);
            }

            ResponseText = "{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":"+ResponseText+"}";

            Response.Write(ResponseText);
            Response.End();
        }

        /// <summary>
        /// 获取人员下井信息
        /// </summary>
        /// <param name="minecode">煤矿编号</param>
        /// <returns></returns>
        public string ReturnXX(string minecode)
        {

            InternetDataMine.Models.DataService.DataBLL bll = new Models.DataService.DataBLL();
            DataTableCollection dtc = bll.GetRealDataForRY(minecode, "", "");

            //Random r = new Random();
            //int x = r.Next(1, 180);

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Name", typeof(string));//姓名
            //dt.Columns.Add("dutyid", typeof(string));
            //dt.Columns.Add("duty", typeof(string));//职务
            //dt.Columns.Add("deptname", typeof(string));//部门
            //dt.Columns.Add("deptid",typeof(string));
            //dt.Columns.Add("stationPosion", typeof(string));//当前所处分站位置
            //dt.Columns.Add("stationaddress", typeof(string));
            //dt.Columns.Add("Intime", typeof(string));//进入分站时刻
            //dt.Columns.Add("InMineTime", typeof(string));
            //dt.Columns.Add("Remark", typeof(string));//备注

            //for (int i = x; i < 200; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["Name"] = "欧阳俊" + i.ToString();
            //    dr["dutyid"] = (i % 5 == 0 ? 0 : 1).ToString();
            //    dr["duty"] = i % 5 == 0 ? "领导" : "工人";
            //    dr["deptname"] = "综采_" + (i / 20 + 1).ToString() + "_部";
            //    dr["deptid"] = (i / 20 + 1).ToString();
            //    dr["stationPosion"] = "采掘巷_" + (i / 50 + 1).ToString() + "_区";
            //    dr["stationaddress"] = (i / 50 + 1).ToString();
            //    dr["Intime"] = DateTime.Now.ToString("MM-dd HH:mm:ss");
            //    dr["InMineTime"] = DateTime.Now.AddMinutes(-1 * i - 60).ToString("MM-dd HH:mm:ss");
            //    dr["Remark"] = "";
            //    dt.Rows.Add(dr);
            //}

            if(dtc.Count==0)
            {
                return "[]";
            }
            return JsonConvert.SerializeObject(dtc[1]);

        }
    }
}
