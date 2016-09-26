using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
namespace InternetDataMine.Models.DataService
{
    public class ReadConfig
    {
        string configpath = "";
        public ReadConfig()
        { 
            //configpath = Directory.GetCurrentDirectory() + @"\web.config";
           
        }

        /// <summary>
        /// 指定读取的config文件
        /// </summary>
        /// <param name="configpath">config文件路径</param>
        public ReadConfig(string configpath)
        {
            this.configpath = configpath;
        }

        /// <summary>
        /// 获取配置文件中的连接串
        /// </summary>
        /// <returns></returns>
        public string GetSQLConnection()
        {
            return GetValue("ConnectionString");
        }
        public string GetSQLConnectionHis()
        {
            return GetValue("ConnectionStringHis");
        }

        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <param name="AppKey">键</param>
        /// <returns></returns>
        public string GetValue(string AppKey)
        {
            return ConfigurationManager.AppSettings["ConnectionString"].ToString();
            //System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            //xDoc.Load(configpath);
            //System.Xml.XmlNode xNode;
            //xNode = xDoc.SelectSingleNode("//appSettings");
            //if (xNode != null)
            //{
            //    System.Xml.XmlElement xElem = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            //    if (xElem != null)
            //    {
            //        string value = xElem.GetAttribute("value");
            //        return value;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}
