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
using InteligentWelding.Entity;

namespace InteligentWelding
{
    public partial class Form1 : Form
    {
        public Girders Entity { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        private void Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "请选择项目文件",
                Filter = "所有文件(*xls*)|*.xls*"
            };
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    string file = dialog.FileName;//返回文件的完整路径
            //    ExcelAccess.GetGirdersFromExcel(file);
            //}

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Entity.Name = Guid.NewGuid().ToString();
            
        }
        //根据A,B模板类型加载隔板参数
        private void ShowBulkheads()
        {
            if (txtType.Text == "A")
            {
                DataGridViewCheckBoxColumn column1 = new DataGridViewCheckBoxColumn();
                column1.HeaderText = "区域1是否进入";
                column1.Width = 70;
                dgvLeft.Columns.Add(column1);
                DataGridViewCheckBoxColumn column2 = new DataGridViewCheckBoxColumn();
                column2.HeaderText = "区域2是否进入";
                column2.Width = 70;
                dgvLeft.Columns.Add(column2);
                DataGridViewCheckBoxColumn column3 = new DataGridViewCheckBoxColumn();
                column3.HeaderText = "区域3是否进入";
                column3.Width = 70;
                dgvLeft.Columns.Add(column3);
                panelA.Show();
                panelB.Hide();
            }
            else
            {
                panelA.Hide();
                panelB.Show();
            }
        }
        /// <summary>
        /// 动态生成焊缝信息
        /// </summary>
        private void ShowBeads()
        {
            //编号
            for (int i = 0; i < 40; i++)
            {
                Label lb1 = new Label();
                lb1.Text = (i + 1).ToString();
                lb1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tableLayoutPanel1.Controls.Add(lb1);
            }
            //是否焊接
            for (int i = 0; i < 40; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "cbIsWelding" + (i + 1).ToString();
                cb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tableLayoutPanel1.Controls.Add(cb);
            }
            //焊接顺序
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtSerialNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tableLayoutPanel1.Controls.Add(tb);
            }
            //JOB号
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtJobNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tableLayoutPanel1.Controls.Add(tb);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowBeads();
            Entity = new Girders();
            Entity.Name = "";
            txtName.DataBindings.Add("Text", Entity, "Name");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Entity.Name);
        }
    }
}
