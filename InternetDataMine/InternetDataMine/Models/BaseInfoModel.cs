﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using InternetDataMine.Models.DataService;
namespace InternetDataMine.Models
{
    public class BaseInfoModel
    {
        DataBLL bll = new DataBLL();
        public string ReturnTreeMineInfo(string mineCode)
        {
            DataTable dt = bll.GetMineInfo(mineCode);

            string json = "[";

            string city = "";
            if(dt !=null && dt.Rows.Count >0)
            {
              
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(city!=dt.Rows[i]["City"].ToString() && city=="")
                    {
                        city = dt.Rows[i]["City"].ToString();
                        json += "{\"id\":\"" + Guid.NewGuid().ToString() + "\"" +
                       ",\"text\":\"" + city + "\"" +
                       ",\"minecode\":0" +
                       ",\"AQJKState\":\"\"" +
                       ",\"RYGLState\":\"\"";
                        json += " ,\"children\": [";
                    }
                    else if(city!=dt.Rows[i]["City"].ToString() && city!="")
                    {
                        city = dt.Rows[i]["City"].ToString();
                        json += "]},{\"id\":\"" + Guid.NewGuid().ToString() + "\"" +
                       ",\"text\":\"" + city + "\"" +
                       ",\"minecode\":0" +
                       ",\"AQJKState\":\"\"" +
                       ",\"RYGLState\":\"\"";
                        json += " ,\"children\": [";
                    }
                    else
                    {
                        json += ",";
                    }
                   // json += string.Format(@"{{ " + "\"id\": \"{0}\",\"text\": \"{1}\",\"minecode\":\"{4}\" ,\"children\":[{{\"id\":\"AQJK\",\"text\":\"安全监控\",\"State\":\"{2}\"}},{{\"id\":\"RYGL\",\"text\":\"人员管理\",\"State\":\"{3}\"}} ,{{\"id\":\"KSYL\",\"text\":\"矿山压力\",\"State\":\"{5}\"}} ,{{\"id\":\"HZSG\",\"text\":\"火灾束管\",\"State\":\"{6}\"}}            ]}}", dr["rowid"].ToString(),
                               //   dr["SimpleName"].ToString(), dr["AQJKState"].ToString(), dr["RYGLState"].ToString(), dr["MineCode"].ToString(), dr["ksylstate"].ToString(), dr["hzsgstate"].ToString());
                    DataRow dr = dt.Rows[i];
                    json += string.Format(@"{{ " + "\"id\": \"{0}\",\"text\": \"{1}\",\"AQJKState\": \"{2}\",\"RYGLState\": \"{3}\",\"minecode\":\"{4}\",\"KSYLState\":\"{5}\",\"HZSGState\":\"{6}\"}}", dr["rowid"].ToString(),
                                          dr["SimpleName"].ToString(), dr["AQJKState"].ToString(), dr["RYGLState"].ToString(), dr["MineCode"].ToString(), dr["KSYLState"].ToString(), dr["hzsgstate"].ToString());

                    if (i < dt.Rows.Count - 1)
                    {
                        //json += ",";
                    }
                }
            }
            json += "]}]";
            return json;// "[{\"id\": 1,\"text\": \"Node 1\",\"state\": \"closed\", \"children\": [{\"id\": 11,\"text\": \"Node 11\" },{ \"id\": 12,\"text\": \"Node 12\"}]},{\"id\": 2,\"text\": \"Node 2\",\"state\": \"closed\"}]";
        }

