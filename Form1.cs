using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace InteligentWelding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择项目文件";
            dialog.Filter = "所有文件(*xls*)|*.xls*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;//返回文件的完整路径
                //ExcelAccess.GetGirdersFromExcel(file);
            }
        }
    }
}
