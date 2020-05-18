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
    static class ExcelAdapter
    {
        /// <summary>
        /// 从excel中读取大梁参数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileMode">文件模板类型</param>
        /// <returns></returns>
        public static void GetGirdersFromExcel(string filePath, ref Girders girders)
        {
            // 读取
            try
            {
                byte[] bin = System.IO.File.ReadAllBytes(filePath);
                GetGirdersFromExcel(bin, ref girders);
            }
            catch (IOException)
            {
                MessageBox.Show("文件读取失败,请检查文件格式或内容");
            }
        }
        public static void GetGirdersFromExcel(byte[] bin, ref Girders girders)
        {
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet sheetGirders = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet sheetBulkhead = excelPackage.Workbook.Worksheets[2];
                girders.Name = Convert.ToString(sheetGirders.Cells["C3"].Value);
                girders.WorkNo = Convert.ToString(sheetGirders.Cells["C4"].Value);
                girders.WireType = Convert.ToString(sheetGirders.Cells["C6"].Value);
                girders.Type = Convert.ToString(sheetGirders.Cells["C8"].Value);
                girders.BulkheadCount = Convert.ToInt16(sheetGirders.Cells["C7"].Value);
                OfficeOpenXml.Drawing.ExcelPicture pic = sheetGirders.Drawings[0] as OfficeOpenXml.Drawing.ExcelPicture;
                girders.Picture = pic.Image;
                girders.BulkheadsLeftA = new List<BulkheadA>();
                girders.BulkheadsRightA = new List<BulkheadA>();
                girders.BulkheadsLeftB = new List<BulkheadB>();
                girders.BulkheadsRightB = new List<BulkheadB>();

                if (girders.Type != "B")
                {
                    //A型隔板参数
                    girders.BulkheadParaA.H1 = Convert.ToDouble(sheetGirders.Cells["C9"].Value);
                    girders.BulkheadParaA.W1 = Convert.ToDouble(sheetGirders.Cells["C10"].Value);
                    girders.BulkheadParaA.H2 = Convert.ToDouble(sheetGirders.Cells["C11"].Value);
                    girders.BulkheadParaA.L1 = Convert.ToDouble(sheetGirders.Cells["C12"].Value);
                    girders.BulkheadParaA.L2 = Convert.ToDouble(sheetGirders.Cells["C13"].Value);
                    girders.BulkheadParaA.L3 = Convert.ToDouble(sheetGirders.Cells["C14"].Value);
                    girders.BulkheadParaA.R1 = Convert.ToDouble(sheetGirders.Cells["C15"].Value);
                    girders.BulkheadParaA.R2 = Convert.ToDouble(sheetGirders.Cells["C16"].Value);
                    girders.BulkheadParaA.R3 = Convert.ToDouble(sheetGirders.Cells["C17"].Value);
                    girders.BulkheadParaA.R4 = Convert.ToDouble(sheetGirders.Cells["C18"].Value);
                    girders.BulkheadParaA.H3 = Convert.ToDouble(sheetGirders.Cells["C19"].Value);
                    girders.BulkheadParaA.W3 = Convert.ToDouble(sheetGirders.Cells["C20"].Value);
                    girders.BulkheadParaA.H4 = Convert.ToDouble(sheetGirders.Cells["C21"].Value);
                    girders.BulkheadParaA.H5 = Convert.ToDouble(sheetGirders.Cells["C22"].Value);
                    girders.BulkheadParaA.H6 = Convert.ToDouble(sheetGirders.Cells["C23"].Value);
                    girders.BulkheadParaA.R7 = Convert.ToDouble(sheetGirders.Cells["C24"].Value);
                    girders.BulkheadParaA.R8 = Convert.ToDouble(sheetGirders.Cells["C25"].Value);
                    girders.BulkheadParaA.R9 = Convert.ToDouble(sheetGirders.Cells["C26"].Value);
                    girders.BulkheadParaA.R10 = Convert.ToDouble(sheetGirders.Cells["C27"].Value);
                    girders.BulkheadParaA.T1 = Convert.ToDouble(sheetGirders.Cells["C28"].Value);
                    girders.BulkheadParaA.T2 = Convert.ToDouble(sheetGirders.Cells["C29"].Value);
                    for (int i = 0; i < girders.BulkheadCount; i++)
                    {
                        //隔板参数
                        BulkheadA bk = new BulkheadA();
                        bk.BulkHeadNo = Convert.ToInt16(sheetGirders.Cells["A" + (34 + i).ToString()].Value);
                        bk.SerialNo = Convert.ToInt16(sheetGirders.Cells["B" + (34 + i).ToString()].Value);
                        bk.BulkheadSpace = Convert.ToInt16(sheetGirders.Cells["C" + (34 + i).ToString()].Value);
                        bk.IsWelding = Convert.ToBoolean(sheetGirders.Cells["D" + (34 + i).ToString()].Value);
                        bk.IsSection1 = Convert.ToBoolean(sheetGirders.Cells["E" + (34 + i).ToString()].Value);
                        bk.IsSection2 = Convert.ToBoolean(sheetGirders.Cells["F" + (34 + i).ToString()].Value);
                        bk.IsSection3 = Convert.ToBoolean(sheetGirders.Cells["G" + (34 + i).ToString()].Value);
                        bk.Robot = Convert.ToInt16(sheetGirders.Cells["H" + (34 + i).ToString()].Value);
                        //焊缝
                        for (int j = 0; j < 40; j++)
                        {
                            bk.Beads[j].BeadNo = Convert.ToInt16(sheetBulkhead.Cells[2 + 9 * i, 2 + j].Value);//"B2",B11
                            bk.Beads[j].IsWeld = Convert.ToBoolean(sheetBulkhead.Cells[3 + 9 * i, 2 + j].Value);
                            bk.Beads[j].SerialNo = Convert.ToInt16(sheetBulkhead.Cells[4 + 9 * i, 2 + j].Value);
                            bk.Beads[j].JobNo = Convert.ToInt16(sheetBulkhead.Cells[5 + 9 * i, 2 + j].Value);
                        }
                        if (bk.Robot == (int)Robot.Left)
                        {
                            girders.BulkheadsLeftA.Add(bk);
                        }
                        else
                        {
                            girders.BulkheadsRightA.Add(bk);
                        }
                    }
                }
                else
                {
                    //B型隔板参数
                    girders.BulkheadParaB.H1 = Convert.ToDouble(sheetGirders.Cells["C9"].Value);
                    girders.BulkheadParaB.W1 = Convert.ToDouble(sheetGirders.Cells["C10"].Value);
                    girders.BulkheadParaB.H2 = Convert.ToDouble(sheetGirders.Cells["C11"].Value);
                    girders.BulkheadParaB.L1 = Convert.ToDouble(sheetGirders.Cells["C12"].Value);
                    girders.BulkheadParaB.L2 = Convert.ToDouble(sheetGirders.Cells["C13"].Value);
                    girders.BulkheadParaB.G1 = Convert.ToDouble(sheetGirders.Cells["C14"].Value);
                    girders.BulkheadParaB.G2 = Convert.ToDouble(sheetGirders.Cells["C15"].Value);
                    girders.BulkheadParaB.R1 = Convert.ToDouble(sheetGirders.Cells["C16"].Value);
                    girders.BulkheadParaB.R2 = Convert.ToDouble(sheetGirders.Cells["C17"].Value);
                    girders.BulkheadParaB.R3 = Convert.ToDouble(sheetGirders.Cells["C18"].Value);
                    girders.BulkheadParaB.R4 = Convert.ToDouble(sheetGirders.Cells["C19"].Value);
                    girders.BulkheadParaB.R5 = Convert.ToDouble(sheetGirders.Cells["C20"].Value);
                    girders.BulkheadParaB.R6 = Convert.ToDouble(sheetGirders.Cells["C21"].Value);
                    girders.BulkheadParaB.T1 = Convert.ToDouble(sheetGirders.Cells["C22"].Value);
                    for (int i = 0; i < girders.BulkheadCount; i++)
                    {
                        //隔板参数
                        BulkheadB bk = new BulkheadB();
                        bk.BulkHeadNo = Convert.ToInt16(sheetGirders.Cells["A" + (27 + i).ToString()].Value);
                        bk.SerialNo = Convert.ToInt16(sheetGirders.Cells["B" + (27 + i).ToString()].Value);
                        bk.BulkheadSpace = Convert.ToInt16(sheetGirders.Cells["C" + (27 + i).ToString()].Value);
                        bk.IsWelding = Convert.ToBoolean(sheetGirders.Cells["D" + (27 + i).ToString()].Value);
                        bk.Robot = Convert.ToInt16(sheetGirders.Cells["E" + (27 + i).ToString()].Value);
                        //焊缝
                        for (int j = 0; j < 40; j++)
                        {
                            bk.Beads[j].BeadNo = Convert.ToInt16(sheetBulkhead.Cells[2 + 9 * i, 2 + j].Value);//"B2",B11
                            bk.Beads[j].IsWeld = Convert.ToBoolean(sheetBulkhead.Cells[3 + 9 * i, 2 + j].Value);
                            bk.Beads[j].SerialNo = Convert.ToInt16(sheetBulkhead.Cells[4 + 9 * i, 2 + j].Value);
                            bk.Beads[j].JobNo = Convert.ToInt16(sheetBulkhead.Cells[5 + 9 * i, 2 + j].Value);
                        }
                        if (bk.Robot == (int)Robot.Left)
                        {
                            girders.BulkheadsLeftB.Add(bk);
                        }
                        else
                        {
                            girders.BulkheadsRightB.Add(bk);
                        }
                    }
                }
            }
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
            byte[] binA = Properties.Resources.TemplateA;
            byte[] binB = Properties.Resources.TemplateB;
            try
            {
                byte[] bin = girders.Type != "B" ? binA : binB;
                using (MemoryStream stream = new MemoryStream(bin))
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    ExcelWorksheet sheetGirders = excelPackage.Workbook.Worksheets[1];
                    ExcelWorksheet sheetBulkhead = excelPackage.Workbook.Worksheets[2];
                    sheetGirders.Cells["C3"].Value = girders.Name;
                    sheetGirders.Cells["C4"].Value = girders.WorkNo;
                    sheetGirders.Cells["C6"].Value = girders.WireType;
                    sheetGirders.Cells["C8"].Value = girders.Type;
                    sheetGirders.Cells["C5"].Value= girders.BulkheadCount;
                    sheetGirders.Cells["C7"].Value = girders.BulkheadCount;
                    if (girders.Type != "B")
                    {
                        //A型隔板参数
                        sheetGirders.Cells["C9"].Value = girders.BulkheadParaA.H1;
                        sheetGirders.Cells["C10"].Value = girders.BulkheadParaA.W1;
                        sheetGirders.Cells["C11"].Value = girders.BulkheadParaA.H2;
                        sheetGirders.Cells["C12"].Value = girders.BulkheadParaA.L1;
                        sheetGirders.Cells["C13"].Value = girders.BulkheadParaA.L2;
                        sheetGirders.Cells["C14"].Value = girders.BulkheadParaA.L3;
                        sheetGirders.Cells["C15"].Value = girders.BulkheadParaA.R1;
                        sheetGirders.Cells["C16"].Value = girders.BulkheadParaA.R2;
                        sheetGirders.Cells["C17"].Value = girders.BulkheadParaA.R3;
                        sheetGirders.Cells["C18"].Value = girders.BulkheadParaA.R4;
                        sheetGirders.Cells["C19"].Value = girders.BulkheadParaA.H3;
                        sheetGirders.Cells["C20"].Value = girders.BulkheadParaA.W3;
                        sheetGirders.Cells["C21"].Value = girders.BulkheadParaA.H4;
                        sheetGirders.Cells["C22"].Value = girders.BulkheadParaA.H5;
                        sheetGirders.Cells["C23"].Value = girders.BulkheadParaA.H6;
                        sheetGirders.Cells["C24"].Value = girders.BulkheadParaA.R7;
                        sheetGirders.Cells["C25"].Value = girders.BulkheadParaA.R8;
                        sheetGirders.Cells["C26"].Value = girders.BulkheadParaA.R9;
                        sheetGirders.Cells["C27"].Value = girders.BulkheadParaA.R10;
                        sheetGirders.Cells["C28"].Value = girders.BulkheadParaA.T1;
                        sheetGirders.Cells["C29"].Value = girders.BulkheadParaA.T2;
                        List<BulkheadA> res = girders.BulkheadsLeftA.Union(girders.BulkheadsRightA).OrderBy(item => item.BulkHeadNo).ToList();
                        for (int i = 0; i < res.Count; i++)
                        {
                            //隔板参数
                            BulkheadA bk = res[i];
                            sheetGirders.Cells["A" + (34 + i).ToString()].Value = bk.BulkHeadNo;
                            sheetGirders.Cells["B" + (34 + i).ToString()].Value = bk.SerialNo;
                            sheetGirders.Cells["C" + (34 + i).ToString()].Value = bk.BulkheadSpace;
                            sheetGirders.Cells["D" + (34 + i).ToString()].Value = bk.IsWelding  ? 1: 0;
                            sheetGirders.Cells["E" + (34 + i).ToString()].Value = bk.IsSection1 ? 1: 0;
                            sheetGirders.Cells["F" + (34 + i).ToString()].Value = bk.IsSection2 ? 1: 0;
                            sheetGirders.Cells["G" + (34 + i).ToString()].Value = bk.IsSection3 ? 1: 0;
                            sheetGirders.Cells["H" + (34 + i).ToString()].Value = bk.Robot;
                            //焊缝
                            for (int j = 0; j < 40; j++)
                            {
                                sheetBulkhead.Cells[2 + 9 * i, 2 + j].Value = bk.Beads[j].BeadNo;//"B2",B11
                                sheetBulkhead.Cells[3 + 9 * i, 2 + j].Value = bk.Beads[j].IsWeld ? 1 : 0;
                                sheetBulkhead.Cells[4 + 9 * i, 2 + j].Value = bk.Beads[j].SerialNo;
                                sheetBulkhead.Cells[5 + 9 * i, 2 + j].Value = bk.Beads[j].JobNo;
                            }
                        }
                    }
                    else
                    {
                        //B型隔板参数
                        sheetGirders.Cells["C9"].Value = girders.BulkheadParaB.H1;
                        sheetGirders.Cells["C10"].Value = girders.BulkheadParaB.W1;
                        sheetGirders.Cells["C11"].Value = girders.BulkheadParaB.H2;
                        sheetGirders.Cells["C12"].Value = girders.BulkheadParaB.L1;
                        sheetGirders.Cells["C13"].Value = girders.BulkheadParaB.L2;
                        sheetGirders.Cells["C14"].Value = girders.BulkheadParaB.G1;
                        sheetGirders.Cells["C15"].Value = girders.BulkheadParaB.G2;
                        sheetGirders.Cells["C16"].Value = girders.BulkheadParaB.R1;
                        sheetGirders.Cells["C17"].Value = girders.BulkheadParaB.R2;
                        sheetGirders.Cells["C18"].Value = girders.BulkheadParaB.R3;
                        sheetGirders.Cells["C19"].Value = girders.BulkheadParaB.R4;
                        sheetGirders.Cells["C20"].Value = girders.BulkheadParaB.R5;
                        sheetGirders.Cells["C21"].Value = girders.BulkheadParaB.R6;
                        sheetGirders.Cells["C22"].Value = girders.BulkheadParaB.T1;
                        List<BulkheadB> res = girders.BulkheadsLeftB.Union(girders.BulkheadsRightB).OrderBy(item => item.BulkHeadNo).ToList();
                        for (int i = 0; i < res.Count; i++)
                        {
                            //隔板参数
                            BulkheadB bk = res[i];
                            sheetGirders.Cells["A" + (27 + i).ToString()].Value = bk.BulkHeadNo;
                            sheetGirders.Cells["B" + (27 + i).ToString()].Value = bk.SerialNo;
                            sheetGirders.Cells["C" + (27 + i).ToString()].Value = bk.BulkheadSpace;
                            sheetGirders.Cells["D" + (27 + i).ToString()].Value = bk.IsWelding ? 1 : 0;
                            sheetGirders.Cells["E" + (27 + i).ToString()].Value = bk.Robot;
                            //焊缝
                            for (int j = 0; j < 40; j++)
                            {
                                sheetBulkhead.Cells[2 + 9 * i, 2 + j].Value = bk.Beads[j].BeadNo;//"B2",B11
                                sheetBulkhead.Cells[3 + 9 * i, 2 + j].Value = bk.Beads[j].IsWeld ? 1 : 0;
                                sheetBulkhead.Cells[4 + 9 * i, 2 + j].Value = bk.Beads[j].SerialNo;
                                sheetBulkhead.Cells[5 + 9 * i, 2 + j].Value = bk.Beads[j].JobNo;
                            }
                        }
                    }
                    excelPackage.SaveAs(new FileInfo(filePath + ".xlsx"));
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("文件保存失败");
            }
            return true;
        }
    }
}
