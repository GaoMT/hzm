﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using InternetDataMine.Models;

namespace InternetDataMine.Models.UserAndPower
{
    public class TransDataModel
    {
        DataService.DataBLL MyBLL = new DataService.DataBLL();
        ToolModel MyTool = new ToolModel();
        string OperationType = string.Empty;
        string MenuID = string.Empty;
        string MenuParentID = string.Empty;
        string MenuName = string.Empty;
        string MenuDescription = string.Empty;
        string MenuPath = string.Empty;
        string Remark = string.Empty;
        string RoleID = string.Empty;
        string RoleName = string.Empty;
        string RoleDescription = string.Empty;
        string GroupName = string.Empty;
        string GroupDescription = string.Empty;
        string MineCode = string.Empty;
        string GroupID = string.Empty;
        string ClassNO = string.Empty;
        string MenuIndex = string.Empty;
        string IsChecked = string.Empty;
        string MenuIDs = string.Empty;
        string test = string.Empty;
        string transjson = string.Empty;
        string UserName = string.Empty;
        string Password = string.Empty;
        string UserID = string.Empty;
        string oldpassword = string.Empty;
        string Condition = string.Empty;
        public TransDataModel()
        {

        }

        public TransDataModel(string Type, string RoleID, string RoleName, string RoleDescription, string Remark)
        {
            this.OperationType = Type;
            this.RoleID = RoleID;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.Remark = Remark;
        }

        public TransDataModel(string Type, string MenuID, string MenuParentID, string MenuName, string MenuDescription, string MenuPath, string Remark, string RoleID, string RoleName, string RoleDescription, string MineCode, string GroupName,string MenuIDs, string GroupDescription, string GroupID, string Condition, string ClassNO, string MenuIndex, string IsCheck, string json, string UserName, string Password, string UserID, string oldpassword)
        {
            this.OperationType = Type;
            this.MenuID = MenuID;
            this.MenuParentID = MenuParentID;
            this.MenuName = MenuName;
            this.MenuDescription = MenuDescription;
            this.MenuPath = MenuPath;
            this.Remark = Remark;
            this.RoleID = RoleID;
            this.RoleName = RoleName;
            this.RoleDescription = RoleDescription;
            this.MineCode = MineCode;
            this.GroupName = GroupName;
            this.GroupDescription = GroupDescription;
            this.GroupID = GroupID;
            this.ClassNO = ClassNO;
            this.MenuIndex = MenuIndex;
            this.IsChecked = IsCheck;
            this.transjson = json;
            this.UserName = UserName;
            this.Password = Password;
            this.UserID = UserID;
            this.oldpassword = oldpassword;
            this.Condition = Condition;
            this.MenuIDs = MenuIDs;
        }

