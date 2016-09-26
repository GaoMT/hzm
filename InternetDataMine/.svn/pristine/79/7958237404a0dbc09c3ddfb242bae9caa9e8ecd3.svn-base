using System.Data;
using Newtonsoft.Json;

namespace InternetDataMine.Models
{
    public class RealDataModel
    {
        #region [变量]
        InternetDataMine.Models.DataService.DataBLL _BLL_Data = new DataService.DataBLL();
        #endregion

        public string GetAllAlarmInfo(string MindeCode,string AccountID)
        {
          DataTable dt =  _BLL_Data.GetAllAlarmInfo(MindeCode,AccountID);
          return JsonConvert.SerializeObject(dt);
        }

        public string MineName { get; set; }

        public string SensorNum { get; set; }

        public string RealValue { get; set; }

        public string BodyXMLPath { get { return "../dataxml/TestList.xml"; } }

        public string ToolXMLPath { get { return "../dataxml/RealDataBar.xml"; } }

        /// <summary>
        /// 获取所有煤矿名称
        /// </summary>
        /// <returns></returns>
        public string GetMineName()
        {
            DataTable dt =  _BLL_Data.MineList();
            return JsonConvert.SerializeObject(dt);
        }
    }
}