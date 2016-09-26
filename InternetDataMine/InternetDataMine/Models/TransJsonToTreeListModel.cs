using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json.Converters;
namespace InternetDataMine.Models
{
    public class TransJsonToTreeListModel
    {
        #region [变量]
        InternetDataMine.Models.DataService.DataBLL _BLL_Data = new DataService.DataBLL();
        InternetDataMine.Models.DataService.ReportDataBLL _BLL_Report = new DataService.ReportDataBLL();
        string TreeListType = string.Empty;
        string MineCode = string.Empty;
        string SensorNum = string.Empty;
        string DevType = string.Empty;
        string DropListType = string.Empty;
        string Position = string.Empty;
        string Name = string.Empty;
        string ReportName = string.Empty;
        DateTime BeginTime = new DateTime();
        DateTime EndTime = new DateTime();
        string TypeName = string.Empty;
        int SystemType;
        int StartRow;
        int Rows;
        string DropName = string.Empty;
        string m_deviceKind = null;

        #endregion

        //public TransJsonToTreeListModel(string Type)
        //{
        //    TreeListType = Type;            
        //}
        public TransJsonToTreeListModel(string SystemType, string DataType, string MineCode, string SensorNum, string DevType, string DropListName, string ReportName, int startRows, int rows, string StartTime, string EndTime, string TypeName, string DropName, string p_position = null)
        {
           
            TreeListType = DataType;
            this.MineCode = MineCode;
            this.SensorNum = SensorNum;
            this.DevType = DevType;
            this.m_deviceKind = p_position;
            if (StartTime != "" && StartTime != null)
            {
                this.BeginTime = Convert.ToDateTime(StartTime);
            }
            else
            {
                this.BeginTime = DateTime.Now;
            }
            if (EndTime != "" && EndTime != null)
            {
                this.EndTime = Convert.ToDateTime(EndTime);
            }
            else
            {
                this.EndTime = DateTime.Now;
            }
            this.DropName = DropName;
            this.TypeName = DevType;
            this.DropListType = DropListName;
            this.ReportName = ReportName;
            this.StartRow = startRows;
            this.Rows = rows;
            this.Position = p_position;
            if (SystemType != null)
            { this.SystemType = int.Parse(SystemType); }
        }
        /// <summary>
        /// 获取Data Json
        /// </summary>
        /// <returns></returns>
        public string GetDataJson
        {
            get
            {
                if (Rows != 0)
                {
                    _BLL_Data.PageIndex = StartRow / Rows;
                    _BLL_Data.PageSize = Rows;
                }
                else
                {
                    _BLL_Data.PageIndex = 0;
                    _BLL_Data.PageSize = 100;
                }
                DataTable Result = null;
                DataTableCollection Results;
                EnumDataType VIEW = (EnumDataType)Enum.Parse(typeof(EnumDataType), TreeListType);//字符串转化为枚举
                Results = null;
                #region 枚举方式
                switch (VIEW)
                {
                    case EnumDataType.ShowAQJKRTData:
                        Result = new DataTable();
                        return "";

                    #region [加载查询bar数据]
                    case EnumDataType.Filter:
                        if (SystemType == 1)
                        {
                            switch (DropName)
                            {
                                case "DevType": Result = _BLL_Data.GetDevTypeList(MineCode); break;
                                case "SensorNum": Result = _BLL_Data.GetRealDataForAQList(MineCode, DevType, SystemType.ToString()); break;
                            }

                        }
                        else
                        {
                            switch (DropListType)
                            {
                                case "DevType": _BLL_Data.GetRealDataForAQList(MineCode, DevType, SystemType.ToString()); break;
                                case "FZ": Result = _BLL_Data.GetRYFZList(MineCode); break;
                                case "QY": Result = _BLL_Data.GetRYQYList(MineCode); break;
                                case "Card": Result = _BLL_Data.GetRYXXList(MineCode, Position, Name); break;
                                case "Name": Result = _BLL_Data.GetRYXXList(MineCode, Position, Name); break;
                            }
                        }
                        break;
                    #endregion

                    #region [安全监控数据查询]
                    //获取所有煤矿信息  
                    case EnumDataType.MineName: Result = _BLL_Data.MineList(); break;
                    //获取测点编号 
                    case EnumDataType.Sensor: Results = _BLL_Data.GetRealDataForAQ(MineCode); break;
                    //获取所有设备名
                    case EnumDataType.DevType: Result = m_deviceKind == null ? _BLL_Data.DeviceList() : _BLL_Data.DeviceList(m_deviceKind); break;
                    //获取实时数据
                    case EnumDataType.RealData: Results = _BLL_Data.GetRealDataForAQ(MineCode, DevType, SensorNum, "","1"); break;
                    //加载煤矿信息内容 
                    case EnumDataType.Mine: Result = _BLL_Data.MineList(); break;
                    //加载实时故障
                    case EnumDataType.AQGZ: Results = _BLL_Data.GetRealAQGZ(MineCode, DevType, "1"); break;
                    //获取测点配置信息  
                    case EnumDataType.PointSet: Results = _BLL_Data.GetDeviceInfo(MineCode, DevType, SensorNum); break;
                    //获取煤矿信息 
                    case EnumDataType.MineInfoData: Result = _BLL_Data.MineList(MineCode); break;
                    //获取煤矿传输状态 
                    case EnumDataType.MineState: Result = _BLL_Data.GetBadLog(MineCode, SystemType); break;
                    //获取子系统配置信息 
                    case EnumDataType.ChildSystemSet:
                        Result = _BLL_Data.GetChildSystemSet(MineCode); break;
                    //获取实时报警信息 
                    case EnumDataType.AQBJ: Results = _BLL_Data.GetRealAQBJ(MineCode, TypeName,"1"); break;
                    //历史开关量统计查询
                    case EnumDataType.AQMCHis: Results = _BLL_Data.GetHisAQLT(MineCode, DevType, BeginTime, EndTime); break;
                    //历史报警信息 
                    case EnumDataType.AQBJHis: Results = _BLL_Data.GetHisAQBJ(MineCode, TypeName, SensorNum, BeginTime, EndTime, "1"); break;
                    //历史断电信息
                    case EnumDataType.AQDDHis: Results = _BLL_Data.GetHisAQDD(MineCode, TypeName, SensorNum, BeginTime, EndTime,"1"); break;
                    //历史馈电异常信息
                    case EnumDataType.AQKDHis: Results = _BLL_Data.GetHisAQYC(MineCode, DevType, SensorNum, BeginTime, EndTime); break;


                    #region 2015-2-3[修改记录]
                    //模拟量统计数据
                    case EnumDataType.AQMT: Results = _BLL_Data.GetMinutesData(MineCode, TypeName, BeginTime, EndTime); break;
                    //历史故障信息
                    case EnumDataType.AQGZHis: Results = _BLL_Data.GetHisAQGZ(MineCode, TypeName, SensorNum, BeginTime, EndTime,"1"); break;
                    //测点类型下拉
                    case EnumDataType.PointType: Result = _BLL_Data.DeviceType(); break;
                    //实时断电信息
                    case EnumDataType.AQDD: Results = _BLL_Data.GetRealAQDD(MineCode, TypeName,"1"); break;
                    //实时馈电信息
                    case EnumDataType.AQKD: Results = _BLL_Data.GetRealAQYC(MineCode, TypeName); break;
                    //历史曲线
                    case EnumDataType.HistLine:
                        Results = _BLL_Data.GetMnlMinute_Curve(MineCode, DevType, SensorNum, BeginTime, EndTime); break;
                    #endregion

                    //模拟量统计数据
                    case EnumDataType.AQMNL_1M:
                    case EnumDataType.AQMNL_3M:
                    case EnumDataType.AQMNL_5M:
                        Results = _BLL_Data.GetData_AQMNL(MineCode, TypeName, SensorNum, BeginTime, EndTime, VIEW,"1"); break;
                    case EnumDataType.AQMNL_1D:
                        Results = _BLL_Data.GetData_AQMNL(MineCode, TypeName, SensorNum, DateTime.Parse(BeginTime.ToString("yyyy-MM-dd 00:00:00")), DateTime.Parse(BeginTime.AddDays(1).ToString("yyyy-MM-dd 00:00:00")), VIEW,"1"); break;
                    case EnumDataType.AQMNL_30D:
                        Results = _BLL_Data.GetData_AQMNL(MineCode, TypeName, SensorNum, DateTime.Parse(BeginTime.ToString("yyyy-MM-01 00:00:00")), DateTime.Parse(BeginTime.AddMonths(1).ToString("yyyy-MM-01 00:00:00")), VIEW,"1"); break;

                    //开关量统计数据——郁森
                    case EnumDataType.AQKGL_Day: Results = _BLL_Data.GetAQKGLData_Day(MineCode, TypeName, BeginTime, BeginTime.AddDays(1)); break;
                    case EnumDataType.AQKGL_Week: Results = _BLL_Data.GetAQKGLData_Week(MineCode, TypeName, BeginTime, BeginTime.AddDays(7)); break;

                    #endregion

                    #region [人员管理数据查询]
                    #region 基本信息
                    case EnumDataType.RatedNumber: Result = _BLL_Data.GetRYRatedNumber(MineCode); break;
                    //获取分站信息
                    case EnumDataType.RYStation: Results = _BLL_Data.GetRYFZ(MineCode, SensorNum); break;
                    //获取人员区域信息
                    case EnumDataType.RYAreaInfo: Results = _BLL_Data.GetRYQY(MineCode, SensorNum, DevType); break;
                    //获取人员信息
                    case EnumDataType.RYXX: Results = _BLL_Data.GetRYXX(MineCode, SensorNum, DevType); break;
                    //路线预设
                    case EnumDataType.RYPathInfo: Results = _BLL_Data.GetPathInfo(MineCode, SensorNum, DevType); break;
                    case EnumDataType.Duty:
                        Result = _BLL_Data.GetRYDuty(MineCode);
                        break;
                    case EnumDataType.Department:
                        Result = _BLL_Data.GetRYDepartment(MineCode);
                        break;
                    #endregion
                    #region 路线预设
                    case EnumDataType.PreRoute: Results = _BLL_Data.GetRYLXYS(MineCode, Position, Name); break;
                    #endregion
                    #region 实时数据
                    case EnumDataType.RealInPeople: Results = _BLL_Data.GetRealDataForRY(MineCode, SensorNum, DevType); break;
                    //实时通信状态
                    case EnumDataType.RealTXState: Results = _BLL_Data.GetRealTXState(MineCode); break;
                    #endregion
                    #region 实时报警
                    //实时超时报警
                    case EnumDataType.RealCS: Results = _BLL_Data.GetRYCS(MineCode, SensorNum, DevType, Position); break;
                    //实时限制报警
                    case EnumDataType.RealXZ: Results = _BLL_Data.GetRYXZ(MineCode, SensorNum, DevType); break;
                    //实时超员报警
                    case EnumDataType.RealCY: Results = _BLL_Data.GetRYCY(MineCode, SensorNum); break;
                    //实时超员报警
                    case EnumDataType.RealTZYC: Results = _BLL_Data.GetRYTZYC(MineCode, SensorNum, DevType); break;
                    #endregion
                    #region 历史数据
                    //获取历史上下井信息
                    case EnumDataType.HisTrack: Results = _BLL_Data.GetTrack(MineCode, SensorNum, DevType, BeginTime, EndTime); break;
                    //获取历史轨迹信息
                    case EnumDataType.HisTrackInfo: Results = _BLL_Data.GetTrackInfo(MineCode, SensorNum, BeginTime, EndTime); break;
                    //区域分站人员历史信息
                    case EnumDataType.QYFZQuery:
                        //Results = _BLL_Data.GetAreaStationPerson(MineCode, Name, BeginTime, EndTime); 
                        break;
                    //历史超时信息
                    case EnumDataType.HisCS: Results = _BLL_Data.GetHisCS(MineCode, SensorNum, DevType, BeginTime, EndTime); break;
                    //历史限制信息
                    case EnumDataType.HisXZ: Results = _BLL_Data.GetHisXZ(MineCode, SensorNum, DevType, BeginTime, EndTime); break;
                    //历史超员限制
                    case EnumDataType.HisCY: Results = _BLL_Data.GetHisCY(MineCode, SensorNum, DevType, BeginTime, EndTime); break;
                    //历史特种异常
                    case EnumDataType.HisTZYC: Results = _BLL_Data.GetHisTZYC(MineCode, SensorNum, DevType, BeginTime, EndTime); break;
                    //历史通信故障查询
                    case EnumDataType.HisTXState: Results = _BLL_Data.GetHisTXState(MineCode, BeginTime, EndTime); break;
                    #endregion
                    #region 报表分析
                    case EnumDataType.Report: Result = _BLL_Report.GetReportData(ReportName, MineCode, DevType, BeginTime, BeginTime.AddDays(1)); break;

                    case EnumDataType.ReportMNLRKD:
                    case EnumDataType.ReportKGLRKD:
                        Result = _BLL_Report.GetReportData_KD(MineCode, DevType, BeginTime, BeginTime.AddDays(1), VIEW);
                        break;
                    case EnumDataType.ReportMNLYKD:
                    case EnumDataType.ReportKGLYKD:
                        Result = _BLL_Report.GetReportData_KD(MineCode, DevType, BeginTime, BeginTime.AddMonths(1), VIEW);
                        break;

                    case EnumDataType.ReportMNLRDD:

                    case EnumDataType.ReportKGLRDD:
                        Result = _BLL_Report.GetReportData_DD(MineCode, DevType, BeginTime, BeginTime.AddDays(1), VIEW);
                        break;
                    case EnumDataType.ReportMNLYDD:
                    case EnumDataType.ReportKGLYDD:
                        Result = _BLL_Report.GetReportData_DD(MineCode, DevType, BeginTime, BeginTime.AddMonths(1), VIEW);
                        break;

                    case EnumDataType.ReportMNLRBJ:
                        Result = _BLL_Report.GetReportData_BJ(MineCode, DevType, BeginTime, BeginTime.AddDays(1), VIEW);
                        break;
                    case EnumDataType.ReportMNLYBJ:
                        Result = _BLL_Report.GetReportData_BJ(MineCode, DevType, BeginTime, BeginTime.AddMonths(1), VIEW);
                        break;

                    case EnumDataType.ReportSBGZR:
                        Result = _BLL_Report.GetReportData_SBGZ(MineCode, DevType, BeginTime, BeginTime.AddDays(1), VIEW);
                        break;
                    case EnumDataType.ReportSBGZY:
                        Result = _BLL_Report.GetReportData_SBGZ(MineCode, DevType, BeginTime, BeginTime.AddMonths(1), VIEW);
                        break;

                    case EnumDataType.ReportRYCSBB_R:
                        Result = _BLL_Report.GetReportData_RYCSBB(MineCode, BeginTime, EndTime, VIEW);
                        break;
                    case EnumDataType.ReportRYCSBB_Y:
                        Result = _BLL_Report.GetReportData_RYCSBB(MineCode, BeginTime, EndTime, VIEW);
                        break;

                    case EnumDataType.ReportRYCSBB_B:
                        Result = _BLL_Report.GetReportData_RYCSBB_B(MineCode, BeginTime, EndTime);
                        break;

                    case EnumDataType.ReportRYCYBB_R:
                    case EnumDataType.ReportRYCYBB_Y:
                        Result = _BLL_Report.GetReportData_RYCSBB(MineCode, BeginTime, EndTime, VIEW);
                        break;

                    case EnumDataType.ReportRYSXJBB_R:
                    case EnumDataType.ReportRYSXJBB_Y:
                    case EnumDataType.ReportRYGBLDXJBB_R:
                    case EnumDataType.ReportRYGBLDXJBB_Y:
                        Result = _BLL_Report.GetReportData_RYSXJBB(MineCode, BeginTime, EndTime, VIEW);
                        break;
                    case EnumDataType.ReportRYTXYCBB:
                        Result = _BLL_Report.GetReportData_RYTXYCBB(MineCode, BeginTime, EndTime);
                        break;

                    #endregion

                    #endregion

                    #region 报表查询
                    ////获取通讯异常统计报表信息
                    //case EnumDataType.MineStateHisBB:
                    //    string json = _ReportBLL_Data.GetMineBB(MineCode);
                    //    return json;  
                    #endregion

                    #region [ 公告 ]

                    case EnumDataType.NoticeList:     // 公告列表
                        Result = _BLL_Data.NoticeList();
                        return JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }).Replace("shine998", "<br/>");
                    case EnumDataType.NoticeDelete:
                        return _BLL_Data.NoticeDelete(MineCode).ToString();

                    #endregion


                    #region [ 网络硬盘 ]

                    case EnumDataType.DiskTree://目录树
                        return CallTreeGridDataFormat(_BLL_Data.NetDiskTree(MineCode));
                    case EnumDataType.FileList://文件列表
                        IsoDateTimeConverter timeConverter1 = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                        DataTable dt = _BLL_Data.NetDiskFileList(MineCode);
                        return "{\"total\":" + dt.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(dt, Formatting.Indented, timeConverter1) + "}";
                    case EnumDataType.RemoveFiles:// 删除文件
                        return _BLL_Data.RemoveFiles(MineCode).ToString();
                    case EnumDataType.RemoveDisk:// 删除目录
                        return _BLL_Data.RemoveDisk(MineCode).ToString();
                    case EnumDataType.DiskReName:// 新增子目录，重命名
                        return _BLL_Data.DiskReName(MineCode, SensorNum, DropName, ReportName, Position).ToString();
                    case EnumDataType.DiskViewUsers:
                        Result = _BLL_Data.DiskViewUsers();
                        IsoDateTimeConverter timeConverter2 = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                        return JsonConvert.SerializeObject(Result, Formatting.Indented, timeConverter2);
                    case EnumDataType.DiskSaveUsers:
                        return _BLL_Data.DiskSaveUsers(MineCode, SensorNum, DropName).ToString();

                    #endregion

                    // 预警

                    #region [ 预警 ]

                    case EnumDataType.WarnMineName:
                        Result = _BLL_Data.MineList();
                        DataRow dr = Result.NewRow();
                        //dr["MineCode"] = -1;
                        dr["SimpleName"] = "全部";
                        Result.Rows.InsertAt(dr, 0);
                        return "{\"total\":" + Result.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter()) +
                               "}";
                    case EnumDataType.WarnDevType: Result = m_deviceKind == null ? _BLL_Data.DeviceList() : _BLL_Data.DeviceList(m_deviceKind);
                        DataRow dr1 = Result.NewRow();
                        dr1["TypeName"] = "全部";
                        Result.Rows.InsertAt(dr1, 0);

