using InteligentWelding.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace InteligentWelding
{
    static class ExcelAccess
    {
        /// <summary>
        /// 从excel中读取大梁参数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileMode">文件模板类型</param>
        /// <returns></returns>
        public static Girders GetGirdersFromExcel(string filePath)
        {
            Application excelApp = new Application();
            //if (null == excelApp)
            //{
            //    throw (new Exception("读取Excel失败!"));
            //}
            Workbooks wbks = excelApp.Workbooks;
            Workbook _wb = wbks.Add(filePath);
            //大梁参数页
            Worksheet _wsGirder = _wb.Sheets[0];
            //隔板参数页
            Worksheet _wsBulkhead = _wb.Sheets[1];
            //大梁参数模型
            Girders girders = new Girders();

            girders.bulkheadCount = _wsGirder.Cells[1, 1];

            return girders;
        }
        /// <summary>
        /// 将大梁参数另存为新的文件
        /// </summary>
        /// <param name="filePath">保存路径</param>
        /// <param name="modePath">模板路径</param>
        /// <param name="girders">大梁参数</param>
        /// <returns></returns>
        public static bool SaveGirdersAsNewFile(string filePath, Girders girders)
        {
            return true;
        }
    }
}
