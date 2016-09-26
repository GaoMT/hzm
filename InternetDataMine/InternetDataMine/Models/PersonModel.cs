using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using InternetDataMine.Models.DataService;
namespace InternetDataMine.Models
{
    public class PersonModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Remark { get; set; }

        ///// <summary>
        ///// 分页查询测试
        ///// </summary>
        ///// <returns>数据表集合</returns>
        //public DataTableCollection test()
        //{
        //    DataBLL _BLL_Data = new DataBLL();
        //    return _BLL_Data.Test(1, 20, "");
        //}
        public string ReturnPersons()
        {
            DataTable DT = new DataTable();

            //DT.Columns.Add("Name", typeof(string));
            //DT.Columns.Add("Age", typeof(int));
            //DT.Columns.Add("Remark",typeof(string));
            DT.Columns.Add("Name");
            DT.Columns.Add("Age");
            DT.Columns.Add("Remark");
            for(int i=0;i<100;i++)
            {
                DataRow dr = DT.NewRow();
                dr["Name"] = "李" + i.ToString();
                dr[1] = i.ToString();
                dr[2] = "备注"+i.ToString();
                DT.Rows.Add(dr);

            }
           return  JsonConvert.SerializeObject(DT);
        
        }

    }
}