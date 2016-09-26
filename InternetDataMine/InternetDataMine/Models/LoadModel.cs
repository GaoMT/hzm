using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetDataMine.Models
{
    public class LoadModel
    {
        //查询条件加载XML路径
        public string QueryBarPath { get; set; }
        //数据格式加载XML路径
        public string QueryDataPath { get; set; }
        //页面显示时加载的数据
        public string PreLoadData { get; set; }
        //系统类别 1表示安全监控 2表示人员管理
        public int SystemType { get; set; }
        //用户权限
        public string UserAbility { get; set; }
        //用户所属煤矿编码
        public string UserMineCode { get; set; }
        //页面类型：报表、数据
        public string PageType { get; set; }
        //报表名称
        public string ReportName { get; set; }
        //页面高度
        public string Height { get; set; }

        public string PageTitle { get; set; }

        public string Width { get; set; }
        //数据列
        public string DataCol { get; set; }
    }
}