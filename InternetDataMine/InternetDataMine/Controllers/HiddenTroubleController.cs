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
    public class HiddenTroubleController : Controller
    {

        DataBLL bll = new DataBLL();
        //
        // GET: /HiddenTrouble/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 加载企业隐患自查-隐患处置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenTroubleSelfProcess()
        {
            return View();
        }

        /// <summary>
        /// 加载企业隐患自查-隐患排查界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenTroubleSelfCensor()
        {
            return View();
        }

        /// <summary>
        /// 加载企业隐患自查-隐患复查界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenTroubleSelfRecheck()
        {
            return View();
        }

        /// <summary>
        /// 加载监管机构隐患抽查-隐患抽查界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenMonitorCensor()
        {
            return View();
        }

        /// <summary>
        /// 加载监管机构隐患抽查-隐患处理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenMonitorProcess()
        {
            return View();
        }

        /// <summary>
        /// 加载监管机构隐患抽查-隐患复查界面
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenMonitorRecheck()
        {
            return View();
        }

        /// <summary>
        /// 加载企业隐患监管
        /// </summary>
        /// <returns></returns>
        public ActionResult HiddenMonitorEnterpriseRecheck()
        {
            return View();
        }

        #region 隐患复查
        /// <summary>
        /// 保存隐患复查
        /// </summary>
        /// <param name="RowID">自增列</param>
        /// <param name="TroubleID">隐患排查自增列</param>
        /// <param name="RecheckCategory">隐患复查单位类别</param>
        /// <param name="TroubleProcessID">隐患处理自增列</param>
        /// <param name="TroubleRecheckDate">隐患复查日期</param>
        /// <param name="TroubleRechecker">隐患复查人</param>
        /// <param name="TroubleRecheckResult">隐患复查意见</param>
        /// <param name="IsPass">隐患是否已消除</param>
        /// <param name="Remark">备注</param>
        /// <param name="Insert">数据保存类别 1插入 0更新</param>
        public void HiddenTrouble_Recheck_Save(string RowID,string TroubleID, string RecheckCategory, string TroubleProcessID, string TroubleRecheckDate, string TroubleRechecker, string TroubleRecheckResult, string IsPass, string Remark,string Insert)
        {
            TroubleRecheckModel model = new TroubleRecheckModel() { RowID = int.Parse(RowID), 
                TroubleID = int.Parse(TroubleID), TroubleProcessID = int.Parse(TroubleProcessID),
                                                                    RecheckCategory = int.Parse(RecheckCategory),
                                                                    TroubleRecheckDate = DateTime.Parse(TroubleRecheckDate),
                                                                    TroubleRechecker = TroubleRechecker,
                                                                    TroubleRecheckResult = TroubleRecheckResult.Replace("shine998", "<br>"),
                                                                    IsPass = int.Parse(IsPass),
                                                                    Remark = Remark.Replace("shine998", "<br>")
            };

            switch (Insert)
            {
                case "1":
                    if (bll.HiddenTrouble_Recheck_Insert(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患复查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患复查信息失败！</span>");
                    }
                    break;
                default:
                    if (bll.HiddenTrouble_Recheck_Update(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患再复查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患再复查信息失败！</span>");
                    }
                    break;
            }

            Response.End();
        }



        /// <summary>
        /// 查询隐患复查内容
        /// </summary>
        /// <param name="MineCode">煤矿编号</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="CheckCategory">查询单位类别</param>
        public void HiddenTrouble_Recheck_Query(string MineCode,string StartTime,string EndTime,string CheckCategory)
        {
             string Condition = string.Empty;
            Condition += " CheckDate >='" + StartTime + "' and CheckDate<='" + EndTime + "'";
            if (MineCode != null && MineCode != "")
            {
                Condition += " and mineCode like '%" + MineCode + "%'";
            }
            if (CheckCategory != null)
            {
                Condition += " and CheckCategory=" + CheckCategory;
            }

            DataTable dt = bll.HiddenTrouble_Recheck_Query(Condition);

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            Response.Write("{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":" + JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter) + "}");
            Response.End();

        }


        #endregion

        #region 隐患整改


        /// <summary>
        /// 根据条件查询隐患整改信息
        /// </summary>
        /// <param name="MineCode">煤矿编号</param>
        /// <param name="StartTime">查询开始时间</param>
        /// <param name="EndTime">查询结束时间</param>
        /// <param name="CheckCategory">隐患处理单位类别 1代表企业 2代表监管机构</param>
        public void HiddenTrouble_CheckProcess_Query(string MineCode,string StartTime,String EndTime,string CheckCategory)
        {
            string Condition = string.Empty;
            Condition += " CheckDate >='" + StartTime + "' and CheckDate<='" + EndTime + "'";
            if (MineCode != null && MineCode != "")
            {
                Condition += " and mineCode like '%" + MineCode + "%'";
            }
            if (CheckCategory != null)
            {
                Condition += " and CheckCategory=" + CheckCategory;
            }

            DataTable dt = bll.HiddenTrouble_CheckProcess_Query(Condition);

            //在对DATATABLE进行序列化的时候，规范日期格式
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            Response.Write("{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":" + JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter) + "}");
            Response.End();
        }


         /// <summary>
        /// 保存隐患信息
        /// </summary>
        /// <param name="RowID">自增列</param>
        /// <param name="TroubleID">隐患自增列</param>
        /// <param name="ProcessCategory">隐患处理单位类别 1代表企业 2代表监管机构</param>
        /// <param name="TroubleProcessContent">隐患处理内容</param>
        /// <param name="TroubleProcessDate">隐患处理开始日期</param>
        /// <param name="TroubleProcesser">隐患处理监督人</param>
        /// <param name="TroubleProcessCompleteDate">隐患处理截止日期</param>
        /// <param name="Remark">备注</param>
        /// <param name="Insert">插入/更新的标志</param>
        public void HiddenTrouble_Process_Save(string RowID, string TroubleID, string ProcessCategory, string TroubleProcessContent, string TroubleProcessDate, string TroubleProcesser, string TroubleProcessCompleteDate, string Remark, string Insert)
        {
            TroubleProcessModel model = new TroubleProcessModel()
            {
                RowID = int.Parse(RowID),
                TroubleID = int.Parse(TroubleID),
                ProcessCategory = int.Parse(ProcessCategory),

                TroubleProcessContent = TroubleProcessContent.Replace("shine998", "<br>"),
                TroubleProcessDate = TroubleProcessDate,
                TroubleProcesser = TroubleProcesser,
                TroubleProcessCompleteDate = TroubleProcessCompleteDate,
                Remark = Remark.Replace("shine998", "<br>")
            };

            switch (Insert)
            {
                case "1":
                    if (bll.HiddenTrouble_Process_Insert(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患排查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患排查信息失败！</span>");
                    }
                    break;
                default:
                    if (bll.HiddenTrouble_Process_Update(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患排查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患排查信息失败！</span>");
                    }
                    break;
            }


            Response.End();
        }

        #endregion

        #region 隐患排查
        /// <summary>
        /// 根据条件查询隐患信息
        /// </summary>
        /// <param name="MindeCode">煤矿编号</param>
        /// <param name="StartTime">查询开始时间</param>
        /// <param name="EndTime">查询结束时间</param>
        public void HiddenTrouble_Query(string MineCode, string StartTime, string EndTime, string CheckCategory)
        {
            string Condition = string.Empty;
            Condition += " CheckDate >='" + StartTime + "' and CheckDate<='" + EndTime + "'";
            Condition += " and a.mineCode like '%" + MineCode + "%'";

            if (CheckCategory != null)
            {
                Condition += " and CheckCategory=" + CheckCategory;
            }

            DataTable dt= bll.HiddenTouble_Check_Query(Condition);

            //在对DATATABLE进行序列化的时候，规范日期格式
            IsoDateTimeConverter timeConverter=new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            Response.Write("{\"total\":"+dt.Rows.Count.ToString()+",\"rows\":"+JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter)+"}");
            Response.End();
        }


        /// <summary>
        /// 根据条件删除隐患信息
        /// </summary>
        /// <param name="Condition">查询条件，自增ID集合</param>
        public void HiddenTrouble_Delete(string Condition)
        {
           if(bll.HiddenTouble_Check_Delete(" RowID in  (" + Condition + ")"))
            {
                 Response.Write("<span style='font-color:green'>删除隐患排查信息成功！</span>");
            }
            else
            {
                Response.Write("<span style='font-color:red'>删除隐患排查信息失败！</span>");
            }
           Response.End();
        }

        /// <summary>
        /// 保存隐患信息
        /// </summary>
        /// <param name="RowID">自增ID</param>
        /// <param name="CheckCategory">隐患排查单位类别 1代表企业 2代表监管机构</param>
        /// <param name="MineCode">煤矿编号</param>
        /// <param name="TroubleClass">隐患等级</param>
        /// <param name="TroubleCategory">隐患类别</param>
        /// <param name="CheckDept">检查部门</param>
        /// <param name="Rummager">检查人</param>
        /// <param name="CheckDate">检查日期</param>
        /// <param name="HiddenResponsibilityDept">隐患责任部门</param>
        /// <param name="TroubleContent">隐患内容</param>
        /// <param name="Remark">备注</param>
        /// <param name="Insert">保存类别 1为插入，2为更新</param>
        public void HiddenTrouble_Save(string RowID, string CheckCategory, string MineCode, string TroubleClass, string TroubleCategory, string CheckDept, string Rummager, string CheckDate, string HiddenResponsibilityDept, string TroubleContent, string Remark, string Insert)
        {
            TroubleCheckModel model = new TroubleCheckModel()
            {
                RowID =int.Parse(RowID),
                CheckCategory = int.Parse(CheckCategory),
                MineCode = MineCode,
                TroubleClass = int.Parse(TroubleClass),
                TroubleCategory = int.Parse(TroubleCategory),
                CheckDept = CheckDept,
                Rummager = Rummager,
                CheckDate = CheckDate,
                HiddenResponsibilityDept = HiddenResponsibilityDept,
                TroubleContent = TroubleContent.Replace("shine998", "<br>"),
                Remark = Remark.Replace("shine998", "<br>")
            };

            switch(Insert)
            {
                case "1":
                    if (bll.HiddenTouble_Check_Insert(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患排查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患排查信息失败！</span>");
                    }
                    break;
                default:
                    if (bll.HiddenTouble_Check_Update(model))
                    {
                        Response.Write("<span style='font-color:green'>保存隐患排查信息成功！</span>");
                    }
                    else
                    {
                        Response.Write("<span style='font-color:red'>保存隐患排查信息失败！</span>");
                    }
                    break;
            }

           
            Response.End();
        }

        #endregion
    }
}
