using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using InternetDataMine.Models;
using InternetDataMine.Models.DataService;
using System.Data;
namespace InternetDataMine.Controllers
{
    public class CompanyInfoManagerController : Controller
    {
        //
        // GET: /CompanyInfoManager/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 读取上传文件内容
        /// </summary>
        /// <param name="Type">数据操作类型</param>
        /// <returns></returns>
        private byte[] GetFileContent()
        {
          
            HttpFileCollectionBase files = Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string error = string.Empty;
            if (files.Count > 0)
            {
                if (files[0].FileName == "")
                {
                    return null;
                }
                else
                {
                   byte[] Buffer = new byte[files[0].ContentLength];
                   files[0].InputStream.Read(Buffer,0,Buffer.Length);
                   return Buffer;
                }
            }
            else
            {
                return null;
            }
        }


        public void DownloadFile(string type,string ID)
        {
            TransCompanyModel transModel = new TransCompanyModel(type, "", ID, "");
            DataTable dt = transModel.DownloadFile();
            Response.Buffer = true;
            Response.ContentType = "application/octet-stream";

            byte[] readfilecontent = (byte[])dt.Rows[0]["filecontent"];

            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(dt.Rows[0]["filename"].ToString(), System.Text.Encoding.UTF8));

            Response.BinaryWrite(readfilecontent);
            Response.Flush();
            Response.End();

            //Response.OutputStream.Write(readfilecontent, 0, readfilecontent.Length);
            //Response.OutputStream.Flush();
            //Response.OutputStream.Close();
            //Response.Flush();
            //Response.End();

            //Response.OutputStream(dt.Rows[0]["filecontent"], 0, dt.Rows[0]["filecontent"]);
        }

       
        /// <summary>
        /// 处理公司档案信息
        /// </summary>
        /// <param name="type">档案相关信息类别</param>
        /// <param name="Info">档案信息内容</param>
        /// <param name="ID">档案信息编号</param>
        /// <param name="MineCode">煤矿编号</param>
        /// <returns></returns>
        public void TransCompanyData(string type,string Info,string ID,string MineCode)
        {
            TransCompanyModel Trans = new TransCompanyModel(type, Info, ID, MineCode);
            string ResponseMessage = string.Empty;
            if (type.IndexOf("AddQYZZ") >= 0 | type.IndexOf("AlterQYZZ") >= 0 | type.IndexOf("AddQYTZ") >= 0
                | type.IndexOf("AlterQYTZ") >= 0 | type.IndexOf("AddXGZL") >= 0 | type.IndexOf("AlterXGZL") >= 0)
            {
                byte[] ReadFileResult = GetFileContent();
                if (ReadFileResult == null)
                {
                    ResponseMessage = "{ error:'没有选择要上传的文件,请选择！', msg:'请选择要上传的文件'}";
                }
                else
                {
                    Trans.FileContent = ReadFileResult;
                    string ReturnMessage = Trans.GetJson;
                    switch (ReturnMessage)
                    {
                        case "True":
                            ResponseMessage = "{ error:'', msg:'恭喜，数据保存成功！'}";
                            break;
                        case "False":
                            ResponseMessage = "{ error:'数据保存失败！', msg:'数据保存失败，数据不允许重复或网络故障！'}";
                            break;
                    }

                }
            }
            else
            {
                ResponseMessage = Trans.GetJson;

            }
            Response.Write(ResponseMessage);
            Response.End();
        }

        /// <summary>
        /// 加载煤矿基本信息，用于绑定煤矿名称的下拉列表，用于插入和修改
        /// </summary>
        public void QueryJBXX()
        {
            DataBLL bll = new DataBLL();
            Response.Write(JsonConvert.SerializeObject(bll.QuereyJBXXSimple()));
            Response.End();
        }

        /// <summary>
        /// 加载煤矿基本信息，用于绑定煤矿名称的下拉列表，用于查询
        /// </summary>
        public void QueryJBXXAll()
        {
            DataBLL bll = new DataBLL();
            Response.Write(JsonConvert.SerializeObject(bll.QuereyJBXXSimple()).Insert(1, "{\"id\":\"\",\"minecode\":\"\",\"simplename\":\"全部\"},"));
            Response.End();
        }

        public void QuerySysTypes()
        {
            DataDAL dal = new DataDAL();
            Response.Write(JsonConvert.SerializeObject(dal.ReturnData("Select * From dbo.SystemTypesInfo")));
            Response.End();
        }


        public ActionResult JBXX()
        {
            return View();
        }

        public ActionResult QYZZ()
        {
            return View();
        }

        public ActionResult XGZL()
        {
            return View();
        }

        public ActionResult QYTZ()
        {
            return View();
        }

        public ActionResult MKSB()
        {
            return View();
        }

        public ActionResult AQJG()
        {
            return View();
        }

        public ActionResult CZRY()
        {
            return View();
        }

        public ActionResult JBXXEdit()
        {
            return View();
        }
    }
}
