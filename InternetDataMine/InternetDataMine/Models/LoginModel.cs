using System.Data;
using InternetDataMine.Models.DataService;
using System.Net;
using System.IO;
using System.Text;
namespace InternetDataMine.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName {get;set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 登录验证成功后，返回对应的权限
        /// </summary>
        public string MineCode { get; set; }
        /// <summary>
        /// 获取的菜单
        /// </summary>
        public string ReturnMenus { get; set; }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public string Login()
        {

            DataBLL bll = new DataBLL();
            string secrityPassword = ToolModel.GetMd5Hash(PassWord);
            DataTable dt = bll.getUserInfo_IsExists(UserName, secrityPassword);
            if (dt.Rows.Count > 0)
            {
                dt = bll.getUserInfo_IsPassword(UserName, secrityPassword);
                if (dt.Rows.Count > 0)
                {
                    return "[\"正确\",{\"message\":[{\"userID\":\""+dt.Rows[0]["UserID"].ToString()+"\",\"userName\":\"" + dt.Rows[0]["UserName"].ToString() + "\",\"mineCode\":\"" + dt.Rows[0]["MineCode"].ToString() + "\"}]}]";
                }
                else
                {
                    return "[\"错误\",{\"message\":\"输入密码有误，请重新填写！\"}]";
                }
            }
            else
            {
                return "[\"错误\",{\"message\":\"用户名不存在，请重新填写！\"}]";
            }
        }

        public void LoadMenuDynamic(string UserName, string MineCode)
        {
            DataBLL bll = new DataBLL();
            ReturnMenus = bll.getUserMenu(UserName, MineCode);
        }


        public  string GetWeather(string url, string encode)
        {
            string HtmlCode = "";
            HttpWebRequest wrequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse wresponse = (HttpWebResponse)wrequest.GetResponse();
                Stream stream = wresponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(encode));
                HtmlCode = reader.ReadToEnd();
                reader.Close();
                wresponse.Close();
            }
            catch
            {
                HtmlCode = "Error";
            }
            return HtmlCode;
        }
    }
}