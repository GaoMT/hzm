using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using InternetDataMine.Models.DataService;
namespace InternetDataMine.Controllers
{
    public class NoticeController : Controller
    {
        //
        // GET: /Notice/
        DataDAL dal = new DataDAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NoticeManage()
        {
            return View();
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="File_ID">GUID</param>
        /// <param name="Disk_ID">目录名</param>
        /// <param name="FileUpName">上传的文件名</param>
        /// <param name="FileSavePath">服务端路径</param>
        /// <param name="FileSaveName">服务端文件名</param>
        /// <param name="FileSize">文件大小</param>
        /// <param name="UploadUser">上传人</param>
        /// <returns></returns>
        private bool SaveFile(string File_ID, string Disk_ID, string FileUpName, string FileSavePath, string FileSaveName, int FileSize, string UploadUser)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[FileManage]([File_ID],[Disk_ID],[FileUpName],[FileSavePath],[FileSaveName]
           ,[FileSize],[UploadUser]) VALUES ('{0}','{1}','{2}','{3}','{4}',{5},'{6}')", File_ID, Disk_ID, FileUpName, FileSavePath, FileSaveName, FileSize, UploadUser);
            return dal.ExcuteSql(sql);
        }

        /// <summary>
        /// 公告文件下载
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="FileName">文件名称</param>
        public void FileDownLoad(string FilePath, string FileName)
        {
            //string filePath = MapPathFile(FileName);
            //指定块大小
            long chunkSize = 204800;
            //建立一个200K的缓冲区
            byte[] buffer = new byte[chunkSize];
            //已读的字节数
            long dataToRead = 0;
            FileStream stream = null;
            try
            {


                //打开文件
                stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = stream.Length;
                //添加Http头
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(FileName));
                Response.AddHeader("Content-Length", dataToRead.ToString());
                while (dataToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();
                        //HttpContext.Current.Response.Clear();
                        buffer = new Byte[chunkSize];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //防止client失去连接
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {

                //HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                Response.Close();
            }


            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/octet-stream";
            //Response.ContentEncoding = Encoding.UTF8;
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8));

            //Response.WriteFile(FilePath);

            //Response.End();
        }

        #region [ 写公告 ]

        /// <summary>
        /// 写公告
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="title">标题</param>
        /// <param name="contents">内容</param>
        public void NoticeFileUpLoad(string username, string title, string contents)
        {
            HttpFileCollectionBase files = Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string error = string.Empty;
            string imgurl;
            string Notice_ID = Guid.NewGuid().ToString();
            // 出入公告数据
            string sql =
                string.Format(
                    @"INSERT INTO [dbo].[NoticeManage]([Notice_ID],[Notice_Title],[Contents],[CreateUser],[CreateTime])
     VALUES ('{0}','{1}','{2}','{3}',GETDATE())", Notice_ID, title, contents, username);
            dal.ExcuteSql(sql);

            if (files.Count > 0)
            {
                if (files[0].FileName == "")
                {
                    //string res = "{ error:'没有选择文件', msg:'请选择文件',imgurl:''}";
                    //Response.Write(res);
                    //Response.End();
                }
                else
                {
                    string directory = Server.MapPath("Notice\\" + DateTime.Now.ToString("yyyyMM"));
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    string FileSaveName = Guid.NewGuid().ToString();
                    string FileUpName = Path.GetFileName(files[0].FileName);
                    string FileSavePath = directory + "\\" + FileSaveName;
                    //Path.GetFileName(Guid.NewGuid() + "." + files[0].FileName.Split(new char[] {'.'})[1]);

                    files[0].SaveAs(FileSavePath);
                    msg = " 成功! 文件大小为:" + files[0].ContentLength;
                    imgurl = "/" + files[0].FileName;

                    // 插入数据库
                    SaveFile(FileSaveName, Notice_ID, FileUpName, FileSavePath, FileSaveName, files[0].ContentLength,
                             username);

                    string res = "{ error:'" + error + "', msg:'" + msg + "',imgurl:'" + imgurl + "'}";
                    Response.Write(res);
                    Response.End();
                }
            }
        }

        #endregion
    }
}
