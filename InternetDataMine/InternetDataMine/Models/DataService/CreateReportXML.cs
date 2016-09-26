using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace InternetDataMine.Models.DataService
{
    public class CreateReportXML
    {
        #region【属性】

        private string _title = "";
        /// <summary>
        /// 报表标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private bool _addReserved = true;
        /// <summary>
        /// 是否预留第一列，建议预留
        /// </summary>
        public bool AddReserved
        {
            get { return _addReserved; }
            set { _addReserved = value; }
        }

        private bool _showID = true;
        /// <summary>
        /// 结果是否显示序号列，默认显示
        /// </summary>
        public bool ShowID
        {
            get { return _showID; }
            set { _showID = value; }
        }


        private string _savePath;
        /// <summary>
        /// XML保存路径，包含完整的文件名称
        /// </summary>
        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        private int _pageLeft = 19;
        /// <summary>
        /// 左边距，默认为19
        /// </summary>
        public int PageLeft
        {
            get { return _pageLeft; }
            set { _pageLeft = value; }
        }
        private int _pageTop = 25;
        /// <summary>
        /// 上边距，默认为25
        /// </summary>
        public int PageTop
        {
            get { return _pageTop; }
            set { _pageTop = value; }
        }
        private int _pageRight = 19;
        /// <summary>
        /// 右边距，默认与左边距相同
        /// </summary>
        public int PageRight
        {
            get { return _pageRight; }
            set { _pageRight = value; }
        }
        private int _pageBottom = 25;
        /// <summary>
        /// 下边距，默认与上边距相同
        /// </summary>
        public int PageBottom
        {
            get { return _pageBottom; }
            set { _pageBottom = value; }
        }

        #endregion

        /// <summary>
        /// 报表标题
        /// </summary>
        /// <param name="title">报表标题</param>
        /// <param name="savePath">包含xml的文件名及后缀的完整保存路径</param>
        public CreateReportXML(string title,string savePath)
        {
            _title = title;
            _savePath = savePath;
        }

        /// <summary>
        /// 生成报表样式
        /// </summary>
        /// <param name="columns">报表中显示的列，有name、text、type 列，name:绑定的数据集中的列名；text:报表中显示的列名；type:数据集中列的数据类型 </param>
        /// <param name="merger">报表中需要合并相同内容的从0开始的列序号，多个列以“,”隔开</param>
        /// <returns>true 保存成功,false 保存失败 </returns>
        public bool SaveXML(DataTable columns,string merger)
        {
            try
            {
                #region[获取报表列信息]
                if (_savePath == "" || columns == null)
                {
                    return false;
                }
                DataTable dscolumn = new DataTable();
                ReportDataDAL _repdall = new ReportDataDAL();
                #endregion

                #region[局部变量]
                int columncount = columns.Rows.Count;
                //当前报表中已经处理的行数，仅供行编号使用
                int execount = 0;
                //临时列编号
                int colid = 0;
                //数据表格部分起始编号
                int startid = 0;
                if (_addReserved)
                {
                    startid = 1;
                }
                #endregion

                #region[创建XML头]
                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                #endregion

                #region[Report]
                //创建一个根节点（一级）
                XmlElement Report = doc.CreateElement("Report");
                doc.AppendChild(Report);
                #endregion

                #region[WorkSheet]
                //创建节点（二级）
                XmlElement WorkSheet = doc.CreateElement("WorkSheet");
                WorkSheet.SetAttribute("name", merger);//将需要合并的列存入工作的name中
                WorkSheet.SetAttribute("isDefaultPrint", "true");
                Report.AppendChild(WorkSheet);

                #region[Properties]
                //创建节点（三级）
                XmlElement Properties = doc.CreateElement("Properties");
                WorkSheet.AppendChild(Properties);
                #endregion

                #region[BackGround]
                XmlElement BackGround = doc.CreateElement("BackGround");
                BackGround.SetAttribute("bgColor", "#FFFFFF");
                BackGround.SetAttribute("arange", "tile");
                BackGround.SetAttribute("alpha", "255");
                Properties.AppendChild(BackGround);
                #endregion

                #region[DefaultTD]
                XmlElement DefaultTD = doc.CreateElement("DefaultTD");
                XmlElement TD = doc.CreateElement("TD");
                TD.SetAttribute("fontIndex", "1");
                TD.SetAttribute("textColor", "#000000");
                TD.SetAttribute("transparent", "true");
                TD.SetAttribute("leftBorder", "1");
                TD.SetAttribute("topBorder", "1");
                TD.SetAttribute("leftBorderColor", "#808080");
                TD.SetAttribute("leftBorderStyle", "solid");
                TD.SetAttribute("topBorderColor", "#808080");
                TD.SetAttribute("topBorderStyle", "solid");
                TD.SetAttribute("decimal", "2");
                TD.SetAttribute("align", "center");
                TD.SetAttribute("vAlign", "middle");
                TD.SetAttribute("isProtected", "false");
                TD.SetAttribute("isThousandSeparat", "true");
                TD.SetAttribute("isRound", "true");
                TD.SetAttribute("isPrint", "true");
                TD.SetAttribute("font-size", "9");
                DefaultTD.AppendChild(TD);
                Properties.AppendChild(DefaultTD);
                #endregion

                #region[Other]
                XmlElement Other = doc.CreateElement("Other");
                Other.SetAttribute("isShowZero", "true");
                Other.SetAttribute("isRefOriPrecision", "true");
                Other.SetAttribute("LineDistance", "0");
                Other.SetAttribute("isRowHeightAutoExtendAble", "true");
                Properties.AppendChild(Other);
                #endregion

                #region[Fonts]
                XmlElement Fonts = doc.CreateElement("Fonts");
                XmlElement Font = doc.CreateElement("Font");
                Font.SetAttribute("faceName", "Verdana");
                Font.SetAttribute("height", "-10");
                Font.SetAttribute("weight", "700");
                Fonts.AppendChild(Font);

                Font = doc.CreateElement("Font");
                Font.SetAttribute("faceName", "Verdana");
                Font.SetAttribute("height", "-13");
                Font.SetAttribute("weight", "400");
                Fonts.AppendChild(Font);

                Font = doc.CreateElement("Font");
                Font.SetAttribute("faceName", "楷体");
                Font.SetAttribute("charSet", "134");
                Font.SetAttribute("height", "-27");
                Font.SetAttribute("weight", "700");
                Fonts.AppendChild(Font);

                Font = doc.CreateElement("Font");
                Font.SetAttribute("faceName", "Verdana");
                Font.SetAttribute("height", "-9");
                Font.SetAttribute("weight", "400");
                Fonts.AppendChild(Font);
                WorkSheet.AppendChild(Fonts);
                #endregion

                #region[Table]
                XmlElement Table = doc.CreateElement("Table");
                WorkSheet.AppendChild(Table);

                #region[全局列宽]
                XmlElement Public_Col;
                if (_addReserved)
                {
                    //前预制列
                    Public_Col = doc.CreateElement("Col");
                    Public_Col.SetAttribute("width", "15");
                    Table.AppendChild(Public_Col);
                }
                int tmp_width = 70;
                #region[动态数据列]
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    Public_Col = doc.CreateElement("Col");
                    Public_Col.SetAttribute("width", tmp_width.ToString());
                    Table.AppendChild(Public_Col);
                }
                #endregion
                //后预制列
                Public_Col = doc.CreateElement("Col");
                Public_Col.SetAttribute("width", "15");
                Table.AppendChild(Public_Col);
                #endregion

                #region[行集合]

                #region[标题行]
                XmlElement Title_TR = doc.CreateElement("TR");
                Title_TR.SetAttribute("height", "45");
                Title_TR.SetAttribute("sequence", "0");
                Table.AppendChild(Title_TR);
                execount++;
                XmlElement Title_TD;
                colid = 0;
                if (_addReserved)
                {
                    //前预制列
                    Title_TD = doc.CreateElement("TD");
                    Title_TD.SetAttribute("col", colid.ToString());
                    Title_TD.SetAttribute("topBorder", "0");
                    Title_TD.SetAttribute("leftBorder", "0");
                    Title_TR.AppendChild(Title_TD);
                    colid++;
                }
                #region[动态生成列]
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    Title_TD = doc.CreateElement("TD");
                    Title_TD.SetAttribute("col", colid.ToString());
                    Title_TD.SetAttribute("fontIndex", "2");
                    Title_TD.SetAttribute("topBorder", "0");
                    Title_TD.SetAttribute("leftBorder", "0");
                    Title_TD.SetAttribute("align", "center");
                    colid++;
                    if (i == 0)
                    {
                        Title_TD.InnerText = _title;
                        Title_TD.SetAttribute("alias", "Title");
                    }
                    Title_TR.AppendChild(Title_TD);
                }
                #endregion
                //后预制列
                Title_TD = doc.CreateElement("TD");
                Title_TD.SetAttribute("col", colid.ToString());
                Title_TD.SetAttribute("topBorder", "0");
                Title_TD.SetAttribute("leftBorder", "0");
                Title_TR.AppendChild(Title_TD);

                #endregion

                #region[打印时间行]
                XmlElement PrintTime_TR = doc.CreateElement("TR");
                PrintTime_TR.SetAttribute("height", "20");
                PrintTime_TR.SetAttribute("sequence", "0");
                Table.AppendChild(PrintTime_TR);
                execount++;
                //前预制列
                XmlElement PrintTime_TD;
                colid = 0;
                if (_addReserved)
                {
                    PrintTime_TD = doc.CreateElement("TD");
                    PrintTime_TD.SetAttribute("col", colid.ToString());
                    PrintTime_TD.SetAttribute("topBorder", "0");
                    PrintTime_TD.SetAttribute("leftBorder", "0");
                    PrintTime_TR.AppendChild(PrintTime_TD);
                    colid++;
                }
                #region[动态生成列]
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    PrintTime_TD = doc.CreateElement("TD");
                    PrintTime_TD.SetAttribute("col", colid.ToString());
                    PrintTime_TD.SetAttribute("fontIndex", "1");
                    PrintTime_TD.SetAttribute("topBorder", "0");
                    PrintTime_TD.SetAttribute("leftBorder", "0");
                    colid++;
                    if (i == 0)
                    {
                        PrintTime_TD.SetAttribute("align", "left");
                        PrintTime_TD.SetAttribute("tabOrder", "1");
                        PrintTime_TD.SetAttribute("datatype", "1");
                        //PrintTime_TD.SetAttribute("maskid", "1");
                        //PrintTime_TD.SetAttribute("tip", "选择数据日期,可以查询");
                        PrintTime_TD.SetAttribute("alias", "BeginDate");
                    }
                    else if (i == (columns.Rows.Count - 4))
                    {
                        PrintTime_TD.SetAttribute("tabOrder", "1");
                        PrintTime_TD.SetAttribute("datatype", "1");
                        PrintTime_TD.SetAttribute("align", "right");
                        PrintTime_TD.SetAttribute("alias", "PrintDate");
                        PrintTime_TD.SetAttribute("formula", "=formatDate(now(),'打印时间:YYYY-MM-dd HH:mm:ss')");
                    }
                    PrintTime_TR.AppendChild(PrintTime_TD);
                }
                #endregion
                //后预制列
                PrintTime_TD = doc.CreateElement("TD");
                PrintTime_TD.SetAttribute("col", colid.ToString());
                PrintTime_TD.SetAttribute("topBorder", "0");
                PrintTime_TD.SetAttribute("leftBorder", "0");
                PrintTime_TR.AppendChild(PrintTime_TD);
                #endregion

                #region[字段行]
                XmlElement Column_TR = doc.CreateElement("TR");
                Column_TR.SetAttribute("height", "30");
                Column_TR.SetAttribute("sequence", "0");
                Table.AppendChild(Column_TR);
                execount++;
                //前预制列
                XmlElement Column_TD;
                colid = 0;
                if (_addReserved)
                {
                    Column_TD = doc.CreateElement("TD");
                    Column_TD.SetAttribute("col", colid.ToString());
                    Column_TD.SetAttribute("topBorder", "0");
                    Column_TD.SetAttribute("leftBorder", "0");
                    Column_TR.AppendChild(Column_TD);
                    colid++;
                }
                #region[动态生成列]
                for (int j = 0; j < columns.Rows.Count; j++)
                {
                    Column_TD = doc.CreateElement("TD");
                    Column_TD.SetAttribute("col", colid.ToString());
                    Column_TD.SetAttribute("fontIndex", "1");
                    Column_TD.SetAttribute("align", "center");
                    Column_TD.SetAttribute("decimal", "2");
                    Column_TD.InnerText = columns.Rows[j]["text"].ToString();
                    if (j == 0)
                    {
                        Column_TD.SetAttribute("formula", "=headrow('ds1')");
                        Column_TR.AppendChild(Column_TD);
                    }
                    else
                    {
                        Column_TR.AppendChild(Column_TD);
                    }
                    colid++;
                }
                #endregion
                //后预制列
                Column_TD = doc.CreateElement("TD");
                Column_TD.SetAttribute("col", colid.ToString());
                Column_TD.SetAttribute("topBorder", "0");
                Column_TD.SetAttribute("leftBorder", "1");
                Column_TR.AppendChild(Column_TD);
                #endregion

                #region[数据行]
                XmlElement Data_TR = doc.CreateElement("TR");
                Data_TR.SetAttribute("height", "30");
                Data_TR.SetAttribute("sequence", "0");
                Table.AppendChild(Data_TR);
                execount++;
                XmlElement Data_TD;
                colid = 0;
                if (_addReserved)
                {
                    //前预制列
                    Data_TD = doc.CreateElement("TD");
                    Data_TD.SetAttribute("col", colid.ToString());
                    Data_TD.SetAttribute("topBorder", "0");
                    Data_TD.SetAttribute("leftBorder", "0");
                    Data_TR.AppendChild(Data_TD);
                    colid++;
                }
                #region[动态生成列]
                for (int j = 0; j < columns.Rows.Count; j++)
                {
                    Data_TD = doc.CreateElement("TD");
                    Data_TD.SetAttribute("col", colid.ToString());
                    Data_TD.SetAttribute("fontIndex", "1");
                    Data_TD.SetAttribute("tabOrder", "1");
                    //Data_TD.SetAttribute("datatype", "1");
                    Data_TD.SetAttribute("decimal", "2");
                    //Data_TD.SetAttribute("isRound", "true");
                    Data_TD.SetAttribute("align", "center");
                    //Data_TD.SetAttribute("tip",columns[j].ColumnName);
                    if (j == 0)
                    {
                        Data_TD.SetAttribute("formula", "=datarow('ds1')");
                    }
                    Data_TR.AppendChild(Data_TD);
                    colid++;
                }
                #endregion
                //后预制列
                Data_TD = doc.CreateElement("TD");
                Data_TD.SetAttribute("col", colid.ToString());
                Data_TD.SetAttribute("topBorder", "0");
                Data_TD.SetAttribute("leftBorder", "1");
                Data_TR.AppendChild(Data_TD);
                #endregion

                #region[签字行]
                XmlElement Signature_TR = doc.CreateElement("TR");
                Signature_TR.SetAttribute("height", "30");
                Signature_TR.SetAttribute("sequence", "0");
                Table.AppendChild(Signature_TR);
                //execount++;//签字行后可不累计，因为下文中不再使用
                XmlElement Signature_TD;
                colid = 0;
                if (_addReserved)
                {
                    //前预制列
                    Signature_TD = doc.CreateElement("TD");
                    //Signature_TD.SetAttribute("col", colid.ToString());
                    Signature_TD.SetAttribute("topBorder", "0");
                    Signature_TD.SetAttribute("leftBorder", "0");
                    Signature_TR.AppendChild(Signature_TD);
                    colid++;
                }
                #region[动态生成列]
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    Signature_TD = doc.CreateElement("TD");
                    //Signature_TD.SetAttribute("col", colid.ToString());
                    Signature_TD.SetAttribute("fontIndex", "1");
                    Signature_TD.SetAttribute("topBorder", "1");
                    Signature_TD.SetAttribute("leftBorder", "0");
                    //Signature_TD.SetAttribute("datatype", "1");
                    //Signature_TD.SetAttribute("tabOrder", "1");
                    Signature_TD.SetAttribute("align", "left");
                    colid++;
                    Signature_TR.AppendChild(Signature_TD);
                }
                #endregion
                //后预制列
                Signature_TD = doc.CreateElement("TD");
                //Signature_TD.SetAttribute("col", colid.ToString());
                Signature_TD.SetAttribute("topBorder", "0");
                Signature_TD.SetAttribute("leftBorder", "0");
                Signature_TR.AppendChild(Signature_TD);
                #endregion

                #endregion

                #endregion

                #region[Merges]
                XmlElement Merges = doc.CreateElement("Merges");
                WorkSheet.AppendChild(Merges);
                #region[标题行合并]
                XmlElement Range = doc.CreateElement("Range");
                Range.SetAttribute("row1", "0");
                Range.SetAttribute("col1", startid.ToString());
                Range.SetAttribute("row2", "0");
                Range.SetAttribute("col2", (colid - 1).ToString());
                Merges.AppendChild(Range);
                #endregion

                #region[打印时间行合并]

                #region[合并时间显示单元格]
                Range = doc.CreateElement("Range");
                Range.SetAttribute("row1", "1");
                Range.SetAttribute("col1", startid.ToString());
                Range.SetAttribute("row2", "1");
                Range.SetAttribute("col2", (startid +2).ToString());
                Merges.AppendChild(Range);
                #endregion

                #region[合并打印时间单元格]
                Range = doc.CreateElement("Range");
                Range.SetAttribute("row1", "1");
                Range.SetAttribute("col1", (columns.Rows.Count + startid - 4).ToString());
                Range.SetAttribute("row2", "1");
                Range.SetAttribute("col2", (columns.Rows.Count + startid - 1).ToString());
                Merges.AppendChild(Range);
                #endregion
                #endregion

                #region[签字行合并]
                //Range = doc.CreateElement("Range");
                //Range.SetAttribute("row1", execount.ToString());
                //Range.SetAttribute("col1", startid.ToString());
                //Range.SetAttribute("row2", execount.ToString());
                //Range.SetAttribute("col2", (columns.Count - startid).ToString());
                //Merges.AppendChild(Range);
                #endregion

                #endregion

                #region[PrintPage]
                XmlElement PrintPage = doc.CreateElement("PrintPage");
                WorkSheet.AppendChild(PrintPage);

                XmlElement Paper = doc.CreateElement("Paper");
                PrintPage.AppendChild(Paper);

                XmlElement Margin = doc.CreateElement("Margin");
                Other.SetAttribute("left", _pageLeft.ToString());
                Other.SetAttribute("top", _pageTop.ToString());
                Other.SetAttribute("right", _pageRight.ToString());
                Other.SetAttribute("bottom", _pageBottom.ToString());
                Paper.AppendChild(Margin);

                XmlElement Page = doc.CreateElement("Page");
                Page.SetAttribute("align", "center");
                PrintPage.AppendChild(Page);

                XmlElement PageCode = doc.CreateElement("PageCode");
                Page.AppendChild(PageCode);

                XmlElement Page_Font = doc.CreateElement("Font");
                Page_Font.SetAttribute("faceName", "宋体");
                Page_Font.SetAttribute("charSet", "134");
                Page_Font.SetAttribute("height", "-14");
                Page_Font.SetAttribute("weight", "400");
                PageCode.AppendChild(Page_Font);
                #endregion

                #endregion

                #region[DataSources]
                XmlElement DataSources = doc.CreateElement("DataSources");
                DataSources.SetAttribute("Version", "255");
                DataSources.SetAttribute("isAutoCalculateWhenOpen", "false");
                DataSources.SetAttribute("isSaveCalculateResult", "false");
                Report.AppendChild(DataSources);

                #region[DataSource]
                XmlElement DataSource = doc.CreateElement("DataSource");
                DataSource.SetAttribute("type", "4");
                DataSources.AppendChild(DataSource);

                #region[Data]
                XmlElement DS_Data = doc.CreateElement("Data");
                DataSource.AppendChild(DS_Data);

                #region[其他]
                XmlElement DS_ID = doc.CreateElement("ID");
                DS_ID.InnerText = "ds1";
                DS_Data.AppendChild(DS_ID);

                XmlElement DS_Version = doc.CreateElement("Version");
                DS_Version.InnerText = "2";
                DS_Data.AppendChild(DS_Version);

                XmlElement DS_Type = doc.CreateElement("Type");
                DS_Type.InnerText = "4";
                DS_Data.AppendChild(DS_Type);

                XmlElement DS_TypeMeaning = doc.CreateElement("TypeMeaning");
                DS_TypeMeaning.InnerText = "JSON";
                DS_Data.AppendChild(DS_TypeMeaning);

                XmlElement DS_Source = doc.CreateElement("Source");
                DS_Data.AppendChild(DS_Source);

                XmlElement DS_XML_RecordAble_Nodes = doc.CreateElement("XML_RecordAble_Nodes");
                DS_Data.AppendChild(DS_XML_RecordAble_Nodes);
                XmlElement DS_Node = doc.CreateElement("Node");
                DS_XML_RecordAble_Nodes.AppendChild(DS_Node);
                XmlElement DS_name = doc.CreateElement("name");
                DS_Node.AppendChild(DS_name);
                #endregion

                #region[DS_Columns]
                XmlElement DS_Columns = doc.CreateElement("Columns");
                DS_Data.AppendChild(DS_Columns);

                #region[动态生成]
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    XmlElement DS_Column = doc.CreateElement("Column");
                    DS_Columns.AppendChild(DS_Column);

                    XmlElement Column_name = doc.CreateElement("name");
                    Column_name.InnerText = columns.Rows[i]["name"].ToString();
                    DS_Column.AppendChild(Column_name);

                    XmlElement Column_text = doc.CreateElement("text");
                    Column_text.InnerText = columns.Rows[i]["text"].ToString();
                    DS_Column.AppendChild(Column_text);

                    XmlElement Column_type = doc.CreateElement("type");
                    Column_type.InnerText = columns.Rows[i]["type"].ToString();
                    DS_Column.AppendChild(Column_type);

                    XmlElement Column_visible = doc.CreateElement("visible");
                    Column_visible.InnerText = "true";
                    DS_Column.AppendChild(Column_visible);

                    XmlElement Column_sequence = doc.CreateElement("sequence");
                    Column_sequence.InnerText = (i + 1).ToString();
                    DS_Column.AppendChild(Column_sequence);
                }

                #endregion

                #endregion

                #endregion

                #endregion

                #endregion

                #region[保存XML到文件]
                if (_savePath != "" && _savePath != null)
                {
                    doc.Save(_savePath);
                }
                return true;
                #endregion

            }
            catch
            {
                return false;
            }

        }
    }
}