        public string GetDataJson
        {
            get
            {
                DataTable ResultTable = null;
                string Result = string.Empty;
                switch (OperationType)
                {
                    #region MenuManager
                    case "DelMenu":
                        Result = MyBLL.DelMenus(MenuID).ToString();
                        return Result;
                    case "Menu":
                        ResultTable = MyBLL.QueryMenu(MenuID);
                        return ToTreeGrid(ResultTable);
                    case "MenuCombox":
                        ResultTable = MyBLL.QueryMenuCombox(ClassNO);
                        string boxjson = string.Empty;
                        boxjson = "[";
                        for (int i = 0; i < ResultTable.Rows.Count; i++)
                        {
                            boxjson += "{\"id\":\"" + ResultTable.Rows[i]["MenuID"].ToString() + "\",\"text\":\"" + ResultTable.Rows[i]["MenuName"].ToString() + "\"},";
                        }
                        if (boxjson.Length > 2)
                        {
                            boxjson = boxjson.Remove(boxjson.Length - 1);
                        }
                        boxjson += "]";
                        return boxjson;
                    case "ClassNOInfo":
                        string ClassNOjson = string.Empty;
                        ClassNOjson = "[";
                        for (int i = 1; i < 4; i++)
                        {
                            ClassNOjson += "{\"id\":" + i + ",\"text\":\"" + i + "级菜单\"},";
                        }
                        ClassNOjson = ClassNOjson.Remove(ClassNOjson.Length - 1);
                        ClassNOjson += "]";
                        return ClassNOjson;
                    case "SaveMenu":
                        UserAndPower.MenusModel MyModel = new MenusModel();
                        int menuIndex;
                        int.TryParse(MenuIndex, out menuIndex);
                        MyModel.MenuIndex = menuIndex;
                        MyModel.ParentMenuID = MenuParentID;
                        MyModel.MenuName = MenuName;
                        MyModel.MenuPath = MenuPath;
                        if (ClassNO != null && ClassNO != "")
                        {
                            MyModel.ClassNO = int.Parse(ClassNO);
                        }
                        MyModel.MenuDescription = MenuDescription;
                        MyModel.Remark = Remark;
                        bool IsSuccess = MyBLL.AddMenu(MyModel);
                        return IsSuccess.ToString();
                    case "EditMenu":
                        InternetDataMine.Models.UserAndPower.MenusModel MenuModel = new Models.UserAndPower.MenusModel();
                        int menuIndex1;
                        int.TryParse(MenuIndex, out menuIndex1);
                        MenuModel.MenuIndex = menuIndex1;
                        MenuModel.MenuID = MenuID;
                        MenuModel.ParentMenuID = MenuParentID;
                        MenuModel.MenuName = MenuName;
                        MenuModel.MenuDescription = MenuDescription;
                        MenuModel.MenuPath = MenuPath;
                        if (ClassNO != null && ClassNO != "")
                        {
                            MenuModel.ClassNO = int.Parse(ClassNO);
                        }
                        MenuModel.Remark = Remark;
                        Result = MyBLL.AlterMenu(MenuModel).ToString();
                        break;
                    #endregion

                    #region RoleManager
                    case "Role":
                        ResultTable = MyBLL.QueryRole(RoleID);
                        break;
                    case "RoleCombo":
                        DataTable RoleCombodt = MyBLL.QueryRole(RoleID);
                        string RoleJson = string.Empty;
                        RoleJson = "[";
                        for (int i = 0; i < RoleCombodt.Rows.Count; i++)
                        {
                            RoleJson += "{\"id\":\"" + RoleCombodt.Rows[i]["RoleID"].ToString() + "\",\"text\":\"" + RoleCombodt.Rows[i]["RoleName"].ToString() + "\"},";
                        }
                        if (RoleJson.Length > 2)
                        {
                            RoleJson = RoleJson.Remove(RoleJson.Length - 1);
                        }
                        RoleJson += "]";
                        return RoleJson;
                    case "AddRole":
                        UserAndPower.RoleModel MyRoleModel = new RoleModel();
                        MyRoleModel.RoleName = RoleName;
                        MyRoleModel.RoleDescription = RoleDescription;
                        MyRoleModel.Remark = Remark;
                        if (MyBLL.AddRole(MyRoleModel))
                        {
                            Result = "True";
                        }
                        else
                        {
                            Result = "False";
                        }
                        break;
                    case "EditRole":
                        InternetDataMine.Models.UserAndPower.RoleModel RoleModel = new Models.UserAndPower.RoleModel();
                        RoleModel.RoleID = RoleID;
                        RoleModel.RoleName = RoleName;
                        RoleModel.RoleDescription = RoleDescription;
                        RoleModel.Remark = Remark;
                        Result = MyBLL.AlterRole(RoleModel).ToString();
                        break;
                    case "DelRole":
                        Result = MyBLL.DelRole(RoleID).ToString();
                        break;
                    #endregion

                    #region UserGroupManager
                    case "UserGroup":
                        ResultTable = MyBLL.QueryUserGroup(GroupID);
                        break;
                    case "UserGroupCombo":
                        DataTable usergroupdt = MyBLL.QueryUserGroup(GroupID);
                        string usergroupjson = string.Empty;
                        usergroupjson = "[";
                        for (int i = 0; i < usergroupdt.Rows.Count; i++)
                        {
                            usergroupjson += "{\"id\":\"" + usergroupdt.Rows[i]["GroupID"].ToString() + "\",\"text\":\"" + usergroupdt.Rows[i]["GroupName"].ToString() + "\"},";
                        }
                        if (usergroupjson.Length > 2)
                        {
                            usergroupjson = usergroupjson.Remove(usergroupjson.Length - 1);
                        }
                        usergroupjson += "]";
                        return usergroupjson;
                    case "AddGroup":
                        UserAndPower.UserGroupModel MyGroupModel = new UserGroupModel();
                        MyGroupModel.RoleID = RoleID;
                        MyGroupModel.MineCode = MineCode;
                        MyGroupModel.GroupName = GroupName;
                        MyGroupModel.GroupDescription = GroupDescription;
                        MyGroupModel.Remark = Remark;
                        if (MyBLL.AddUserGroup(MyGroupModel))
                        {
                            Result = "添加成功！";
                        }
                        else
                        {
                            Result = "添加失败！";
                        }
                        break;
                    case "EditUserGroup":
                        InternetDataMine.Models.UserAndPower.UserGroupModel UserGroupModel = new Models.UserAndPower.UserGroupModel();
                        UserGroupModel.GroupID = GroupID;
                        UserGroupModel.RoleID = RoleID;
                        UserGroupModel.MineCode = MineCode;
                        UserGroupModel.GroupName = GroupName;
                        UserGroupModel.GroupDescription = GroupDescription;
                        UserGroupModel.Remark = Remark;
                        if (MyBLL.AlterUserGroup(UserGroupModel))
                            Result = "修改成功！";
                        else
                            Result = "修改失败！";
                        break;
                    case "DelUserGroup":
                        Result = MyBLL.DelUserGroup(Condition).ToString();
                        break;
                    #endregion

                    #region PowerManager
                    case "savePower":
                        string MyMenuID = string.Empty;
                        string[] MyMenuIDs = MenuIDs.Split(',');
                        if (RoleID != null && RoleID != "")
                        {
                            MyBLL.DelRolePower(RoleID);
                            for (int i = 0; i < MyMenuIDs.Count(); i++)
                            {
                                Result = MyBLL.AddRolePower(RoleID, MyMenuIDs[i], Remark).ToString();
                            }
                        }
                       
                        break;
                    case "PowerManagerRole":
                        string rolejson = string.Empty;
                        DataTable roledt = MyBLL.QueryRole(RoleID);
                        rolejson += "[";
                        for (int i = 0; i < roledt.Rows.Count; i++)
                        {
                            rolejson += "{";
                            rolejson += "\"id\":\"" + roledt.Rows[i]["RoleID"].ToString() + "\",\"text\":\"" + roledt.Rows[i]["RoleName"].ToString() + "\"";
                            rolejson += "},";
                        }
                        rolejson = rolejson.Remove(rolejson.Length - 1);
                        rolejson += "]";
                        return rolejson;
                    case "TreeTest":
                        string json = string.Empty;
                        //for (int i = 0; i < 4; i++)
                        //{
                        DataTable dt = MyBLL.QueryMenus(1);
                        DataTable Powerdt = MyBLL.QueryPowers(RoleID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                json += "{\"id\":\"" + dr["MenuID"] + "\",\"text\":\"" + dr["MenuName"].ToString() + "\"";
                                //if (IsSelect(Powerdt, dr["MenuID"].ToString())) json += ",\"checked\":\"true\"";
                                json += Return(MyBLL.QueryMenusByMenuParentID(dr["MenuID"].ToString()), Powerdt);
                                json += "},";
                            }
                            json = json.Remove(json.Length - 1);
                            json = "[" + json + "]";
                        }
                        //}
                        return json;
                    #endregion

                    #region UserManager
                    case "UserInfo":
                        ResultTable = MyBLL.QueryUser(UserID);
                        break;
                    case "AddUser":
                        string AddUserjson = string.Empty;
                        InternetDataMine.Models.UserAndPower.UserModel User = new Models.UserAndPower.UserModel();
                        Guid guid = Guid.NewGuid();
                        User.UserID = guid.ToString();
                        User.GroupID = GroupID;
                        User.UserName = UserName;
                        User.PassWord = ToolModel.GetMd5Hash(Password);
                        User.Remark = Remark;
                        AddUserjson = MyBLL.AddUser(User).ToString();
                        return AddUserjson;
                    case "DelUser":
                        return MyBLL.DelUser(UserID).ToString();
                    case "getpassword":
                        if (ToolModel.GetMd5Hash(Password) == oldpassword)
                        {
                            Result = "True";
                        }
                        else
                        {
                            Result = "False";
                        }
                        break;
                    case "EditUser":
                        InternetDataMine.Models.UserAndPower.UserModel EditUser = new Models.UserAndPower.UserModel();
                        EditUser.UserID = UserID;
                        EditUser.GroupID = GroupID;
                        EditUser.UserName = UserName;
                        EditUser.PassWord = ToolModel.GetMd5Hash(Password);
                        EditUser.Remark = Remark;
                        Result = MyBLL.EditUser(EditUser).ToString();
                        break;
                    #endregion

                    #region
                    case "MineCode":
                        ResultTable = MyBLL.QueryMineCode();
                        boxjson = "[";
                        for (int i = 0; i < ResultTable.Rows.Count; i++)
                        {
                            boxjson += "{\"id\":\"" + ResultTable.Rows[i]["MineCode"].ToString() + "\",\"text\":\"" + ResultTable.Rows[i]["SimpleName"].ToString() + "\"},";
                        }
                        if (boxjson.Length > 2)
                        {
                            boxjson = boxjson.Remove(boxjson.Length - 1);
                        }
                        boxjson += "]";
                        return boxjson;
                    case "MineCode1":
                        ResultTable = MyBLL.QueryMineCode();
                        boxjson = "[";
                        boxjson += "]";
                        return boxjson;
                    #endregion
                }
                int TotalRows = 0;
                if (ResultTable != null)
                {
                    TotalRows = ResultTable.Rows.Count;
                    Result = "{\"total\":" + TotalRows + ",\"rows\":" + JsonConvert.SerializeObject(ResultTable) + "}";
                }
                return Result;
            }
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string Return(DataTable dt, DataTable Powerdt)
        {
            string Result = string.Empty;
            if (dt != null && dt.Rows.Count > 0)//有子菜单
            {
                Result += ",\"children\":[";
                foreach (DataRow sondr in dt.Rows)
                {
                    Result += "{\"id\":\"" + sondr["MenuID"] + "\",\"text\":\"" + sondr["MenuName"] + "\"";
                    if (IsSelect(Powerdt, sondr["MenuID"].ToString()) && sondr["ClassNO"].ToString() == "3")
                        Result += ",\"checked\":\"true\"";
                    Result += Return(MyBLL.QueryMenusByMenuParentID(sondr["MenuID"].ToString()), Powerdt);
                    Result += "},";
                }
                Result = Result.Remove(Result.Length - 1);
                Result += "]";
            }
            return Result;
        }

