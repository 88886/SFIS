using System;
using System.Data;
using System.Reflection;
using System.IO;
using Microsoft.Office.Core;
using  Excel;
//using System.Collections.Generic;
//using System.Windows.Forms;
//using System.Data.OleDb;

namespace FrmBLL
{
    public partial class ClsAllExcel
    {
        #region Variables
        private Excel.Application xlsApp = null;
        private Excel.Workbooks xlsWbs = null;
        private Excel.Workbook xlsWb = null;
        private Excel.Worksheet xlsWs = null;
        private Excel.Range xlsRg = null;
        private Excel.Range xlsCSRg = null;
        private int excelActiveWorkSheetIndex;          
        private string xlsOpenFileName = "";     
        private string xlsSaveFileName = "";      
        #endregion
        #region Properties
        public int ActiveSheetIndex
        {
            get
            {
                return excelActiveWorkSheetIndex;
            }
            set
            {
                excelActiveWorkSheetIndex = value;
            }
        }
        public string OpenFileName
        {
            get
            {
                return xlsOpenFileName;
            }
            set
            {
                xlsOpenFileName = value;
            }
        }
        public string SaveFileName
        {
            get
            {
                return xlsSaveFileName;
            }
            set
            {
                xlsSaveFileName = value;
            }
        }
        #endregion

