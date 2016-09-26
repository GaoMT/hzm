using System.Data;
using InternetDataMine.Models.DataService;
using Newtonsoft.Json;

namespace InternetDataMine.Models.Config
{
    public class SubSystemConfigModel
    {
        private readonly DataDAL _dal = new DataDAL();

        public string Query()
        {
            DataTable dt = _dal.GetSystemConfigList();
            return JsonConvert.SerializeObject(dt);
        }

        public bool Exec(string sql)
        {
            return _dal.ExcuteSql(sql);
        }
    }
}