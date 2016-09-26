using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace InternetDataMine.Models.DataService
{
    public class ReportDataDAL
    {
        string conn = "";
        public ReportDataDAL()
        {
            ReadConfig read = new ReadConfig();
            conn = read.GetSQLConnection();
        }


        /// <summary>
        /// 获取所有非固定格式报表
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable GetReportList()
        {
            string sql = "select * from Report_config where Dispname is not null and SystemType>0";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取所有报表
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable GetAllReportList()
        {
            string sql = "select * from Report_config";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取所有固定格式报表
        /// </summary>
        /// <returns></returns>
        public DataTable GetFixedReport()
        {
            string sql = "select * from Report_config where SystemType=0";
            return SQLDataServer.ToDataTable(sql, conn);
        }
        /// <summary>
        /// 通讯异常统计
        /// </summary>
        //public class Pxmplan
        //{
        //    private int _r;

        //    public int R
        //    {
        //        get { return _r; }
        //        set { _r = value; }
        //    }
        //    private string _id;
        //    public string Id
        //    {
        //        get { return _id; }
        //        set { _id = value; }
        //    }
        //    private string city;

        //    public string City
        //    {
        //        get { return city; }
        //        set { city = value; }
        //    }
        //    private string mineNum;//	varchar(50)	Checked煤矿编码

        //    public string MineNum
        //    {
        //        get { return mineNum; }
        //        set { mineNum = value; }
        //    }
        //    private string mineName;//		varchar(50)	Checked煤矿名称
        //    /// <summary>
        //    /// 煤矿名称
        //    /// </summary>
        //    public string MineName
        //    {
        //        get { return mineName; }
        //        set { mineName = value; }
        //    }
        //    private string rq;//填写日期
        //    /// <summary>
        //    /// 填写日期
        //    /// </summary>
        //    public string Rq
        //    {
        //        get { return rq; }
        //        set { rq = value; }
        //    }
        //    private string contents;//计划内容
        //    /// <summary>
        //    /// 计划内容
        //    /// </summary>
        //    public string Contents
        //    {
        //        get { return contents; }
        //        set { contents = value; }
        //    }
        //}
        ///// <summary>
        ///// 通讯异常查询
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public string QueryInfo(string sql)
        //{
        //    sql = "select * from Report_config where "+sql;
        //    string jsonStr = "";
        //    DataTable dt = SQLDataServer.ToDataTable(sql, conn);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        //此数据只有一条（取第一条）
        //        Pxmplan m = new Pxmplan();
        //        m.Contents = dt.Rows[0]["Remark"].ToString();
        //        m.MineName = dt.Rows[0]["ReportName"].ToString();
        //        m.MineNum = dt.Rows[0]["DispName"].ToString();
        //        m.Rq = dt.Rows[0]["CreateTime"].ToString();
        //        jsonStr = JsonConvert.SerializeObject(m);
        //    }
        //    return jsonStr;
        //}
        /// <summary>
        /// 获取指定报表类型的报表信息
        /// </summary>
        /// <param name="reportname">报表类型</param>
        /// <returns>数据集</returns>
        public DataTable GetReportList(string reportname)
        {
            //string sql = "select * from Report_config where Dispname='" + reportname + "'";
            string sql = "select * from Report_config where ReportName='" + reportname + "'";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取指定报表类型的XML
        /// </summary>
        /// <param name="reportname">报表类型</param>
        /// <returns></returns>
        public DataTable GetReportXML(string reportname)
        {
            string sql = "select ReportXML from Report_config where ReportName='" + reportname + "'";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取指定报表的数据
        /// </summary>
        /// <param name="reportname">报表名称</param>
        /// <returns>数据集</returns>
        public DataTable GetReportData(string reportname)
        {
            string sql = "select ROW_NUMBER()over(order by getdate()) as TmpID,* from " + reportname;
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取指定报表和特定条件的数据
        /// </summary>
        /// <param name="reportname">报表名称</param>
        /// <param name="where">条件</param>
        /// <returns>数据集</returns>
        public DataTable GetReportData(string reportname, string where)
        {
            string sql = "select ROW_NUMBER()over(order by getdate()) as TmpID,* from [ShineView_His].[dbo]." + reportname + " where " + where;
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取指定表的列信息
        /// </summary>
        /// <param name="tabname">表名</param>
        /// <returns>列名，数据类型</returns>
        public DataTable GetTableColumns(string tabname)
        {
            //SubmitTime
            string sql = "select COLUMN_NAME,(case when data_type in ('char','nchar','varchar','nvarchar','text') then 'string' else DATA_TYPE end) as DATA_TYPE from information_schema.columns where TABLE_NAME='" + tabname + "' and COLUMN_NAME not in ('SubmitTime')";
            //string sql = "select * from " + tabname + " where 1=2 ";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        public DataTable GetColumns(string tabname)
        {
            string sql = "select COLUMN_NAME as name,(case when data_type in ('char','nchar','varchar','nvarchar','text') then 'string' else DATA_TYPE end) as type from information_schema.columns where TABLE_NAME='" + tabname + "' and COLUMN_NAME<>'SubmitTime'";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 获取指定表指定列信息
        /// </summary>
        /// <param name="tabname">表名</param>
        /// <param name="columns">列</param>
        /// <returns></returns>
        public DataTable GetTableColumns(string tabname, string columns)
        {
            string sql = "select " + columns + " from " + tabname + " where 1=2 ";
            return SQLDataServer.ToDataTable(sql, conn);
        }


        /// <summary>
        /// 获取指定的报表数据
        /// </summary>
        /// <param name="reportname">报表名称</param>
        /// <param name="begintime">参数</param>
        /// <param name="endtime">参数</param>
        /// <returns></returns>
        public bool SetReportData(string reportname, DateTime begintime, DateTime endtime)
        {
            try
            {
                string procname = "UP_" + reportname;
                
                SqlParameter[] sqlparms ={
                                new SqlParameter("@begintime ",begintime),
                                new SqlParameter("@endtime",endtime)
                            };
                return SQLDataServer.OperationProcedure(procname,sqlparms,conn);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取报表类型的表名
        /// </summary>
        /// <returns></returns>
        public DataTable GetReportTables()
        {
            string sql = "SELECT name FROM sys.objects WHERE type in (N'U') and name <>'Report_Config' and name like 'Report_%'";
            return SQLDataServer.ToDataTable(sql, conn);
        }

        /// <summary>
        /// 增加报表
        /// </summary>
        /// <param name="reportname">报表名称</param>
        /// <param name="tablename">报表对应的表名</param>
        /// <param name="systemtype">报表所属系统类型</param>
        /// <param name="remark">报表备注</param>
        /// <returns></returns>
        public bool InsertReport(string reportname,string tablename,int systemtype,string remark)
        {
            try
            {
                string sql = "insert into Report_Config(ReportName,DispName,SystemType,Remark) values('"+reportname+"','"+tablename+"',"+systemtype+",'"+remark+"')";
                return SQLDataServer.OperationSQL(sql,conn);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定编号的报表
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
        public bool DeleteReport(int ID)
        {
            try
            {
                string sql = "delete Report_Config where ID="+ID;
                return SQLDataServer.OperationSQL(sql,conn);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定的表
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public bool DeleteReportTable(string tablename)
        {
            try
            {
                string sql = "drop table " + tablename;
                return SQLDataServer.OperationSQL(sql,conn);
            }
            catch
            {
                return false;
            }
        }
    }
}
