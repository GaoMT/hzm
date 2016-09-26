using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetDataMine.Models
{
    public class WorkShiftModel
    {
        /// <summary>
        /// 自增列
        /// </summary>
        public int RowID { get; set; }
        /// <summary>
        /// 煤矿编号
        /// </summary>
        public string MineCode { get; set; }
        /// <summary>
        /// 交接班部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 交班时间
        /// </summary>
        public string PreWorkTime { get; set; }
        /// <summary>
        /// 交班情况
        /// </summary>
        public string PreWorkContent { get; set; }
        /// <summary>
        /// 交班班次
        /// </summary>
        public string PreWorkShift { get; set; }
        /// <summary>
        /// 交班人姓名
        /// </summary>
        public string PreWorkPersonName { get; set; }
        /// <summary>
        /// 接班时间
        /// </summary>
        public string NextWorkTime { get; set; }
        /// <summary>
        /// 接班情况
        /// </summary>
        public string NextWorkContent { get; set; }
        /// <summary>
        /// 接班班次
        /// </summary>
        public string NextWorkShift { get; set; }
        /// <summary>
        /// 接班人姓名
        /// </summary>
        public string NextWorkPersonName { get; set; }

        public int WorkShiftCategory { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}