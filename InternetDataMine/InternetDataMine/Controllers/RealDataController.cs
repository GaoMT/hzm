using System;
using System.Web.Mvc;
using InternetDataMine.Models;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json;

namespace InternetDataMine.Controllers
{
    public class RealDataController : Controller
    {
        /// <summary>
        /// 动态加载菜单
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="mineCode"></param>
        /// <returns></returns>
        public ActionResult Index(string userName,string mineCode)
        {
            return View();
        }

        public ActionResult MainPage(string userName, string mineCode)
        {
            return View();
        }

        public void GetAlarmPageData(string mineCode)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");

            string queryString = string.Empty;

            if (!string.IsNullOrEmpty(mineCode) && mineCode.ToLower() != "null")
            {
                queryString = string.Format(" Where MineCode Like '%{0}%'", mineCode);
            }
            else
            {
                queryString = "";
            }
            string sql = string.Format(@"select * from (Select mc.MineCode,mc.SimpleName,case sc.TypeCode when 1 then '安全监控系统' when 2 then '人员管理系统' when 3 then '瓦斯抽放系统' else '安全监控/瓦斯抽放系统' end as systemName,
case sc.StateCode when 1 then '通讯中断' when 2 then '传输异常'end as communicationState 
  from SystemConfig sc left join MineConfig mc on sc.MineCode=mc.ID where sc.StateCode<>0 ) as A {0}

/*安全监控报警*/
select * from (select mc.MineCode,mc.SimpleName MineName,dt.TypeName deviceName,aqss.Place,
case aqss.ValueState When 1 Then '报警' When 4 Then '断电报警' When 8 Then '故障报警' When 16 Then '馈电异常' Else '工作异常' end as AlarmType,
ShowValue,aqss.PoliceMaxValue,aqss.PoliceMaxDatetime,aqss.PowerMax,aqss.PowerMaxDatetime,aqss.PowerDateTime,aqss.PoliceDateTime,[dbo].[FunConvertTime](datediff(second, PoliceDatetime,getdate())) as PoliceContinuoustime,
[dbo].[FunConvertTime](datediff(second, PowerDateTime,getdate())) as PowerContinuoustime,[dbo].[FunConvertTime](datediff(second, PowerDateTime,getdate())) as AbnormalContinuoustime,
aqss.AbnormalDateTime
from (select * from aqss where ValueState<>0) as aqss  
left join DeviceType dt on AQSS.Type=dt.TypeCode 
left join MineConfig mc on aqss.MineCode=mc.MineCode ) as B {0}  Order by AlarmType   

/*人员管理超时报警*/
select * from (select mc.MineCode,mc.SimpleName,rycs.JobCardCode,ryxx.Name,RYXX.Position Post,RYXX.Department Dept,rycs.InTime InMineTime,rycs.StartAlTime,
dbo.FunConvertTime(DateDiff(second,RYCS.StartAlTime,GETDATE())) OverTimeLength 
from RYCS 
left join MineConfig mc on rycs.MineCode=mc.MineCode 
left join RYXX on RYCS.JobCardCode=RYXX.JobCardCode and RYCS.MineCode=RYXX.MineCode ) as C {0}

/*人员管理超员报警*/
select * from (select mc.MineCode,mc.SimpleName,xx.Name,xx.Position Post,xx.Department Dept,cy.Number LimiteNumber,cy.[Sum] RealNumber
,cy.[Type] AlarmType,cy.InTime InMineTime,cy.StartAlTime,dbo.FunConvertTime(DateDiff(second,cy.StartAlTime,GETDATE())) OverTimeLength  
from RYCYXZ cy 
left join RYXX xx on cy.MineCode=xx.MineCode and cy.JobCardCode=xx.JobCardCode 
left join MineConfig mc on cy.MineCode=xx.MineCode ) as D {0}

/*人员管理特种工作异常报警*/
select * from (select mc.MineCode,mc.SimpleName,xx.Name,xx.Position Post,xx.Department Dept,tz.InTime InMineTime,tz.OrigAddress PlanReachPlace,tz.OrigTime PlanReachTime,tz.RealTime RealReachTime,tz.[State] NowState 
from RYTZYC tz 
left join RYXX xx on tz.MineCode=xx.MineCode and tz.JobCardCode=xx.JobCardCode 
left join MineConfig mc on tz.MineCode=mc.MineCode ) as E {0}
", queryString);

