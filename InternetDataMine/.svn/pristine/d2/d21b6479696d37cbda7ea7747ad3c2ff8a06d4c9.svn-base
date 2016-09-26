using System.Data;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json;

namespace InternetDataMine.Models.Config
{
    public class MineConfigModel
    {
        //private readonly DataBLL _bllData = new DataBLL();
        private readonly DataDAL _dal = new DataDAL();

        public string Query()
        {
            DataTable dt = _dal.GetMineList("1=1");
            return JsonConvert.SerializeObject(dt);
        }

        public bool Exec(string sql)
        {
            return _dal.ExcuteSql(sql);
        }
    }
}