        #region [ 返回TreeGrid的Json ]

        /// <summary>
        /// 返回TreeGrid的Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ToTreeGrid(DataTable dt)
        {
            string toJosn = "[";

            var drsClass1 = dt.Select("ClassNO =1");
            for (int i = 0; i < drsClass1.Length; i++)
            {
                if (i != 0) toJosn += ",";
                toJosn += "{";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != 0) toJosn += ",";

                    string jsonFormat = "\"{0}\":\"{1}\"";
                    if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
                    {
                        jsonFormat = "\"{0}\":{1}";
                    }
                    toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass1[i][j].ToString());
                }

                #region [class2]

                string parentMenuID = drsClass1[i]["MenuID"].ToString();
                var drsClass2 = dt.Select("MenuParentID='" + parentMenuID + "'");

                if (drsClass2.Length > 0)
                {
                    toJosn += " ,\"children\": [";

                    for (int child = 0; child < drsClass2.Length; child++)
                    {
                        if (child != 0) toJosn += ",";
                        toJosn += "{";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (j != 0) toJosn += ",";

                            string jsonFormat = "\"{0}\":\"{1}\"";
                            if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
                            {
                                jsonFormat = "\"{0}\":{1}";
                            }
                            toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass2[child][j].ToString());
                        }