            var dal = new DataDAL();
            var dt = dal.ReturnDs(sql);
            var data = JsonConvert.SerializeObject(dt);

            Response.Write(data);
            Response.End();
        }

        /// <summary>
        /// 加载所有煤矿报警统计信息
        /// </summary>
        /// <param name="mineCode">煤矿编号</param>
        public void GetAllAlarmCount(string mineCode)
        {
            try
            {
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.AddHeader("Pragma", "No-Cache");

                string queryString = string.Empty;

                if (!string.IsNullOrEmpty(mineCode) && mineCode.ToLower() != "null")
                {
                    queryString = string.Format(" Where MineCode Like '%{0}%'", mineCode);
                }
                else
                {
                    queryString = "";
                }

                string sql = string.Format(@"With U As(
	Select * From (
		-- 人员管理报警信息
		Select '超时报警' AlarmType, MineCode,'人员管理系统' AlarmGroup,'人员管理系统' SensorName From RYCS 
		Union All 
		Select '特种人员工作异常报警' AlarmType, MineCode,'人员管理系统' AlarmGroup,'人员管理系统' SensorName From RYTZYC
		Union All 
		Select '超员报警' AlarmType, MineCode,'人员管理系统' AlarmGroup,'人员管理系统' SensorName From RYCYXZ 
		Union All
		-- 安全监控报警信息
		Select case A.ValueState When 1 Then '报警' When 4 Then '断电报警' When 8 Then '故障报警' When 16 Then '馈电异常报警' Else '工作异常报警' End AlarmType,
			A.MineCode, '安全监控系统' as AlarmGroup, dt.TypeName SensorName From (Select MineCode,ValueState,[type] from Aqss where ValueState!=0) As A 
			Left Join [dbo].[DeviceType] dt on A.[type]=dt.TypeCode Left Join MineConfig mc on A.MineCode=mc.MineCode
	) As U 
)
Select COUNT(1) As AlarmCount From
(
	--通讯状态报警信息
	Select SimpleName, Case [StateCode] When 1 Then '通讯中断报警' When 2 Then '传输异常报警' End AlarmType, M.MineCode, Name AlarmGroup, '' SensorName
		From (
       Select * From SystemConfig S Where StateCode <> 0) S
		Left Join MineConfig M On S.MineCode = M.ID 
	Union All
	-- 人员管理及安全监控报警信息
	Select SimpleName, AlarmType, M.MineCode, U.AlarmGroup, U.SensorName From U Left Join MineConfig M On U.MineCode = M.MineCode 
		--Where U.MineCode Not In (Select M.MineCode From SystemConfig S Left Join MineConfig M On S.MineCode = M.ID Where StateCode <> 0)
) As U {0}
", queryString);

                var dal = new DataDAL();
                var dt = dal.ReturnData(sql);
                var data = JsonConvert.SerializeObject(dt);

                Response.Write(data);
                Response.End();
            }
            catch (Exception)
            {
                

                
            }
         
        }

        /// <summary>
        /// 加载所有煤矿报警信息
        /// </summary>
        /// <param name="MineCode">煤矿编号</param>
        public void GetAllAlarmInfo(string MineCode, string AccountID)
        {
           // try
           // {

                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.AddHeader("Pragma", "No-Cache");

                if (MineCode == null)
                {
                    MineCode = "";
                }
                RealDataModel realDataModel = new RealDataModel();
                string AlarmMessage = realDataModel.GetAllAlarmInfo(MineCode, AccountID);
                Response.Write(AlarmMessage);
                Response.End();
           // }
            //catch
           // {
                
           // }
        }

