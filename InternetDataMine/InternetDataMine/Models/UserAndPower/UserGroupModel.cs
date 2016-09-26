using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetDataMine.Models.UserAndPower
{
    public class UserGroupModel
    {
        private string groupID;
        /// <summary>
        /// 用户组ID
        /// </summary>
        public string GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        private string roleID;
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        private string mineCode;
        /// <summary>
        /// 煤矿编号
        /// </summary>
        public string MineCode
        {
            get { return mineCode; }
            set { mineCode = value; }
        }

        private string groupName;
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        private string groupDescription;
        /// <summary>
        /// 用户组描述
        /// </summary>
        public string GroupDescription
        {
            get { return groupDescription; }
            set { groupDescription = value; }
        }

        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}