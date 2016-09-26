using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json.Converters;

namespace InternetDataMine.Models
{
    public class TransCompanyModel
    {
        DataBLL MyBLL = new DataBLL();
        string type = string.Empty;
        string Info = string.Empty;
        string ID = string.Empty;
        string MineCode = string.Empty;
        
        // 上传文件待赋值
        public byte[] FileContent { get; set; }

        /// <summary>
        /// 加载煤矿基本信息
        /// </summary>
        /// <returns></returns>
        public string LoadMineBaseInfo()
        {
            return "";
        }

        public TransCompanyModel(string type, string Info, string ID,string MineCode)
        {
            this.type = type;
            this.Info = Info;
            this.ID = ID;
            this.MineCode = MineCode;
        }

        public DataTable DownloadFile()
        {
            DataTable dt = new DataTable();
            switch(type)
            {
                case "download_EItable_QYTZ":
                   dt = MyBLL.DownloadQYTZ(ID);
                    break;
                case "download_EItable_XGZL":
                    dt = MyBLL.DownloadXGZL(ID);
                    break;
                case "download_EItable_QYZZ":
                    dt = MyBLL.DownloadQYZZ(ID);
                    break;
            }
            return dt;
        }

        public string GetJson
        {
            get
            {
                string Result = string.Empty;
                DataTable ComboboxTable = new DataTable();
                DataTable ResultTable = new DataTable();
                switch (type)
                {
                    #region combobox
                    //附件类型
                    case "attachmenttype":
                        ComboboxTable = MyBLL.Queryattachmenttype();
                      return  Result = TableToCombojson(ComboboxTable);
                    //经济类型
                    case "col_type":
                        ComboboxTable = MyBLL.Querycol_type();
                      return  Result = TableToCombojson(ComboboxTable);
                    //质量标准化等级
                    case "standard_grade":
                        ComboboxTable = MyBLL.Querystandard_grade();
                      return  Result = TableToCombojson(ComboboxTable);
                    //矿井状态
                    case "col_status":
                        ComboboxTable = MyBLL.Querycol_status();
                       return Result = TableToCombojson(ComboboxTable);
                        
                    //矿井类型
                    case "mine_type":
                        ComboboxTable = MyBLL.Querymine_type();
                       return Result = TableToCombojson(ComboboxTable);
                        
                    //主要煤种
                    case "coal_type":
                        ComboboxTable = MyBLL.Querycoal_type();
                       return Result = TableToCombojson(ComboboxTable);
                       
                    //开拓方式
                    case "tunnel_mode":
                        ComboboxTable = MyBLL.Querytunnel_mode();
                       return Result = TableToCombojson(ComboboxTable);
                        
                    //采煤工艺
                    case "coal_mine":
                        ComboboxTable = MyBLL.Querycoal_mine();
                       return Result = TableToCombojson(ComboboxTable);
                        
                    //运输方式
                    case "mode_of_ship":
                        ComboboxTable = MyBLL.Querymode_of_ship();
                       return Result = TableToCombojson(ComboboxTable);
                        
                    //矿井供电方式
                    case "power_type":
                        ComboboxTable = MyBLL.Querypower_type();
                       return Result = TableToCombojson(ComboboxTable);
                    //查询瓦斯等级
                    case "gas_level":
                        ComboboxTable = MyBLL.Querygas_level();
                      return  Result = TableToCombojson(ComboboxTable);

                    //煤层自燃等级
                    case "self_iginte":
                        ComboboxTable = MyBLL.Queryself_iginte();
                      return  Result = TableToCombojson(ComboboxTable);

                    //煤层顶底板岩性
                    case "rock_behavio":
                        ComboboxTable = MyBLL.Queryrock_behavio();
                      return  Result = TableToCombojson(ComboboxTable);

                    //开拓巷道支护方式
                    case "lw_shoring_type":
                        ComboboxTable = MyBLL.Querylw_shoring_type();
                      return  Result = TableToCombojson(ComboboxTable);

                    //采面支护方式
                    case "me_shoring_type":
                        ComboboxTable = MyBLL.Queryme_shoring_type();
                        Result = TableToCombojson(ComboboxTable);
                        break;
                    //事故类型
                    case "accident_type":
                        ComboboxTable = MyBLL.Queryaccident_type();
                      return  Result = TableToCombojson(ComboboxTable);
  
                    case "Isorno":
                        Result += "[";
                        Result += "{\"id\":\"0\",\"text\":\"无\"},{\"id\":\"1\",\"text\":\"有\"}";
                        Result += "]";
                        return Result;
                    #endregion

                    #region 企业基本信息
                    //查询基本信息
                    case "JBXX":
                        ResultTable = MyBLL.QueryJBXX(MineCode);
                        break;
                    //根据ID 查询企业信息
                    case "JBXXByID":
                        ResultTable = MyBLL.QueryJBXXByID(ID);
                        if (ResultTable == null || ResultTable.Rows.Count < 1) return null;
                        string infos = string.Empty;
                        for (int i = 0; i < ResultTable.Columns.Count;i++ )
                        {
                            infos += ResultTable.Columns[i].ColumnName + "|" + ResultTable.Rows[0][i].ToString()+",";
                        }
                        infos = infos.Remove(infos.Length - 1);
                        return infos;
                    //增加企业基本信息
                    case "AddJBXX":
                        Result = MyBLL.AddJBXX(analysisInfo(Info)[0], analysisInfo(Info)[1]).ToString();
                        break;
                    //修改企业基本信息
                    case "AlterJBXX":
                        Result = MyBLL.AlterJBXX(ID, analysisInfo(Info)[0], analysisInfo(Info)[1]).ToString();
                        break;
                    //删除企业基本信息
                    case "DelJBXX":
                        Result = MyBLL.DelJBXX(ID).ToString();
                        break;
                    #endregion

                    #region 企业证照信息
                    //查询企业证照数据
                    case "QYZZ":
                        ResultTable = MyBLL.QueryQYZZ(MineCode);
                        break;
                    //添加企业证照数据
                    case "AddQYZZ":
                        //MyBLL.MyFileContent = FileContent;
                        Result = MyBLL.AddQYZZ(QYZZanalysis(Info),FileContent).ToString();
                        break;
                    //修改企业证照数据
                    case "AlterQYZZ":
                        Result = MyBLL.AlterQYZZ(QYZZanalysis(Info), FileContent, ID).ToString();
                        break;
                    //删除企业证照数据
                    case "DelQYZZ":
                        string[] ids = ID.Split(',');
                        Result = MyBLL.DelQYZZ(ids).ToString();
                        break;
                    #endregion

                    #region 相关资料信息
                    //相关资料文件类型下拉菜单
                    case "XGZLattachmenttype":
                        ComboboxTable = MyBLL.QueryXGZLType();
                       return Result = TableToCombojson(ComboboxTable);
                    //相关资料数据
                    case "XGZL":
                        ResultTable = MyBLL.QueryXGZL(MineCode);
                        break;
                    //添加相关资料数据
                    case "AddXGZL":
                        Result = MyBLL.AddXGZL(XGZLanalysis(Info), FileContent).ToString();
                        break;
                    //修改相关资料数据
                    case "AlterXGZL":
                        Result = MyBLL.AlterXGZL(XGZLanalysis(Info), FileContent, ID).ToString();
                        break;
                    //删除相关资料数据
                    case "DelXGZL":
                        Result = MyBLL.DelXGZL(ID).ToString();
                        break;
                    #endregion

                    #region 企业图纸档案
                    //查询企业图纸档案数据
                    case "QYTZ":
                        ResultTable = MyBLL.QueryQYTZ(MineCode);
                        break;
                    //加载图纸类型下拉
                    case "drawings_type":
                        ComboboxTable = MyBLL.Querydrawingstype();
                       return  Result = TableToCombojson(ComboboxTable);
                    //添加企业图纸数据
                    case "AddQYTZ":
                        Result = MyBLL.AddQYTZ(QYTZanalysis(Info), FileContent).ToString();
                        break;
                    //修改企业图纸数据
                    case "AlterQYTZ":
                        Result = MyBLL.AlterQYTZ(QYTZanalysis(Info), FileContent, ID).ToString();
                        break;
                        //删除企业图纸数据
                    case "DelQYTZ":
                        string[] TZids = ID.Split(',');
                        Result = MyBLL.DelQYTZ(TZids).ToString();
                        break;
                    #endregion

                    #region 煤矿设备信息
                    //查询设备数据
                    case "MKSB":
                        ResultTable = MyBLL.QueryMineDev(MineCode);
                        break;
                    //使用方向下拉
                    case "use_type":
                        ComboboxTable = MyBLL.QueryUseType();
                       return Result = TableToCombojson(ComboboxTable);
                    //设备类型下拉
                    case "eq_name":
                        ComboboxTable = MyBLL.Queryeqname();
                        return Result = TableToCombojson(ComboboxTable);
                        //添加煤矿设备
                    case "AddMKSB":
                        Result = MyBLL.AddMineDev(MKSBanalysis(Info)[0], MKSBanalysis(Info)[1]).ToString();
                        break;
                        //修改煤矿设备
                    case "AlterMKSB":
                        Result = MyBLL.AlterMineDev(MKSBanalysis(Info)[0],MKSBanalysis(Info)[1],ID).ToString();
                        break;
                        //删除煤矿设备
                    case "DelMKSB":
                        Result = MyBLL.DelMineDev(ID).ToString();
                        break;
                    #endregion

                    #region 安全管理机构
                    //查询安全管理机构信息
                    case "AQJG":
                        ResultTable = MyBLL.QueryAQJG(MineCode);
                        break;
                    //添加安全管理机构数据
                    case "AddAQJG":
                        Result = MyBLL.AddAQJG(AQJGanalysis(Info)).ToString();
                        break;
                    //修改安全管理机构数据
                    case "AlterAQJG":
                        Result = MyBLL.AlterAQJG(AQJGanalysis(Info), ID).ToString();
                        break;
                    //删除安全管理机构数据
                    case "DelAQJG":
                        Result = MyBLL.DelAQJG(ID).ToString();
                        break;
                    #endregion

                    #region 持证人员信息
                    //从事工种
                    case "type_work":
                        ComboboxTable = MyBLL.Queryworktype();
                     return  Result = TableToCombojson(ComboboxTable);
                      
                    //身体状态
                    case "health_state":
                        ComboboxTable = MyBLL.QueryHealth();
                      return  Result = TableToCombojson(ComboboxTable);
                      
                    //职务
                    case "post":
                        ComboboxTable = MyBLL.QueryPost();
                       return Result = TableToCombojson(ComboboxTable);
                       
                    // 持证人员
                    case "CZRY":
                        ResultTable = MyBLL.QueryCZRY(MineCode);
                        break;
                    case "AddCZRY":
                        Result = MyBLL.AddCZRY(MKSBanalysis(Info)[0], MKSBanalysis(Info)[1]).ToString();
                        break;
                    case "AlterCZRY":
                        Result = MyBLL.AlterCZRY(MKSBanalysis(Info)[0], MKSBanalysis(Info)[1], ID).ToString();
                        break;
                    case "DelCZRY":
                        Result = MyBLL.DelCZRY(ID).ToString();
                        break;
                    #endregion
                }

                if (Result.ToLower().Equals("true") | Result.ToLower().Equals("false"))
                {
                    return Result;
                }
                if (ResultTable != null)
                {
                    //在对DATATABLE进行序列化的时候，规范日期格式
                    IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

                    string json = JsonConvert.SerializeObject(ResultTable, Formatting.Indented, timeConverter);

                    int TotalRows = ResultTable.Rows.Count;
                    Result = "{\"total\":" + TotalRows + ",\"rows\":" + json + "}";
                }
                return Result;
            }
        }
        /// <summary>
        /// 将表格数据转化为easyui-combobox能识别的json
        /// </summary>
        /// <param name="ResultTable"></param>
        /// <returns></returns>
        public string TableToCombojson(DataTable ResultTable)
        {
            string Result = "[";
            for (int i = 0; i < ResultTable.Rows.Count; i++)
            {
                Result += "{\"id\":\"" + ResultTable.Rows[i]["Code"].ToString() + "\",\"text\":\"" + ResultTable.Rows[i]["Value"].ToString() + "\"},";
            }
            if (Result.Length > 2)
            {
                Result = Result.Remove(Result.Length - 1);
            }
            Result += "]";
            return Result;
        }
        /// <summary>
        /// 基本信息返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public List<string[]> analysisInfo(string myinfo)
        {
            string[] Prop = new string[86];
            string[] Val = new string[86];
            string[] a = myinfo.Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                string[] b = a[i].Split('|');
                if (b.Length > 1)
                {
                    try
                    {
                        Prop[i] = b[0];
                        if (b[1] == null || b[1] == "")
                        {
                            Val[i] = null;
                        }
                        else
                        {
                            Val[i] = b[1];
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            List<string[]> Result = new List<string[]>();
            Result.Add(Prop);
            Result.Add(Val);
            return Result;
        }
        /// <summary>
        /// 企业证照返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public string[] QYZZanalysis(string myinfo)
        {
            string[] a = myinfo.Split(',');
            string[] Value = new string[a.Length];
            string filepath = string.Empty;
            for (int i = 0; i < a.Count(); i++)
            {
                string[] b = a[i].Split('|');
                if (b[0] == "filename")
                {
                    filepath = b[1];
                    string[] filenames = b[1].Split('\\');
                    Value[i] = filenames[filenames.Length - 1];
                }
                else
                {
                    Value[i] = b[1];
                }
            }
            //FileContent = null;
            //if (File.Exists(filepath))
            //{
            //    FileStream fs = new FileStream(filepath, FileMode.Open);
            //    FileContent = new byte[fs.Length];
            //    fs.Read(FileContent, 0, FileContent.Length);
            //    fs.Close();
            //}
            return Value;
        }
        /// <summary>
        /// 企业图纸档案返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public string[] QYTZanalysis(string myinfo)
        {
            string[] a = myinfo.Split(',');
            string[] Result = new string[a.Length];
            string filepath = string.Empty;
            for (int i = 0; i < a.Length; i++)
            {
                string[] b = a[i].Split('|');
                if (b[0] != "filename")
                {
                    Result[i] = b[1];
                }
                else
                {
                    filepath = b[1];
                    Result[i] = b[1].Split('\\')[b[1].Split('\\').Length - 1];
                }
            }
            //FileContent = null;
            //if (File.Exists(filepath))
            //{
            //    try
            //    {
            //        FileStream fs = new FileStream(filepath, FileMode.Open);
            //        FileContent = new byte[fs.Length];
            //        fs.Read(FileContent, 0, FileContent.Length);
            //        fs.Close();
            //    }
            //    catch
            //    {

            //    }
            //}
            return Result;
        }
        /// <summary>
        /// 安全管理机构返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public string[] AQJGanalysis(string myinfo)
        {
            string[] Value = new string[6];
            string[] a = myinfo.Split(',');
            string filepath = string.Empty;
            for (int i = 0; i < a.Count(); i++)
            {
                string[] b = a[i].Split('|');
                Value[i] = b[1];
            }
            return Value;
        }
        /// <summary>
        /// 相关资料返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public string[] XGZLanalysis(string myinfo)
        {
            string[] a = myinfo.Split(',');
            string[] Result = new string[a.Length];
            string filepath = string.Empty;
            for (int i = 0; i < a.Length; i++)
            {
                string[] b = a[i].Split('|');
                if (b[0] != "filename")
                {
                    Result[i] = b[1];
                }
                else
                {
                    filepath = b[1];
                    Result[i] = b[1].Split('\\')[b[1].Split('\\').Length - 1];
                }
            }
            //FileContent = null;
            //if (File.Exists(filepath))
            //{
            //    FileStream fs = new FileStream(filepath, FileMode.Open);
            //    FileContent = new byte[fs.Length];
            //    fs.Read(FileContent, 0, FileContent.Length);
            //    fs.Close();
            //}
            return Result;
        }
        /// <summary>
        /// 煤矿设备返回数据处理
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        public List<string[]> MKSBanalysis(string myinfo)
        {
            string[] a = myinfo.Split(',');
            string[] Prop = new string[a.Length];
            string[] Val = new string[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                string[] b = a[i].Split('|');
                if (b.Length > 1)
                {
                    try
                    {
                        Prop[i] = b[0];
                        if (b[1] == null || b[1] == "")
                        {
                            Val[i] = null;
                        }
                        else
                        {
                            Val[i] = b[1];
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            List<string[]> Result = new List<string[]>();
            Result.Add(Prop);
            Result.Add(Val);
            return Result;
        }
    }
}