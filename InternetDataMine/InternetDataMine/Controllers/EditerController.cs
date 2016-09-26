using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json;
using InternetDataMine.Models;
namespace InternetDataMine.Controllers
{
    public class EditerController : Controller
    {
        //
        // GET: /Editer/
        InternetDataMine.Models.UserAndPower.DataSaveModel MySave = new Models.UserAndPower.DataSaveModel();
        InternetDataMine.Models.UserAndPower.TransDataModel Trans;
        public ActionResult UserManager()
        {
            return View();
        }

        public ActionResult MyDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RoleID", typeof(int));
            dt.Columns.Add("RoleName", typeof(string));
            dt.Columns.Add("MineName", typeof(string));
            for (int i = 0; i < 100; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "OPerator" + i.ToString();
                dr[2] = "wjz";
                dt.Rows.Add(dr);
            }


            TestModel testModel = new TestModel();

            testModel.DataCol = "{\"total\":100,\"rows\":" + JsonConvert.SerializeObject(dt) + "}";

            return View(testModel);
        }

        public ActionResult TransData(string type, string MenuID, string MenuParentID, string MenuName, string MenuPath, string MenuDescription, string Remark, string RoleID, string RoleName, string RoleDescription, string MineCode, string GroupName,string MenuIDs, string GroupDescription, string GroupID,string Condition, string ClassNO,string MenuIndex,string IsCheck,string json,string UserName,string Password,string UserID,string oldpassword)
        {
            Trans = new Models.UserAndPower.TransDataModel(type, MenuID, MenuParentID, MenuName, MenuDescription, MenuPath, Remark, RoleID, RoleName, RoleDescription, MineCode, GroupName,MenuIDs, GroupDescription, GroupID, Condition,ClassNO,MenuIndex,IsCheck,json,UserName,Password,UserID,oldpassword);

            return View(Trans);
        }

        private readonly DataDAL _dal = new DataDAL();

        public void TransData_PwdSet(string id, string pwd)
        {
            string sql = string.Format(@"Update ShineView_Data.dbo.UsersInfo Set PassWord = '{0}' Where UserID = '{1}'", ToolModel.GetMd5Hash(pwd), id);

            if (_dal.ExcuteSql(sql))
            {
                Response.Write("密码修改成功.");
            }
            else
            {
                Response.Write("密码修改失败.");
            }
            Response.End();
        }

        public ActionResult testcombogrid()
        {
            return View();
        }

        public ActionResult MenusManager()
        {
            return View();
        }

        public ActionResult RoleManager()
        {
            return View();
        }

        public ActionResult UserGroupManager()
        {
            return View();
        }

        public ActionResult RolesPowerManager()
        {
            return View();
        }
    }
}