        public ClsAllExcel()
        {
            xlsApp = null;
            xlsWbs = null;
            xlsWb = null;
            xlsWs = null;
            ActiveSheetIndex = 1;     
        }
        /// <summary>
        /// 以excelOpenFileName为模板新建Excel文件
        /// </summary>
        public bool OpenExcelFile()
        {
            if (xlsApp != null) 
            //检查文件是否存在
            if (xlsOpenFileName == "")
            {
                throw new Exception("请选择文件!");
            }
            if (!File.Exists(xlsOpenFileName))
            {
                throw new Exception(xlsOpenFileName + "该文件不存在!");
            }
            try
            {
                xlsApp = new Excel.ApplicationClass();
                xlsWbs = xlsApp.Workbooks;
                xlsWb = ((Excel.Workbook)xlsWbs.Open(xlsOpenFileName, Missing.Value, Missing.Value, 
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, 
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                xlsWs = (Excel.Worksheet)xlsWb.Worksheets[excelActiveWorkSheetIndex];

                xlsApp.Visible = false;
                return true;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception("(1)没有安装Excel 2003;\n(2)或没有安装Excel 2003 .NET 可编程性支持;\n详细信息:\n"+ e.Message);
            }
        }

        public bool CloseExcelFile()
        {
            try
            {
                if (xlsApp != null)
                {
                    xlsWb.Save();
                    xlsApp.ActiveWorkbook.Save();
                    this.CloseExcelApplication();
                }
                return true;
            }
            catch
            {
                  this.CloseExcelApplication();
                  return false;
            }
        }
        /// <summary>
        /// 读取一个Cell的值
        /// </summary>
        /// <param name="CellRowID">要读取的Cell的行索引</param>
        /// <param name="CellColumnID">要读取的Cell的列索引</param>
        /// <returns>Cell的值</returns>
        public string getOneCellValue(int CellRowID, int CellColumnID)
        {
            if (CellRowID <= 0)
            {
                throw new Exception("行索引超出范围！");
            }
            string sValue = "";
            try
            {
                sValue = ((Excel.Range)xlsWs.Cells[CellRowID, CellColumnID]).Text.ToString();
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
            return (sValue);
        }

        /// <summary>
        /// 读取一个连续区域的Cell的值(矩形区域，包含一行或一列,或多行，多列)，返回一个一维字符串数组。
        /// </summary>
        /// <param name="StartCell">StartCell是要写入区域的左上角单元格</param>
        /// <param name="EndCell">EndCell是要写入区域的右下角单元格</param>
        /// <returns>值的集合</returns>
        public string[] getCellsValue(string StartCell, string EndCell)
        {
            string[] sValue = null;
            xlsRg = (Excel.Range)xlsWs.get_Range(StartCell, EndCell);
            sValue = new string[xlsRg.Count];
            int rowStartIndex = ((Excel.Range)xlsWs.get_Range(StartCell, StartCell)).Row;      //起始行号
            int columnStartIndex = ((Excel.Range)xlsWs.get_Range(StartCell, StartCell)).Column;    //起始列号
            int rowNum = xlsRg.Rows.Count;                 //行数目
            int columnNum = xlsRg.Columns.Count;               //列数目
            int index = 0;
            for (int i = rowStartIndex; i < rowStartIndex + rowNum; i++)
            {
                for (int j = columnStartIndex; j < columnNum + columnStartIndex; j++)
                {
                    //读到空值null和读到空串""分别处理
                    sValue[index] = ((Excel.Range)xlsWs.Cells[i, j]).Text.ToString();
                    index++;
                }
            }
            return (sValue);
        }
        /// <summary>
        /// 读取所有单元格的数据(矩形区域)，返回一个datatable.假设所有单元格靠工作表左上区域。
        /// </summary>
        public System.Data.DataTable getAllCellsValue()
        {
            int columnCount = getTotalColumnCount();
            int rowCount = getTotalRowCount();
            System.Data.DataTable dt = new System.Data.DataTable();
            //设置datatable列的名称
            for (int columnID = 1; columnID <= columnCount; columnID++)
            {
                dt.Columns.Add(((Excel.Range)xlsWs.Cells[1, columnID]).Text.ToString());
            }
            for (int rowID = 2; rowID <= rowCount; rowID++)
            {
                DataRow dr = dt.NewRow();
                for (int columnID = 1; columnID <= columnCount; columnID++)
                {
                    dr[columnID - 1] = ((Excel.Range)xlsWs.Cells[rowID, columnID]).Text.ToString();
                    //读到空值null和读到空串""分别处理
                }
                dt.Rows.Add(dr);
            }
            return (dt);
        }
        /// <summary>
        /// 获取当前活动单元格有效行的总数
        /// </summary>
        /// <returns></returns>
        public int getTotalRowCount()
        {
            int rowsNumber = 0;
            try
            {
                while (true)
                {
                    if (((Excel.Range)xlsWs.Cells[rowsNumber + 1, 1]).Text.ToString().Trim() == "" &
                           ((Excel.Range)xlsWs.Cells[rowsNumber + 2, 1]).Text.ToString().Trim() == "" &
                           ((Excel.Range)xlsWs.Cells[rowsNumber + 3, 1]).Text.ToString().Trim() == "")
                        break;
                    rowsNumber++;
                }
            }
            catch
            {
                return -1;
            }
            return rowsNumber;
        }
        /// <summary>
        /// 当前活动工作表中有效列数(总列数)
        /// </summary>
        /// <param></param> 
        public int getTotalColumnCount()
        {
            int columnNumber = 0;
            try
            {
                while (true)
                {
                    if (((Excel.Range)xlsWs.Cells[1, columnNumber + 1]).Text.ToString().Trim() == "" &
                           ((Excel.Range)xlsWs.Cells[1, columnNumber + 2]).Text.ToString().Trim() == "" &
                           ((Excel.Range)xlsWs.Cells[1, columnNumber + 3]).Text.ToString().Trim() == "")
                        break;
                    columnNumber++;
                }
            }
            catch
            {
                return -1;
            }
            return columnNumber;
        }
        /// <summary>
        /// 向一个Cell写入数据
        /// </summary>
        /// <param name="CellRowID">CellRowID是cell的行索引</param>
        /// <param name="CellColumnID">CellColumnID是cell的列索引</param>
        ///<param name="Value">要写入该单元格的数据值</param>
        public void setOneCellValue(int CellRowID, int CellColumnID, string Value)
        {
            try
            {
                xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
                xlsRg.Value2 = Value;//Value2?
                xlsRg = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 设置活动工作表
        /// </summary>
        /// <param name="SheetIndex">要设置为活动工作表的索引值</param>
        public void SetActiveWorkSheet(int SheetIndex)
        {
            if (SheetIndex <= 0)
            {
                throw new Exception("索引超出范围！");
            }
            try
            {
                ActiveSheetIndex = SheetIndex;
                xlsWs = (Excel.Worksheet)xlsWb.Worksheets[ActiveSheetIndex];
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 向连续区域一次性写入数据;只有在区域连续和写入的值相同的情况下可以使用方法
        /// </summary>
        /// <param name="StartCell">StartCell是要写入区域的左上角单元格</param>
        /// <param name="EndCell">EndCell是要写入区域的右下角单元格</param>
        /// <param name="Value">要写入指定区域所有单元格的数据值</param>
        public void setCellsValue(string StartCell, string EndCell, string Value)
        {
            try
            {
                xlsRg = xlsWs.get_Range(StartCell, EndCell);
                xlsRg.Value2 = Value;
                xlsRg = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 给一行写数据
        /// </summary>
        public void setOneLineValues(int LineID, int StartCellColumnID, int EndCellColumnID, string Values)////已经测试
        {
            //用1-19号元素
            //if (Values.Length!=EndCellColumnID-StartCellColumnID)
            //{
            //    throw new Exception("单元格数目与提供的值的数目不一致！");
            //}
            for (int i = StartCellColumnID; i <= EndCellColumnID; i++)
            {
                setOneCellValue(LineID, i, Values);
            }
        }
        public void setCellsBorder(string startCell, string endCell)
        {
            //设置某个范围内的单元格的边框
            xlsRg = xlsWs.get_Range(startCell, endCell);
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            //excelRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        public void setOneCellBorder(int CellRowID, int CellColumnID)
        {
            //设置某个单元格的边框
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsRg.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //excelRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            //excelRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
        }
        public void SetColumnWidth(string startCell, string endCell, int size)
        {
            //设置某个范围内的单元格的列的宽度
            xlsRg = xlsWs.get_Range(startCell, endCell);
            xlsRg.ColumnWidth = size;
        }
        public void SetOneCellFont(int CellRowID, int CellColumnID, string fontName, int fontSize)
        {
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.Font.Name = fontName;
            xlsRg.Font.Size = fontSize;
        }
        public void SetOneCellHorizontalAlignment(int CellRowID, int CellColumnID, Excel.Constants alignment)
        {
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.HorizontalAlignment = alignment;
        }
        public void SetOneCellColumnWidth(int CellRowID, int CellColumnID, int size)
        {
            //设置某个单元格的列的宽度
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.ColumnWidth = size;
        }
        /// <summary>
        /// 设置一个Cell的数据格式
        /// </summary>
        /// <param name="CellRowID">CellRowID是cell的行索引</param>
        /// <param name="CellColumnID">CellColumnID是cell的列索引</param>
        ///<param name="Value">数据格式</param>
        public void setOneCellNumberFormat(int CellRowID, int CellColumnID, string numberFormat)
        {
            try
            {
                xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
                xlsRg.NumberFormatLocal = numberFormat;
                xlsRg = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        public void SetRowHeight(string startCell, string endCell, int size)
        {
            //设置某个范围内的单元格的行的高度
            xlsRg = xlsWs.get_Range(startCell, endCell);
            xlsRg.RowHeight = size;
        }
        public void SetRowHeight(int CellRowID, int CellColumnID, float size)
        {
            //设置某个范围内的单元格的行的高度
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.RowHeight = size;
        }
        public void SetOneCellRowHeight(int CellRowID, int CellColumnID, int size)
        {
            //设置某个单元格的行的高度
            xlsRg = (Excel.Range)xlsWs.Cells[CellRowID, CellColumnID];
            xlsRg.RowHeight = size;
        }
        /// <summary>
        /// 拷贝区域.限制：在同一个工作表中复制
        /// </summary>
        /// <param name="SourceStart">源区域的左上角单元格</param>
        /// <param name="SourceEnd">源区域的右下角单元格</param> 
        /// <param name="DesStart">目标区域的左上角单元格</param> 
        /// <param name="DesEnd">目标区域的右下角单元格</param> 
        public void CopyCells(string SourceStart, string SourceEnd, string DesStart, string DesEnd)
        {
            try
            {
                xlsCSRg = xlsWs.get_Range(SourceStart, SourceEnd);
                xlsRg = xlsWs.get_Range(DesStart, DesEnd);
                xlsCSRg.Copy(xlsRg);
                xlsCSRg = null;
                xlsRg = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        public void CopyWorksheet(int SourceWorksheetIndex, int DesWorksheetIndex)
        {
            try
            {
                Excel.Worksheet sheetSource = (Excel.Worksheet)xlsWb.Worksheets[SourceWorksheetIndex];
                sheetSource.Select(Missing.Value);
                Excel.Worksheet sheetDest = (Excel.Worksheet)xlsWb.Worksheets[DesWorksheetIndex];
                sheetSource.Copy(Missing.Value, sheetDest);
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 插入一行
        /// </summary>
        /// <param name="CellRowID">要插入所在行的索引位置，插入后其原有行下移</param> 
        /// <param name="RowNum">要插入行的个数</param> 
        public void InsertRow(int CellRowID, int RowNum)//插入空行
        {
            if (CellRowID <= 0)
            {
                throw new Exception("行索引超出范围！");
            }
            if (RowNum <= 0)
            {
                throw new Exception("插入行数无效！");
            }
            try
            {
                xlsRg = (Excel.Range)xlsWs.Rows[CellRowID, Missing.Value];
                for (int i = 0; i < RowNum; i++)
                {
                    xlsRg.Insert(Excel.XlDirection.xlDown);
                }
                xlsRg = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 查找第一个满足的区域
        /// </summary>
        public Excel.Range FindFirstRange(Excel.Range xlRange, string FindText)
        {
            Excel.Range firstFind = null;
            firstFind = xlRange.Find(FindText, Missing.Value, Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false, Missing.Value);
            return firstFind;  //如果没找到，返回空
        }

        /// <param></param> 
        /// <summary>
        /// 判断单元格是否有数据
        /// </summary>
        public bool CellValueIsNull(int CellLineID, int CellColumnID)////已经测试
        {
            //判断单元格是否有数据
            if ((((Excel.Range)xlsWs.Cells[CellLineID, CellColumnID]).Text.ToString().Trim() != ""))
                return false;
            return true;
        }
        public void newWorkbook(string excelTemplate, string fileName)
        {
            //以excelTemplate为模板新建文件fileName
            //excelApplication.
            xlsWb = xlsWbs.Add(excelTemplate);
            SaveFileName = "";
            SaveAsExcel(false);
        }
        public void newWorksheet()
        {
            xlsWb.Worksheets.Add(Missing.Value, Missing.Value, 1, Missing.Value);
        }
        public void setWorksheetName(int sheetIndex, string worksheetName)
        {
            // Missing.Value
            Excel._Worksheet sheet = (Excel._Worksheet)(xlsWb.Worksheets[(object)sheetIndex]);
            sheet.Name = worksheetName;
        }
        public void mergeOneLineCells(string startCell, string endCell)
        {
            //合并一行单元格 
            xlsRg = xlsWs.get_Range(startCell, endCell);
            //excelRange.Merge(true);
            xlsRg.MergeCells = true;
        }
        public void HorizontalAlignmentCells(string startCell, string endCell, Excel.Constants alignment)
        {
            //水平对齐一行单元格 
            xlsRg = xlsWs.get_Range(startCell, endCell);
            xlsRg.HorizontalAlignment = alignment;
        }
        public void VerticalAlignmentCells(string startCell, string endCell, Excel.Constants alignment)
        {
            //垂直对齐一行单元格 
            xlsRg = xlsWs.get_Range(startCell, endCell);
            xlsRg.VerticalAlignment = alignment;
        }

        /// <summary>   
        /// 绘制指定单元格的边框   
        /// </summary>   
        /// <param name="startRow">起始行</param>   
        /// <param name="startColumn">起始列</param>   
        /// <param name="endRow">结束行</param>   
        /// <param name="endColumn">结束列</param>   
        /// <param name="isDrawTop">是否画上外框</param>   
        /// <param name="isDrawBottom">是否画下外框</param>   
        /// <param name="isDrawLeft">是否画左外框</param>   
        /// <param name="isDrawRight">是否画右外框</param>   
        /// <param name="isDrawHInside">是否画水平内框</param>   
        /// <param name="isDrawVInside">是否画垂直内框</param>   
        /// <param name="isDrawDown">是否画斜向下线</param>   
        /// <param name="isDrawUp">是否画斜向上线</param>   
        /// <param name="lineStyle">线类型</param>   
        /// <param name="borderWeight">线粗细</param>   
        /// <param name="color">线颜色</param>   
        public void CellsDrawFrame(int startRow, int startColumn, int endRow, int endColumn,
            bool isDrawTop, bool isDrawBottom, bool isDrawLeft, bool isDrawRight,
            bool isDrawHInside, bool isDrawVInside, bool isDrawDiagonalDown, bool isDrawDiagonalUp,
            Excel.XlLineStyle lineStyle,Excel.XlBorderWeight borderWeight, Excel.XlColorIndex color)
        {
            
            //获取画边框的单元格   
            Excel.Range range = xlsApp.get_Range(xlsApp.Cells[startRow, startColumn], xlsApp.Cells[endRow, endColumn]);

            ////清除所有边框   
            //range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = LineStyle.无;
            //range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = LineStyle.无;

            //以下是按参数画边框    
            if (isDrawTop)
            {
                range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeTop].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = color;
            }

            if (isDrawBottom)
            {
                range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeBottom].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeBottom].ColorIndex = color;
            }

            if (isDrawLeft)
            {
                range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeLeft].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeLeft].ColorIndex = color;
            }

            if (isDrawRight)
            {
                range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlEdgeRight].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlEdgeRight].ColorIndex = color;
            }

            if (isDrawVInside)
            {
                range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideVertical].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideVertical].ColorIndex = color;
            }

            if (isDrawHInside)
            {
                range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlInsideHorizontal].ColorIndex = color;
            }

            if (isDrawDiagonalDown)
            {
                range.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalDown].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalDown].ColorIndex = color;
            }

            if (isDrawDiagonalUp)
            {
                range.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = lineStyle;
                range.Borders[XlBordersIndex.xlDiagonalUp].Weight = borderWeight;
                range.Borders[XlBordersIndex.xlDiagonalUp].ColorIndex = color;
            }
        }   
  

        //实现列号-〉字母 (26-〉Z,27->AA)
        private string ConvertColumnIndexToChar(int columnIndex)
        {
            if (columnIndex < 1 || columnIndex > 256)
            {
                return "A";
            }
            if (columnIndex >= 1 && columnIndex <= 26)//1--26
            {
                return "AA";
            }
            if (columnIndex >= 27 && columnIndex <= 256)//27--256
            {
                return "AA";
            }
            return "A";
        }
        //字母-〉列号 Z-〉26
        //public void SaveAsExcel()
        //{
        //    if (xlsSaveFileName == "")
        //    {
        //        throw new Exception("未指定要保存的文件名");
        //    }
        //    try
        //    {

        //        xlsApp.Visible = true;

        //        xlsWb.PrintPreview(false);
        //        xlsWb = null;
        //        xlsApp.Quit();
        //        xlsApp = null;
        //        //xlsWs.SaveAs(xlsSaveFileName, Excel.XlFileFormat.xlExcel7,
        //        //    Type.Missing, Type.Missing,
        //        //    Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
        //        //    Type.Missing, Type.Missing);               
        //    }
        //    catch (Exception e)
        //    {
        //        CloseExcelApplication();
        //        throw new Exception(e.Message);
        //    }
        //}
        public void SaveAsExcel(bool IsView)
        {
            try
            {
                if (File.Exists(xlsSaveFileName))
                    File.Delete(xlsSaveFileName);
                if (IsView)
                {
                    xlsApp.Visible = true;
                    xlsWb.PrintPreview(false);
                }
                xlsWb.Close(true, xlsSaveFileName,Type.Missing);
                xlsWb = null;
                xlsApp.Quit();
                xlsApp = null;
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 保存Excel文件，格式xml.
        /// </summary>
        public void SaveExcelAsXML()
        {
            if (xlsSaveFileName == "")
            {
                throw new Exception("未指定要保存的文件名");
            }
            try
            {
                xlsWs.SaveAs(xlsSaveFileName, Missing.Value, 
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Excel.XlSaveAsAccessMode.xlNoChange, 
                    Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                CloseExcelApplication();
                throw new Exception(e.Message);
            }
        }
        //--------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 关闭Excel文件，释放对象；最后一定要调用此函数，否则会引起异常
        /// </summary>
        /// <param></param> 
        public void CloseExcelApplication()
        {
            try
            {
                xlsWbs = null;
                xlsWb = null;
                xlsWs = null;
                xlsRg = null;
                if (xlsApp != null)
                {
                    xlsApp.ActiveWorkbook.Close(false, null, null);
                    xlsApp.Workbooks.Close();
                    //Object missing = Type.Missing;
                    xlsApp.Quit();
                    xlsApp = null;
                    //ReleaseAllRef(excelApplication);//Error
                }
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private void ReleaseAllRef(Object obj)
        {//ReleaseComObject()方法可以使RCW减少一个对COM组件的引用，并返回减少一个引用后RCW对COM组件的剩余引用数量。
            //我们用一个循环，就可以让RCW将所有对COM组件的引用全部去掉。
            try
            {
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(obj) > 1) ;
            }
            finally
            {
                obj = null;
            }
        }
        /// <summary>
        /// 将datatable导出到excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="saveasfilepath"></param>
        /// <param name="WorkbookName"></param>
        public void ExportExcel(System.Data.DataTable dt, string saveasfilepath,string WorkbookName)
        {
            if (dt == null) return;
            string saveFileName = saveasfilepath;  // "d:\\333.xlsx";
            bool fileSaved = false;
            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                throw new Exception("无法创建Excel对象，可能您的机子未安装Excel");
            }
            
            Excel.Workbooks workbooks = xlApp.Workbooks;
            Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);          
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            if (!string.IsNullOrEmpty(WorkbookName))
            {
                Excel.Worksheet ws = (Excel.Worksheet)workbook.Worksheets.get_Item(1);
                ws.Name = WorkbookName;
            }         
            Excel.Range range;
            //string oldCaption = DateTime.Today.ToString("yy-MM-dd");
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;

            // worksheet.Cells[1, 1] = DateTime.Today.ToString("yy-MM-dd");
            //写入字段
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            //写入数值

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i];
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
                //this.lbl_process.Text = "正在导出数据[" + percent.ToString("0.00") + "%]..."; //这里可以自己做一个label用来显示进度.
                System.Windows.Forms.Application.DoEvents();
            }
            //this.lbl_process.Visible = false;

            range = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]);
            range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, null);

            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;

            if (dt.Columns.Count > 1)
            {
                range.Borders[Excel.XlBordersIndex.xlInsideVertical].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                range.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                range.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;
            }

            if (saveFileName != "")
            {
                try
                {
                    //xlApp.Visible = true;
                    //workbook.PrintPreview(false);
                    workbook.Close(true, saveFileName, Type.Missing);
                    workbook = null;
                    //SaveAs方法会截断超过255个字符的字符串
                    //workbook.Saved = true;
                    //workbook.SaveAs(saveFileName, Excel.XlFileFormat.xlExcel7, Type.Missing,
                    //    Type.Missing, Type.Missing, Type.Missing,
                    //    Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                    //    Type.Missing, Type.Missing, Type.Missing);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    xlApp.Quit();
                    GC.Collect();
                    fileSaved = false;
                    throw new Exception("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            else
            {
                fileSaved = false;
            }
            xlApp.Quit();
            xlApp = null;
            GC.Collect();//强行销毁
            if (fileSaved && File.Exists(saveFileName))
            {
                //System.Diagnostics.Process.Start(saveFileName);

            }
        }

        /// <summary>
        /// 保存cvs格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="saveasfilepath"></param>
        public void ExportDataTableToText(System.Data.DataTable dt, string saveasfilepath)
        {
            if (!File.Exists(saveasfilepath))
                File.Delete(saveasfilepath);
            StreamWriter sw = new StreamWriter(saveasfilepath);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str = string.Empty;
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    str += dt.Rows[i][x].ToString() + ",'";
                }
                str = str.Substring(0, str.Length - 1) + "\n";

                sw.Write(str);
            }

            sw.Close();
        }

    }
}