                        return "{\"total\":" + Result.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter()) +
                               "}";
                    case EnumDataType.WarnList:
                        Result = _BLL_Data.WarmList(MineCode, SensorNum, DropName);
                        return "{\"total\":" + Result.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }) + "}";
                    case EnumDataType.WarnSave:
                        return _BLL_Data.WarmSave(MineCode, SensorNum, DropName, ReportName).ToString();

                    case EnumDataType.WarnToHis:
                        return _BLL_Data.WarmToHis(MineCode).ToString();

                    case EnumDataType.WarnAlarmHis:
                        Result = _BLL_Data.WarnAlarmHis(MineCode, SensorNum, DropName, ReportName, Position);
                        return "{\"total\":" + Result.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }) + "}";

                    case EnumDataType.WarnAlarmTotal:
                        Result = _BLL_Data.WarnAlarmTotal(MineCode, SensorNum, ReportName, Position);
                        return "{\"total\":" + Result.Rows.Count.ToString() + ",\"rows\":" +
                               JsonConvert.SerializeObject(Result, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }) + "}";

                    #endregion

                    #region 【矿压】
                    case EnumDataType.RealData_KY:
                        Results = _BLL_Data.GetRealDataForAQ(MineCode, DevType, SensorNum, "", "5"); 
                        break;
                    //加载实时故障
                   case EnumDataType.AQGZ_KY:
                        Results = _BLL_Data.GetRealAQGZ(MineCode, DevType, "5");
                        break;
                   case EnumDataType.AQGZHis_KY: Results = _BLL_Data.GetHisAQGZ(MineCode, TypeName, SensorNum, BeginTime, EndTime, "5"); 
                        break;
                   case EnumDataType.AQBJ_KY: Results = _BLL_Data.GetRealAQBJ(MineCode, TypeName, "5"); 
                        break;
                   case EnumDataType.AQMNL_1M_KY:
                        Results = _BLL_Data.GetData_AQMNL(MineCode, TypeName, SensorNum, BeginTime, EndTime, VIEW, "5"); 
                        break;
                   case EnumDataType.AQBJHis_KY: Results = _BLL_Data.GetHisAQBJ(MineCode, TypeName, SensorNum, BeginTime, EndTime, "5"); 
                        break;

                    
                    #endregion


                   #region 【火灾束管】
                   case EnumDataType.RealData_HG:
                        Results = _BLL_Data.GetRealDataForAQ(MineCode, DevType, SensorNum, "", "7");
                        break;
                   //加载实时故障
                   case EnumDataType.AQGZ_HG:
                        Results = _BLL_Data.GetRealAQGZ(MineCode, DevType, "7");
                        break;
                   case EnumDataType.AQGZHis_HG: Results = _BLL_Data.GetHisAQGZ(MineCode, TypeName, SensorNum, BeginTime, EndTime, "7");
                        break;
                   case EnumDataType.AQBJ_HG: Results = _BLL_Data.GetRealAQBJ(MineCode, TypeName, "7");
                        break;
                   case EnumDataType.AQMNL_1M_HG:
                        Results = _BLL_Data.GetData_AQMNL(MineCode, TypeName, SensorNum, BeginTime, EndTime, VIEW, "7");
                        break;
                   case EnumDataType.AQBJHis_HG: Results = _BLL_Data.GetHisAQBJ(MineCode, TypeName, SensorNum, BeginTime, EndTime, "7");
                        break;


                   #endregion
                    //返回空值 
                    default: Result = new DataTable(); break;
                }
                #endregion
                
                string json = "";
                int TotalRows = 0;
                //在对DATATABLE进行序列化的时候，规范日期格式
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                if(Result!=null)
                {
                    TotalRows = Result.Rows.Count;
                    json = JsonConvert.SerializeObject(Result, Formatting.Indented, timeConverter);
                }
                else
                {
                    if(Results!=null)
                    {
                        TotalRows = int.Parse(Results[0].Rows[0][0].ToString());
                        json = JsonConvert.SerializeObject(Results[1], Formatting.Indented, timeConverter);
                    }
                }
             
                //if (Result.Rows.Count == 0 && Results != null && Results.Count > 1)
                //{
                //    Result = Results[1];
                //    TotalRows = int.Parse(Results[0].Rows[0][0].ToString());
                //}
              
                return "{\"totalrows\": " + TotalRows + ",\"data\":" + json + "}";
                //return JsonConvert.SerializeObject(Result);
            }
        }
        #region test
        public string treetest()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Parent";
            dt.Columns.Add("id");
            dt.Columns.Add("text");
            dt.Columns.Add("state");



            for (int i = 0; i < 10; i++)
            {
                DataRow dr0 = dt.NewRow();
                dr0[0] = "1";
                dr0[1] = "a";
                dr0[2] = "closed";
                dt.Rows.Add(dr0);
            }

            //DataRow dr0 = dt.NewRow();
            //dr0[0] = "1";
            //dr0[1] = "a";
            //dt.Rows.Add(dr0);
            //DataRow dr1 = dt.NewRow();
            //dr1[0] = "2";
            //dr1[1] = "b";
            //dt.Rows.Add(dr1);
            //StringBuilder  json = new StringBuilder();
            //json.Append("[");
            //foreach (DataRow dr in dt.Rows)
            //{

            //    json.Append("{\"id\":" + dr["id"].ToString());
            //    json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
            //    json.Append(",\"state\":\"closed\"");

            //    DataTable dtChildren = new DataTable();
            //    dtChildren.Columns.Add("id");
            //    dtChildren.Columns.Add("name");
            //    DataRow dr2=dtChildren.NewRow();
            //    dr2[0] = 3;
            //    dr2[1] = "Son";
            //    dtChildren.Rows.Add(dr2);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        json.Append(",\"children\":[");
            //        json.Append(DataTable2Json(dtChildren, Convert.ToInt32(dr["id"])));
            //        json.Append("]");
            //    }
            //    json.Append("},");

            //}
            //if (dt.Rows.Count > 0)
            //{
            //    json.Remove(json.Length - 1, 1);
            //}
            //json.Append("]");
            //return json.ToString();  

            return JsonConvert.SerializeObject(dt);
        }

        public string DataTable2Json(DataTable dt, int a)
        {
            StringBuilder json = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                json.Append("{\"id\":" + dr["id"].ToString());
                json.Append(",\"text\":\"" + dr["name"].ToString() + "\"");
                json.Append("},");
            }
            if (dt.Rows.Count > 0)
            {
                json.Remove(json.Length - 1, 1);
            }
            return json.ToString();
        }
        #endregion

        #region MineTest
        public DataTable minetest()
        {
            DataTable Result = new DataTable();
            Result.Columns.Add("MineCode");
            Result.Columns.Add("MineName");
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = Result.NewRow();
                dr[0] = (10000 + i).ToString();
                dr[1] = "测试煤矿" + i.ToString();
                Result.Rows.Add(dr);
            }
            return Result;
        }
        #endregion

        #region [ 树 ]

        private string CallTreeGridDataFormat(DataTable dt)
        {
            string json = string.Format("[{0}]", TreeGridDataFormat(dt, dt.Select("PDiskID =0")));
            return json;
        }

        private string TreeGridDataFormat(DataTable dt, DataRow[] drs)
        {
            string toJosn = "";
            for (int i = 0; i < drs.Length; i++)
            {
                if (i != 0) toJosn += ",";
                toJosn += "{";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != 0) toJosn += ",";

                    string jsonFormat = "\"{0}\":\"{1}\"";
                    if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
                    {
                        jsonFormat = "\"{0}\":{1}";
                    }
                    toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drs[i][j].ToString());
                }

                // 调用下一层
                #region [class2]

                string parentMenuID = drs[i]["Disk_ID"].ToString();
                string josnChird = TreeGridDataFormat(dt, dt.Select("PDiskID='" + parentMenuID + "'"));
                toJosn += string.Format(" ,\"children\": [{0}]", josnChird);

                #endregion

                toJosn += "}";
            }


            return toJosn;
        }

        #endregion

        #region 枚举对象
        public enum EnumDataType
        {
            #region 公告管理
            NoticeList,     // 公告列表
            NoticeDelete,   // 公告删除
            #endregion

            #region 预警管理
            WarnMineName,   // 预警煤矿名称
            WarnDevType,    // 预警设备名称
            WarnList,       // 预警列表
            WarnSave,       // 保存预警信息
            WarnToHis,      // 预警放入历史
            WarnAlarmHis,   // 历史预警
            WarnAlarmTotal, // 预警统计
            #endregion

            #region 网络硬盘
            DiskSaveUsers,   //保存权限
            DiskViewUsers,  //查看权限
            DiskReName, //重命名
            RemoveDisk, //删除当前目录
            RemoveFiles,    //删除文件
            DiskTree,   //网络硬盘树
            FileList,   //文件列表
            #endregion

            #region[人员]
            RYStation,
            AQDD,
            RYAreaInfo,
            RYXX,
            PreRoute,
            RealInPeople,
            RealCS,
            RealCY,
            RealXZ,
            RealTZYC,
            RYPathInfo,
            RealTXState,
            HisTXState,
            HisTrack,
            HisTrackInfo,
            QYFZQuery,
            HisCS,
            HisXZ,
            HisCY,
            HisTZYC,
            Report,
            RatedNumber,
            Duty,
            Department,
            #endregion
            #region 安全监控
            Filter,
            MineName,
            Sensor,
            DevType,
            RealData,
            Mine,
            Aqss,
            AQGZ,
            PointSet,
            MineInfoData,
            MineState,
            MineStateHis,
            ChildSystemSet,
            AQKD,
            AQBJ,
            AQMT,
            AQMCHis,
            AQBJHis,
            AQDDHis,
            AQKDHis,
            AQGZHis,

            AQKGL_Day,//每天开关量
            AQKGL_Week,//七天开关量
            AQMNL_1M,//模拟量每分钟数据显示
            AQMNL_3M,//模拟量3分钟数据显示
            AQMNL_5M,//模拟量5分钟数据显示
            AQMNL_1D,//模拟量每天数据显示
            AQMNL_30D,//模拟量每月数据显示

            ReportMNLYKD,//模拟量月馈电异常
            ReportMNLRKD,//模拟量日馈电异常

            ReportMNLRDD,//模拟量日断电异常
            ReportMNLYDD,//模拟量月断电异常

            ReportMNLRBJ,//模拟量日报警异常
            ReportMNLYBJ,//模拟量日报警异常

            ReportKGLYKD,//开关量月馈电异常
            ReportKGLRKD,//开关量日馈电异常

            ReportKGLRDD,//开关量日断电异常
            ReportKGLYDD,//开关量月断电异常

            ReportSBGZR,//设备故障日报表
            ReportSBGZY,//设备故障月报表

            ReportRYCSBB_R,//人员管理 日超时报表
            ReportRYCSBB_Y,//人员管理 月超时报表
            ReportRYCSBB_B,//人员管理 班超时报表

            ReportRYCYBB_R,//人员管理 日超员报表
            ReportRYCYBB_Y,//人员管理 月超员报表

            ReportRYTXYCBB,//人员管理 通讯异常报表
            ReportRYSXJBB_R,//人员管理 日上下井报表
            ReportRYSXJBB_Y,//人员管理 月上下井报表
            ReportRYSXJBB_B,//人员管理 班上下井报表
            ReportRYGBLDXJBB_R,//人员管理 跟班领导下井报表
            ReportRYGBLDXJBB_Y,//人员管理 跟班领导下井报表
            ReportRYGBLDXJBB_B,//人员管理 跟班领导下井报表

            #endregion
            #region 安全监控(报表)
            MineStateHisBB,
            #endregion

            HistLine,
            PointType,
            TreeTest,
            MineTest,
            ShowAQJKRTData,
            #region 【矿压】
            RealData_KY,
            AQGZ_KY,
            AQBJ_KY,
            AQMNL_1M_KY,
            AQGZHis_KY,
           AQBJHis_KY,
            #endregion
            #region 【火灾束管】
            RealData_HG,
            AQGZ_HG,
            AQBJ_HG,
            AQMNL_1M_HG,
            AQGZHis_HG,
           AQBJHis_HG
            #endregion
        }
        #endregion
    }
}