        public void GetMineName()
        {
            RealDataModel realDataModel = new RealDataModel();
            string AlarmMessage = realDataModel.GetMineName();
            Response.Write(AlarmMessage);
            Response.End();
        }

        /// <summary>
        /// 根据帐户 Id 获取报警设置状态
        /// </summary>
        /// <param name="accountId"></param>
        public void GetAlarmSetState(string accountId)
        {
            try
            {
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.AddHeader("Pragma", "No-Cache");

                string sql =
                    string.Format(@"Select AlarmType, T.AlarmGroup, IsNull([Disabled], 1) [Disabled] From dbo.AlarmTypeInfo T
	Left Join 
	(Select * From dbo.AlarmSet Where AccountId = '{0}') S
	On T.AlarmType = S.LinkData", accountId);

                var dal = new DataDAL();
                var dt = dal.ReturnData(sql);
                var data = JsonConvert.SerializeObject(dt);

                Response.Write(data);
                Response.End();
            }
            catch (Exception)
            {

            }
           
        }

        public void SetAlarmSetItem(string accountId, string alarmGroup, string disabled, string linkData)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");
            string sql;

            if (alarmGroup.Equals("99"))
            {
                sql = string.Format(@"Delete From [ShineView_Data].[dbo].[AlarmSet] Where [AccountId] = '{0}' And [AlarmGroup] = {1} And [LinkData] = '{2}' ", accountId, alarmGroup, linkData);
                sql += string.Format(@"INSERT INTO [ShineView_Data].[dbo].[AlarmSet]([AccountId],[AlarmGroup],[Disabled],[LinkData])VALUES('{0}', {1},{2},'{3}') ", accountId, alarmGroup, disabled, linkData);
            }
            else
            {
                sql = string.Format(@"Delete From [ShineView_Data].[dbo].[AlarmSet] Where [AccountId] = '{0}' And [AlarmGroup] = {1} And [LinkData] = '{2}' ", accountId, alarmGroup, linkData);
                //event before is  false ;  event have been is  true;
                if (disabled.Equals("false")) //
                {
                    sql += string.Format(@"INSERT INTO [ShineView_Data].[dbo].[AlarmSet]([AccountId],[AlarmGroup],[Disabled],[LinkData])VALUES('{0}', {1},{2},'{3}') ", accountId, alarmGroup, disabled.Equals("true") ? 1 : 0, linkData);
                }
            }

            var dal = new DataDAL();

            if (dal.ExcuteSql(sql))
            {
                if (!disabled.Equals("true"))
                {
                    Response.Write(string.Format("{0} 更新成功, 状态为报警", linkData));
                }
                else
                {
                    Response.Write(string.Format("{0} 更新成功, 状态为不报警", linkData));
                }
            }
            else
            {
                if (!disabled.Equals("true"))
                {
                    Response.Write(string.Format("{0} 更新失败, 状态为报警", linkData));
                }
                else
                {
                    Response.Write(string.Format("{0} 更新失败, 状态为不报警", linkData));
                }
            }

            Response.End();
        }

        public void ReturnMenus(string userName, string mineCode)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.LoadMenuDynamic(userName, mineCode);
            Response.Write(loginModel.ReturnMenus);
           // string xx = loginModel.GetWeather("http://api.map.baidu.com/telematics/v3/weather?location=兴义&output=json&ak=1Be18hYdbDVirh7pTk6UdEst", "utf-8")
            Response.End();
        }


        public ActionResult RealDataTest()
        {
            RealDataModel rdModel = new RealDataModel();
            return View(rdModel);
        }


        public ActionResult AQGZ()
        {
            RealDataModel rdModel = new RealDataModel();
            return View(rdModel);

        }
    }
}
