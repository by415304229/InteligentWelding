using InteligentWelding.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

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

            //大梁参数模型
            Girders girders = new Girders();
            // 读取
            byte[] bin = System.IO.File.ReadAllBytes(filePath);
            byte[] outStream;
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet sheetGirders = excelPackage.Workbook.Worksheets[0];
                ExcelWorksheet sheetBulkhead = excelPackage.Workbook.Worksheets[1];
            }

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
            string templeteA = System.AppDomain.CurrentDomain.BaseDirectory + @"Templete/导入模板（A型）.xlsx";
            string templeteB = System.AppDomain.CurrentDomain.BaseDirectory + @"Templete/导入模板（B型）.xlsx";
            return true;
        }
    }
}