        /// <summary>
        /// 获取设备类型列表
        /// </summary>
        /// <param name="mineCode"></param>
        /// <returns></returns>
        public string ReturnDeviceType(string mineCode,string Type)
        {
            string OutString = "[";
            if (mineCode == "" || mineCode == "0")
            {
                return "[]";
            }
            else
            {
                DataTable dt = bll.GetDevTypeList(mineCode);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Type"].ToString().Equals(Type))
                    {
                        OutString += "{\"TypeCode\":\"" + dr["TypeCode"] + "\",";
                        OutString += "\"TypeName\":\"" + dr["TypeName"] + "\",";
                        OutString += "\"Unit\":\"" + dr["Unit"] + "\",";
                        OutString += "\"Type\":\"" + dr["Type"] + "\"},";
                    }
                }

                OutString = OutString.Substring(0, OutString.Length - 1);

                OutString += "]";

                return OutString;
            }
            
        }

        /// <summary>
        /// 根据煤矿编号、设备名称编号、设备类型查询测点列表 2016-1-11 申云飞
        /// </summary>
        /// <param name="mineCode">煤矿编号</param>
        /// <param name="SensorNameCode">设备名称编号</param>
        /// <param name="devtype">设备类型 A表示模拟量、D表示开关量,L表示累积量</param>
        /// <returns></returns>
        public string ReturnComboDeviceInfo(string mineCode,string SensorNameCode,string devtype)
        {
            DataTableCollection dts = bll.GetSensorInfo(mineCode, SensorNameCode, devtype);
            string ReturnString = "{";
            /*
             * 
             * 
             * 
wheredata = " select Row_Number() over (order by getdate() asc) as TmpID,SimpleName,o.SensorNum,d.TypeName,d.Unit,o.Place,o.Range,"
+ " o.AlarmLower,o.AlarmHigh,o.AlarmLowerRemove,o.AlarmHighRemove,o.OutPowerLower,o.OutPowerHigh,o.InPowerLower,o.InPowerHigh,"
+ " o.SensorTime from AQMC o left join DeviceType d on o.Type=d.TypeCode left join MineConfig g on o.MineCode=g.MineCode "
             * 
            */

            if (mineCode=="0" || mineCode=="")
            {
                return "{}";
            }
            else
            {
                if (devtype == "A")
                {
                    if (dts.Count > 0)
                    {
                        ReturnString += "\"total\":" + dts[0].Rows[0][0].ToString() + ",\"rows\":[";
                        foreach (DataRow dr in dts[1].Rows)
                        {
                            ReturnString += "{\"SensorNum\":\"" + dr["SensorNum"] + "\",";
                            ReturnString += "\"TypeName\":\"" + dr["TypeName"] + "\",";
                            ReturnString += "\"Place\":\"" + dr["Place"] + "\",";
                            ReturnString += "\"Unit\":\"" + dr["Unit"] + "\",";
                            ReturnString += "\"AlarmLower\":\"" + dr["AlarmLower"] + "\",";
                            ReturnString += "\"AlarmHigh\":\"" + dr["AlarmHigh"] + "\"},";
                        }

                        ReturnString = ReturnString.Substring(0, ReturnString.Length - 1);
                        ReturnString += "]}";

                    }
                }
                else if(devtype=="D")
                {
                    if (dts.Count > 0)
                    {
                        ReturnString += "\"total\":" + dts[0].Rows[0][0].ToString() + ",\"rows\":[";
                        foreach (DataRow dr in dts[1].Rows)
                        {
                            ReturnString += "{\"SensorNum\":\"" + dr["SensorNum"] + "\",";
                            ReturnString += "\"TypeName\":\"" + dr["TypeName"] + "\",";
                            ReturnString += "\"Place\":\"" + dr["Place"] + "\",";
                            ReturnString += "\"ZeroMeaning\":\"" + dr["0态含义"] + "\",";
                            ReturnString += "\"OneMeaning\":\"" + dr["1态含义"] + "\",";
                            ReturnString += "\"TwoMeaning\":\"" + dr["2态含义"] + "\"},";
                        }

                        ReturnString = ReturnString.Substring(0, ReturnString.Length - 1);
                        ReturnString += "]}";
                    }
                }
                return ReturnString;
            }
        }



        /// <summary>
        /// 为煤矿下拉控件-树形结构加载内容
        /// </summary>
        /// <param name="mineCode"></param>
        /// <returns></returns>
        public string ReturnComboTreeMineInfo(string mineCode)
        {
            DataTable dt = bll.GetMineInfo(mineCode);

            string json = "[";

            string city = "";
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (city != dt.Rows[i]["City"].ToString() && city == "")
                    {
                        city = dt.Rows[i]["City"].ToString();
                        json += "{\"id\":\"0\"" +
                       ",\"text\":\"" + city + "\"" +
                       ",\"state\":\"open\"" +
                       ",\"minecode\":0" +
                       ",\"AQJKState\":\"\"" +
                       ",\"RYGLState\":\"\"";
                        json += " ,\"children\": [";
                    }
                    else if (city != dt.Rows[i]["City"].ToString() && city != "")
                    {
                        city = dt.Rows[i]["City"].ToString();
                        json += "]},{\"id\":\"0\"" +
                       ",\"text\":\"" + city + "\"" +
                        ",\"state\":\"closed\"" +
                       ",\"minecode\":0" +
                       ",\"AQJKState\":\"\"" +
                       ",\"RYGLState\":\"\"";
                        json += " ,\"children\": [";
                    }
                    else
                    {
                        json += ",";
                    }

                    DataRow dr = dt.Rows[i];
                    json += string.Format(@"{{ " + "\"id\": \"{0}\",\"text\": \"{1}\",\"AQJKState\": \"{2}\",\"RYGLState\": \"{3}\",\"minecode\":\"{4}\"}}", dr["MineCode"].ToString(),
                                          dr["SimpleName"].ToString(), dr["AQJKState"].ToString(), dr["RYGLState"].ToString(), dr["MineCode"].ToString());

                    if (i < dt.Rows.Count - 1)
                    {
                        //json += ",";
                    }
                }
            }
            json += "]}]";

            return json;// "[{\"id\": 1,\"text\": \"Node 1\",\"state\": \"closed\", \"children\": [{\"id\": 11,\"text\": \"Node 11\" },{ \"id\": 12,\"text\": \"Node 12\"}]},{\"id\": 2,\"text\": \"Node 2\",\"state\": \"closed\"}]";

        }


        public string GetMineTreeList(string mineCode)
        {
            DataTable dt = bll.GetMineInfo(mineCode);

            string json = "[{" +
                          "\"id\":\"0\"" +
                          ",\"text\":\"全部\"";

            if (dt != null && dt.Rows.Count > 0)
            {
                json += " ,\"children\": [";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    json += string.Format(@"{{ " + "\"id\": \"{0}\",\"text\": \"{1}\"}}",dr["MineCode"].ToString(),dr["SimpleName"].ToString());

                    if (i < dt.Rows.Count - 1)
                    {
                        json += ",";
                    }
                }
                json += "]";
            }
            json += "}]";

            return json;// "[{\"id\": 1,\"text\": \"Node 1\",\"state\": \"closed\", \"children\": [{\"id\": 11,\"text\": \"Node 11\" },{ \"id\": 12,\"text\": \"Node 12\"}]},{\"id\": 2,\"text\": \"Node 2\",\"state\": \"closed\"}]";

        }
    }
}