                        #region [class3]
                        string parentID = drsClass2[child]["MenuID"].ToString();
                        var drsClass3 = dt.Select("MenuParentID='" + parentID + "'");
                        if (drsClass3.Length > 0)
                        {
                            toJosn += " ,\"children\": [";
                            for (int child1 = 0; child1 < drsClass3.Length; child1++)
                            {
                                if (child1 != 0) toJosn += ",";
                                toJosn += "{";
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (j != 0) toJosn += ",";

                                    string jsonFormat = "\"{0}\":\"{1}\"";
                                    if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
                                    {
                                        jsonFormat = "\"{0}\":{1}";
                                    }
                                    toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass3[child1][j].ToString());
                                }
                                toJosn += "}";

                            }
                            toJosn += "]";
                        }
                        #endregion

                        toJosn += "}";
                    }

                    toJosn += " ]";
                }

                #endregion

                toJosn += "}";
            }
            toJosn += "]";


            return toJosn;
        }

        

        #endregion

        //#region [ 返回TreeGrid的Json ]

        ///// <summary>
        ///// 返回TreeGrid的Json
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public string ToTreeGrid(DataTable dt)
        //{
        //   string toJosn = "[";

        //    var drsClass1 = dt.Select("ClassNO =1");
        //    for (int i = 0; i < drsClass1.Length; i++)
        //    {
        //        if (i != 0) toJosn += ",";
        //        toJosn += "{";
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            if (j != 0) toJosn += ",";

        //            string jsonFormat = "\"{0}\":\"{1}\"";
        //            if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
        //            {
        //                jsonFormat = "\"{0}\":{1}";
        //            }
        //            toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass1[i][j].ToString());
        //        }
        //        #region [class2]

        //        string parentMenuID = drsClass1[i]["MenuID"].ToString();
        //        var drsClass2 = dt.Select("MenuParentID='" + parentMenuID + "'");

        //        if (drsClass2.Length > 0)
        //        {
        //            toJosn += " ,\"children\": [";

        //            for (int child = 0; child < drsClass2.Length; child++)
        //            {
        //                if (child != 0) toJosn += ",";
        //                toJosn += "{";
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {
        //                    if (j != 0) toJosn += ",";

        //                    string jsonFormat = "\"{0}\":\"{1}\"";
        //                    if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
        //                    {
        //                        jsonFormat = "\"{0}\":{1}";
        //                    }
        //                    toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass2[child][j].ToString());
        //                }

        //                #region [class3]
        //                string parentID = drsClass2[child]["MenuID"].ToString();
        //                var drsClass3 = dt.Select("MenuParentID='" + parentID + "'");
        //                if (drsClass3.Length > 0)
        //                {
        //                    toJosn += " ,\"children\": [";
        //                    for (int child1 = 0; child1 < drsClass3.Length; child1++)
        //                    {
        //                        if (child1 != 0) toJosn += ",";
        //                        toJosn += "{";
        //                        for (int j = 0; j < dt.Columns.Count; j++)
        //                        {
        //                            if (j != 0) toJosn += ",";

        //                            string jsonFormat = "\"{0}\":\"{1}\"";
        //                            if (dt.Columns[j].DataType == typeof(int) && !(dt.Rows[i][j] is DBNull))
        //                            {
        //                                jsonFormat = "\"{0}\":{1}";
        //                            }
        //                            toJosn += string.Format(jsonFormat, dt.Columns[j].ColumnName, drsClass3[child1][j].ToString());
        //                        }
        //                        toJosn += "}";

        //                    }
        //                    toJosn += "]";
        //                }
        //                #endregion

        //                toJosn += "}";
        //            }

        //            toJosn += " ]";
        //        }

        //        #endregion
        //        toJosn += "}";
        //    }
        //    toJosn += "]";


        //    return toJosn;
        //}

        //#endregion

        public bool IsSelect(DataTable dt, string MenuID)
        {
            bool Result = false;
            if (dt == null) return Result;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MenuID"].ToString() == MenuID)
                {
                    Result = true;
                    return Result;
                }
            }
            return Result;
        }

    }
}