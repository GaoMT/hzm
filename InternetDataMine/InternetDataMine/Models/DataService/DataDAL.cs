using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using OSGeo.GDAL;

namespace InternetDataMine.Models.DataService
{
    public class DataDAL
    {

        #region[公共部分]
        string conn;
        ReadConfig read;
        public DataDAL()
        {
            read = new ReadConfig();
            conn = read.GetSQLConnection();
        }

        /// <summary>
        /// 查询煤矿列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSystemConfigList()
        {
            string sql = @"Select C.*,sc.TypeName, M.SimpleName,M.MineCode MyMineCode From SystemConfig C Left Join MineConfig M On C.MineCode = M.ID left join SystemTypesInfo sc on C.TypeCode=sc.TypeCode order by M.MineCode  ";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 查询煤矿列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMineList(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_MineList", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 执行存储过程，放回数据表集合
        /// </summary>
        /// <param name="sqlc">查询参数列表</param>
        /// <returns>数据表集合</returns>
        public DataTableCollection ReturnDTS_ExcutePro(SqlParameter[] sqlc,string DB)
        {
            if (DB == "His") conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("AutoPage", sqlc, conn).Tables;

        }


        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetDeviceList(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_DeviceList",sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取历史传输异常信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRtBadLog(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RtBadLog", sqlparms, conn);
            return dt;
        }

        #endregion

        #region[安全监控部分]
        /// <summary>
        /// 获取安监系统实时数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRealDataForAQ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealDataForAQ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 实时报警
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetRealAQBJ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealAQBJ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 实时断电
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetRealAQDD(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealAQDD", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 实时馈电异常
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetRealAQYC(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealAQYC", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 实时故障状态,0:正常;1:报警;2:复电;4:断电;8:故障;16:馈电异常;32:工作异常
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetRealAQGZ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealAQGZ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取设备配置信息
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="devtype">设备类型</param>
        /// <returns></returns>
        public DataTable GetDeviceInfo(string where, string devtype)
        {
            DataTable dt = new DataTable();
            if (devtype != ""&&devtype!=null)
            {
                where = "d.type='"+devtype+"' and " + where;
            }                
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            if (devtype == "D")
            {
                dt = SQLDataServer.ProcedureDataTable("Call_DeviceInfo_D", sqlparms, conn);
            }
            else
            {
                dt = SQLDataServer.ProcedureDataTable("Call_DeviceInfo", sqlparms, conn);
            }
            return dt;
        }

        /// <summary>
        /// 模拟量分钟数据显示
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetMinutesData(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_MinutesData", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 开关量变动查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetHisAQKD(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQKD", sqlparms, conn);
            return dt;
        }
        /// <summary>
        /// 历史报警查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisAQBJ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQBJ", sqlparms, conn);
            return dt;
        }
        /// <summary>
        /// 历史断电查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisAQDD(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQDD", sqlparms, conn);
            return dt;
        }
        /// <summary>
        /// 历史故障查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisAQGZ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQGZ", sqlparms, conn);
            return dt;
        }
        /// <summary>
        /// 历史馈电异常查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisAQYC(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQYC", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 累计量统计查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisAQLT(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisAQLT", sqlparms, conn);
            return dt;
        }

        public DataTableCollection ReturnDTS_ExcutePro_KGLDay(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetKGLDayCount_AutoPage", sqlc, conn).Tables;
        }

        public DataTableCollection ReturnDTS_ExcutePro_KGLWeek(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetKGLWeekCount_AutoPage", sqlc, conn).Tables;
        }

        public DataTableCollection ReturnDTS_ExcutePro_MNL(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetMNL_AutoPage", sqlc, conn).Tables;
        }

        public DataTableCollection ReturnDTS_ExcutePro_MNLMinute(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetMNLMinute_AutoPage", sqlc, conn).Tables;
        }

        public DataTableCollection ReturnDTS_ExcutePro_MNLDay(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetMNLDay_AutoPage", sqlc, conn).Tables;
        }

        #endregion

        #region[人员管理部分]
        /// <summary>
        /// 获取人员管理系统实时数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRealDataForRY(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RealDataForRY", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 区域信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYQY(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYQY", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 分站信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYFZ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYFZ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 人员信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYXX(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYXX", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取指定条件下的工种或职务信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYPositions(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYPositions", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取指定条件下的人员姓名信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYNames(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYNames", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 人员路线预设信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYLXYS(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYLXYS", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取人员超时信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYCS(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYCS", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取历史超时信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisRYCS(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisRYCS", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取煤矿超员/限制信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYCYXZ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYCYXZ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取历史超员/限制信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisRYCYXZ(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisRYCYXZ", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取特种异常信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetRYTZYC(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_RYTZYC", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 获取历史特种异常信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetHisRYTZYC(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_HisRYTZYC", sqlparms, conn);
            return dt;
        }

        /// <summary>
        /// 上下井信息
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataTable GetTrack(string where)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqlparms ={
                                new SqlParameter("@where",where)
                            };
            dt = SQLDataServer.ProcedureDataTable("Call_Track", sqlparms, conn);
            return dt;
        }

        #endregion

        public DataTable ReturnData(string sql)
        {
            return SQLDataServer.ToDataTable(sql, conn);
        }

        public DataSet ReturnDs(string sql)
        {
            return SQLDataServer.ToDataSet(sql, conn);
        }

        public bool ExcuteSql(string sql)
        {
            return SQLDataServer.OperationSQL(sql,conn);
        }

        public int ExecSql(string sql)
        {
            return SQLDataServer.ExecSql(sql, conn);
        }

        /// <summary>
        /// 向数据库中存文件
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="FileContent"></param>
        /// <returns></returns>
        public bool InsertFileSql(string sql,byte[] FileContent)
        {
            return SQLDataServer.InsertFile(sql, FileContent, conn);
        }

        public DataTableCollection GetMnlMinute_Curve(SqlParameter[] sqlc)
        {
            conn = read.GetSQLConnectionHis();
            return SQLDataServer.ProcedureDataSet("GetMnlMinute_Curve", sqlc, conn).Tables;
        }

    }
}
