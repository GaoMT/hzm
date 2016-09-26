using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft;
namespace InternetDataMine.Controllers
{
    public class DataOrganizationController : Controller
    {
        //
        // GET: /DataOrganization/

        public string Index()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("MineName");
            dt.Columns.Add("Content");
            for(int i=0;i<10;i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dt.Rows.Add(dr);
            }
            return JsonConvert.SerializeObject(dt);
        }
    }
}
