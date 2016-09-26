using System;
using System.Web.Mvc;
using InternetDataMine.Models.Config;

namespace InternetDataMine.Controllers
{
    public class ConfigController : Controller
    {
        //
        // GET: /Config/

        private readonly MineConfigModel _mineCfgModel = new MineConfigModel();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MineConfig()
        {
            return View();
        }

        public void MineConfig_Query()
        {
            var mineCfg = new MineConfigModel();

            string json = mineCfg.Query();

            Response.Write(json);
            Response.End();
        }

        public void MineConfig_Insert(string mineCode, string simpleName, string fullName, string type, string managers, string address, 
            string works, string phone, string miningBureau, string maCode, string mineGroup, string coordinates, string city, string remark)
        {
            string sql = string.Format(@"
INSERT INTO [dbo].[MineConfig]
           ([ID],[MineCode],[SimpleName],[City],[FullName],[Type],
            [Managers],[Address],[Workers],[Phone],[MiningBureau]
           ,[MACode],[MineGroup],[Coordinates],[Remark])
     VALUES
           ('{0}','{1}','{2}','{3}','{4}','{5}'
           ,'{6}','{7}',{8},'{9}','{10}'
           ,'{11}','{12}','{13}','{14}')
", Guid.NewGuid(), mineCode, simpleName, city, fullName, type,
    managers, address, int.Parse(works), phone, miningBureau,
    mineCode, mineGroup, coordinates, remark);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("保存成功.");
                Response.End();
            }
        }

        public void MineConfig_Update(string id, string mineCode, string simpleName, string fullName, string type, string managers, string address,
            string works, string phone, string miningBureau, string maCode, string mineGroup, string coordinates, string city, string remark)
        {
            string sql = string.Format(@"
Update [MineConfig]
   Set [MineCode] = '{1}',[SimpleName] = '{2}',[City] = '{3}',[FullName] = '{4}'
      ,[Type] = '{5}',[Managers] = '{6}',[Address] = '{7}',[Workers] = {8}
      ,[Phone] = '{9}',[MiningBureau] = '{10}',[MACode] = '{11}',[MineGroup] = '{12}'
      ,[Coordinates] = '{13}',[Remark] = '{14}'
    Where [ID] = '{0}'
", id, mineCode, simpleName, city, fullName, type,
    managers, address, int.Parse(works), phone, miningBureau,
    maCode, mineGroup, coordinates, remark);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("保存成功.");
                Response.End();
            }
        }

        public void MineConfig_Delete(string condition)
        {
            string idString = string.Empty;
            var ids = condition.Split(',');
            foreach (var id in ids)
            {
                idString += string.Format("'{0}',", id);
            }
            if (idString.Length > 1)
                idString = idString.Remove(idString.Length - 1);

            string sql =
                string.Format(
                    "Delete From MineConfig Where Id In ({0})", idString);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("删除成功.");
                Response.End();
            }
        }

        public ActionResult SubSystemConfig()
        {
            return View();
        }

        public void SubSystemConfig_Query()
        {
            var subSysCfg = new SubSystemConfigModel();

            string json = subSysCfg.Query();

            Response.Write(json);
            Response.End();
        }


        public void SubSystemConfig_Insert(string sysMaCode, string sysName, string MineCode, string maintainer,
            string supplier, string phone, string ipAddress, string enabled, string remark,string sysType)
        {
            string sql = string.Format(@"INSERT INTO [ShineView_Data].[dbo].[SystemConfig]
           ([SystemCode],[MineCode],[TypeCode],[Name],[IsEnabled]
           ,[Maintainer]
           ,[SystemMACode]
           ,[Supplier]
           ,[Phone]
           ,[IP]
           ,[Remark])
     VALUES
           ('{0}','{1}',{2},'{3}',{4},'{5}'
           ,'{6}','{7}','{8}','{9}','{10}')
", Guid.NewGuid(), MineCode, sysType,sysName, bool.Parse(enabled) ? 1 : 0, maintainer,
 sysMaCode, supplier, phone,ipAddress, remark);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("保存成功.");
                Response.End();
            }
        }

        public void SubSystemConfig_Update(string sysCode, string sysMaCode, string sysName, string MineCode, string maintainer,
            string supplier, string phone, string ipAddress, string enabled, string remark, string sysType)
        {
            string sql = string.Format(@"

UPDATE [ShineView_Data].[dbo].[SystemConfig]
   SET [MineCode] = '{1}'
      ,[TypeCode] = {2}
      ,[Name] = '{3}'
      ,[IsEnabled] = {4}
      ,[Maintainer] = '{5}'
      ,[SystemMACode] = '{6}'
      ,[Supplier] = '{7}'
      ,[Phone] = '{8}'
      ,[IP] = '{9}'
      ,[Remark] = '{10}'
 WHERE [SystemCode] = '{0}'
", sysCode, MineCode, sysType, sysName, bool.Parse(enabled) ? 1 : 0, maintainer,
 sysMaCode, supplier, phone, ipAddress, remark);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("保存成功.");
                Response.End();
            }
            else
            {
                Response.Write("删除失败.");
            }
        }

        public void SubSystemConfig_Delete(string condition)
        {
            string idString = string.Empty;
            var ids = condition.Split(',');
            foreach (var id in ids)
            {
                idString += string.Format("'{0}',", id);
            }
            if (idString.Length > 1)
                idString = idString.Remove(idString.Length - 1);

            string sql =
                string.Format(
                    "Delete From [ShineView_Data].[dbo].[SystemConfig] Where SystemCode In ({0})", idString);

            if (_mineCfgModel.Exec(sql))
            {
                Response.Write("删除成功.");
            }
            else
            {
                Response.Write("删除失败.");
            }
            Response.End();
        }

    }
}
