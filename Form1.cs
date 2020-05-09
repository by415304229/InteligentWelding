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
        public Girders Entity;
        public BindingSource bindingLeft = new BindingSource();
        public BindingSource bindingRight = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }
        //根据A,B模板类型加载隔板参数
        private void ShowBulkheads(string type)
        {
            bindingLeft = new BindingSource();
            bindingRight = new BindingSource();

            if (type != "B")
            {
                foreach (var item in Entity.BulkheadsLeftA)
                {
                    bindingLeft.Add(item);
                }
                foreach (var item in Entity.BulkheadsRightA)
                {
                    bindingRight.Add(item);
                }

                IsSection1L.Visible = true;
                IsSection2L.Visible = true;
                IsSection3L.Visible = true;
                IsSection1R.Visible = true;
                IsSection2R.Visible = true;
                IsSection3R.Visible = true;
                panelA.Show();
                panelB.Hide();
            }
            else
            {
                foreach (var item in Entity.BulkheadsLeftB)
                {
                    bindingLeft.Add(item);
                }
                foreach (var item in Entity.BulkheadsRightB)
                {
                    bindingRight.Add(item);
                }

                IsSection1L.Visible = false;
                IsSection2L.Visible = false;
                IsSection3L.Visible = false;
                IsSection1R.Visible = false;
                IsSection2R.Visible = false;
                IsSection3R.Visible = false;
                panelA.Hide();
                panelB.Show();
            }
            dgvLeft.DataSource = bindingLeft;
            dgvRight.DataSource = bindingRight;
        }
        /// <summary>
        /// 动态生成焊缝信息
        /// </summary>
        private void ShowBeadsA(int bulkNo, int rowNo, Robot robot)
        {
            tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label34, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label35, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label36, 0, 3);
            int serialNo = rowNo;
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
                //非初始化加载时进行数据绑定
                if(bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        cb.Checked = Entity.BulkheadsLeftA[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsLeftA[serialNo].Beads[i], "IsWeld");
                    }
                    else
                    {
                        cb.Checked = Entity.BulkheadsRightA[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsRightA[serialNo].Beads[i], "IsWeld");
                    }
                }
                tableLayoutPanel1.Controls.Add(cb);
            }
            //焊接顺序
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtSerialNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        tb.Text = Entity.BulkheadsLeftA[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftA[serialNo].Beads[i], "SerialNo");
                    }
                    else
                    {
                        tb.Text = Entity.BulkheadsRightA[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightA[serialNo].Beads[i], "SerialNo");
                    }
                }
                tableLayoutPanel1.Controls.Add(tb);
            }
            //JOB号
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtJobNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        tb.Text = Entity.BulkheadsLeftA[serialNo].Beads[i].JobNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftA[serialNo].Beads[i], "JobNo");
                    }
                    else
                    {
                        tb.Text = Entity.BulkheadsRightA[serialNo].Beads[i].JobNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightA[serialNo].Beads[i], "JobNo");
                    }
                }
                tableLayoutPanel1.Controls.Add(tb);
            }
        }
        /// <summary>
        /// 动态生成焊缝信息
        /// </summary>
        private void ShowBeadsB(int bulkNo, int rowNo, Robot robot)
        {
            tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label34, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label35, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label36, 0, 3);
            int serialNo = rowNo;
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
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        cb.Checked = Entity.BulkheadsLeftB[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsLeftB[serialNo].Beads[i], "IsWeld");
                    }
                    else
                    {
                        cb.Checked = Entity.BulkheadsRightB[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsRightB[serialNo].Beads[i], "IsWeld");
                    }
                }
                tableLayoutPanel1.Controls.Add(cb);
            }
            //焊接顺序
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtSerialNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        tb.Text = Entity.BulkheadsLeftB[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftB[serialNo].Beads[i], "SerialNo");
                    }
                    else
                    {
                        tb.Text = Entity.BulkheadsRightB[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightB[serialNo].Beads[i], "SerialNo");
                    }
                }
                tableLayoutPanel1.Controls.Add(tb);
            }
            //JOB号
            for (int i = 0; i < 40; i++)
            {
                TextBox tb = new TextBox();
                tb.Name = "txtJobNo" + (i + 1).ToString();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        tb.Text = Entity.BulkheadsLeftB[serialNo].Beads[i].JobNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftB[serialNo].Beads[i], "JobNo");
                    }
                    else
                    {
                        tb.Text = Entity.BulkheadsRightB[serialNo].Beads[i].JobNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightB[serialNo].Beads[i], "JobNo");
                    }
                }
                tableLayoutPanel1.Controls.Add(tb);
            }
        }
        /// <summary>
        /// 参数绑定(待优化)
        /// </summary>
        private void ParasBind()
        {
            txtName.DataBindings.Add("Text", Entity, "Name");
            txtWorkNo.DataBindings.Add("Text", Entity, "WorkNo");
            txtType.DataBindings.Add("Text", Entity, "Type");
            txtBulkheadCount.DataBindings.Add("Text", Entity, "BulkheadCount");
            txtWireType.DataBindings.Add("Text", Entity, "WireType");
            txtAT2.DataBindings.Add("Text", Entity.BulkheadParaA, "T2");
            txtAT1.DataBindings.Add("Text", Entity.BulkheadParaA, "T1");
            txtAR10.DataBindings.Add("Text", Entity.BulkheadParaA, "R10");
            txtAR9.DataBindings.Add("Text", Entity.BulkheadParaA, "R9");
            txtAR8.DataBindings.Add("Text", Entity.BulkheadParaA, "R8");
            txtAR7.DataBindings.Add("Text", Entity.BulkheadParaA, "R7");
            txtAH6.DataBindings.Add("Text", Entity.BulkheadParaA, "H6");
            txtAH5.DataBindings.Add("Text", Entity.BulkheadParaA, "H5");
            txtAH4.DataBindings.Add("Text", Entity.BulkheadParaA, "H4");
            txtAW3.DataBindings.Add("Text", Entity.BulkheadParaA, "W3");
            txtAH3.DataBindings.Add("Text", Entity.BulkheadParaA, "H3");
            txtAR4.DataBindings.Add("Text", Entity.BulkheadParaA, "R4");
            txtAR3.DataBindings.Add("Text", Entity.BulkheadParaA, "R3");
            txtAR2.DataBindings.Add("Text", Entity.BulkheadParaA, "R2");
            txtAR1.DataBindings.Add("Text", Entity.BulkheadParaA, "R1");
            txtAL3.DataBindings.Add("Text", Entity.BulkheadParaA, "L3");
            txtAL2.DataBindings.Add("Text", Entity.BulkheadParaA, "L2");
            txtAL1.DataBindings.Add("Text", Entity.BulkheadParaA, "L1");
            txtAH2.DataBindings.Add("Text", Entity.BulkheadParaA, "H2");
            txtAW1.DataBindings.Add("Text", Entity.BulkheadParaA, "W1");
            txtAH1.DataBindings.Add("Text", Entity.BulkheadParaA, "H1");
            txtBT1.DataBindings.Add("Text", Entity.BulkheadParaB, "T1");
            txtBR6.DataBindings.Add("Text", Entity.BulkheadParaB, "R6");
            txtBR5.DataBindings.Add("Text", Entity.BulkheadParaB, "R5");
            txtBR4.DataBindings.Add("Text", Entity.BulkheadParaB, "R4");
            txtBR3.DataBindings.Add("Text", Entity.BulkheadParaB, "R3");
            txtBR2.DataBindings.Add("Text", Entity.BulkheadParaB, "R2");
            txtBR1.DataBindings.Add("Text", Entity.BulkheadParaB, "R1");
            txtBG2.DataBindings.Add("Text", Entity.BulkheadParaB, "G2");
            txtBG1.DataBindings.Add("Text", Entity.BulkheadParaB, "G1");
            txtBL2.DataBindings.Add("Text", Entity.BulkheadParaB, "L2");
            txtBL1.DataBindings.Add("Text", Entity.BulkheadParaB, "L1");
            txtBH2.DataBindings.Add("Text", Entity.BulkheadParaB, "H2");
            txtBW1.DataBindings.Add("Text", Entity.BulkheadParaB, "W1");
            txtBH1.DataBindings.Add("Text", Entity.BulkheadParaB, "H1");
            picGirders.DataBindings.Add("Image", Entity, "Picture");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Entity = new Girders();
            //初始化加载
            //ShowBeads(0, 0, Robot.Left);
            ShowBulkheads("A");
            ParasBind();
            //todo:状态展示
            picLeftState.Image = Properties.Resources.Image2;
            picRightState.Image = Properties.Resources.Image2;
            picPLCState.Image = Properties.Resources.Image2;
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "请选择项目文件",
                Filter = "Excel工作簿(*xls*)|*.xls*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;//返回文件的完整路径
                ExcelAdapter.GetGirdersFromExcel(file, ref Entity);
                ShowBulkheads(Entity.Type);
            }
        }

        private void btSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "请选择保存目录",
                Filter = "Excel工作簿(*xls*)|*.xls*",
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;//返回文件的完整路径
                ExcelAdapter.SaveGirdersAsNewFile(file, Entity);
            }
        }

        private void dgvLeft_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            int serialNo = Convert.ToInt16(dgvLeft.Rows[index].Cells["SerialNoL"].Value);
            if (Entity.Type != "B")
            {
                ShowBeadsA(Entity.BulkheadsLeftA[index].BulkHeadNo, index, (Robot)Entity.BulkheadsLeftA[index].Robot);
            }
            else
            {
                ShowBeadsB(Entity.BulkheadsLeftB[index].BulkHeadNo, index, (Robot)Entity.BulkheadsLeftB[index].Robot);
            }
        }

        private void dgvRight_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0)
            {
                return;
            }
            int serialNo = Convert.ToInt16(dgvRight.Rows[index].Cells["SerialNoR"].Value);
            if (Entity.Type != "B")
            {
                ShowBeadsA(Entity.BulkheadsRightA[index].BulkHeadNo, index, (Robot)Entity.BulkheadsRightA[index].Robot);
            }
            else
            {
                ShowBeadsB(Entity.BulkheadsRightB[index].BulkHeadNo, index, (Robot)Entity.BulkheadsRightB[index].Robot);
            }
        }
    }
}
