using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetDataMine.Models
{
    public class TroubleProcessModel
    {
        /// <summary>
        /// 自增列
        /// </summary>
        public int RowID { get; set; }
        /// <summary>
        /// 隐患编号
        /// </summary>
        public int TroubleID { get; set; }
        /// <summary>
        /// 隐患处理单位类别 1企业处理 2 监管单位处理
        /// </summary>
        public int ProcessCategory { get; set; }
        /// <summary>
        /// 隐患处理意见
        /// </summary>
        public string TroubleProcessContent { get; set; }
        /// <summary>
        /// 隐患处理日期
        /// </summary>
        public string TroubleProcessDate { get; set; }
        /// <summary>
        /// 隐患处理人
        /// </summary>
        public string TroubleProcesser { get; set; }
        /// <summary>
        /// 隐患处理截止日期
        /// </summary>
        public string TroubleProcessCompleteDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}