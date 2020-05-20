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
using Opc.Da;

namespace InteligentWelding
{
    public partial class Form1 : Form
    {
        OPCMonitor monitor = new OPCMonitor();
        PLCPara PLCPara = new PLCPara();
        UpperPara upperPara = new UpperPara();
        public Girders Entity;
        public BindingSource bindingLeft = new BindingSource();
        public BindingSource bindingRight = new BindingSource();
        bool isRequest = false;
        bool isReady = false;

        public Form1()
        {
            InitializeComponent();
        }
        public void ShowBeads()
        {
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label34, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label35, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label36, 0, 3);
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
        /// 动态绑定焊缝信息
        /// </summary>
        private void ShowBeadsA(int bulkNo, int rowNo, Robot robot)
        {
            int serialNo = rowNo;
            //是否焊接
            for (int i = 0; i < 40; i++)
            {
                CheckBox cb = tableLayoutPanel1.GetControlFromPosition(i+ 1, 1) as CheckBox;
                TextBox tb = tableLayoutPanel1.GetControlFromPosition(i + 1, 2) as TextBox;
                TextBox tb2 = tableLayoutPanel1.GetControlFromPosition(i+ 1, 3) as TextBox;
                cb.DataBindings.Clear();
                tb.DataBindings.Clear();
                tb2.DataBindings.Clear();
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        cb.Checked = Entity.BulkheadsLeftA[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsLeftA[serialNo].Beads[i], "IsWeld");
                        tb.Text = Entity.BulkheadsLeftA[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftA[serialNo].Beads[i], "SerialNo");
                        tb2.Text = Entity.BulkheadsLeftA[serialNo].Beads[i].JobNo.ToString();
                        tb2.DataBindings.Add("Text", Entity.BulkheadsLeftA[serialNo].Beads[i], "JobNo");
                    }
                    else
                    {
                        cb.Checked = Entity.BulkheadsRightA[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsRightA[serialNo].Beads[i], "IsWeld");
                        tb.Text = Entity.BulkheadsRightA[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightA[serialNo].Beads[i], "SerialNo");
                        tb2.Text = Entity.BulkheadsRightA[serialNo].Beads[i].JobNo.ToString();
                        tb2.DataBindings.Add("Text", Entity.BulkheadsRightA[serialNo].Beads[i], "JobNo");
                    }
                }
            }
        }
        /// <summary>
        /// 动态生成焊缝信息
        /// </summary>
        private void BindBeadsB(int bulkNo, int rowNo, Robot robot)
        {
            int serialNo = rowNo;

            for (int i = 0; i < 40; i++)
            {
                CheckBox cb = tableLayoutPanel1.GetControlFromPosition(i+ 1, 1) as CheckBox;
                TextBox tb = tableLayoutPanel1.GetControlFromPosition(i + 1, 2) as TextBox;
                TextBox tb2 = tableLayoutPanel1.GetControlFromPosition(i+ 1, 3) as TextBox;
                cb.DataBindings.Clear();
                tb.DataBindings.Clear();
                tb2.DataBindings.Clear();
                //非初始化加载时进行数据绑定
                if (bulkNo > 0)
                {
                    //数据绑定
                    if (robot == Robot.Left)
                    {
                        cb.Checked = Entity.BulkheadsLeftB[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsLeftB[serialNo].Beads[i], "IsWeld");
                        tb.Text = Entity.BulkheadsLeftB[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsLeftB[serialNo].Beads[i], "SerialNo");
                        tb2.Text = Entity.BulkheadsLeftB[serialNo].Beads[i].JobNo.ToString();
                        tb2.DataBindings.Add("Text", Entity.BulkheadsLeftB[serialNo].Beads[i], "JobNo");
                    }
                    else
                    {
                        cb.Checked = Entity.BulkheadsRightB[serialNo].Beads[i].IsWeld;
                        cb.DataBindings.Add("Checked", Entity.BulkheadsRightB[serialNo].Beads[i], "IsWeld");
                        tb.Text = Entity.BulkheadsRightB[serialNo].Beads[i].SerialNo.ToString();
                        tb.DataBindings.Add("Text", Entity.BulkheadsRightB[serialNo].Beads[i], "SerialNo");
                        tb2.Text = Entity.BulkheadsRightB[serialNo].Beads[i].JobNo.ToString();
                        tb2.DataBindings.Add("Text", Entity.BulkheadsRightB[serialNo].Beads[i], "JobNo");
                    }
                }
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
            ShowBeads();
            ParasBind();
            //todo:状态展示
            picLeftState.Image = Properties.Resources.Image2;
            picRightState.Image = Properties.Resources.Image2;
            picPLCState.Image = Properties.Resources.Image2;
            monitor.StartMonitor();
            monitor.subscription.DataChanged += new Opc.Da.DataChangedEventHandler(this.OnMonitorChange);
            monitor.Readall(ref PLCPara);
            DialogResult res = MessageBox.Show("是否打开上一次的项目","提示", MessageBoxButtons.YesNo);
            if(res == DialogResult.Yes)
            {
                ExcelAdapter.GetGirdersFromExcel(System.IO.Directory.GetCurrentDirectory() + @"\cache.xlsx", ref Entity);
            }
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
                BindBeadsB(Entity.BulkheadsLeftB[index].BulkHeadNo, index, (Robot)Entity.BulkheadsLeftB[index].Robot);
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
                BindBeadsB(Entity.BulkheadsRightB[index].BulkHeadNo, index, (Robot)Entity.BulkheadsRightB[index].Robot);
            }
        }
        /// <summary>
        /// 检测到OPC数据变化
        /// </summary>
        /// <param name="subscriptionHandle"></param>
        /// <param name="requestHandle"></param>
        /// <param name="values"></param>
        public void OnMonitorChange(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        {
            Type t1 = PLCPara.GetType();
            foreach (ItemValueResult item in values)
            {
                string key = item.ItemName.Split('.')[2];
                //更新数据绑定
                t1.GetField(key).SetValue(PLCPara,item.Value);
                //收到PLC请求信号
                if (key == "Request")
                {
                    isRequest = Convert.ToBoolean(item.Value);
                }
            }
            //发送数据
            if (isRequest && isReady)
            {
                SendBulkheadInfo();
            }

        }
        /// <summary>
        /// 发送数据
        /// </summary>
        public void SendBulkheadInfo()
        {
            if (PLCPara.Request)
            {
                int requestSource = PLCPara.RequestSource;//机器人选择
                int requestSerial = PLCPara.RequestSerial;//隔板顺序
                var paraList = new object();
                var bulkHead = new object();
                Type tUpper = upperPara.GetType();
                if (Entity.Type != "B")
                {
                    paraList = Entity.BulkheadParaA;
                    //遍历隔板中的所有属性赋值给待传参数
                    if (requestSource == (int)Robot.Left)
                    {
                        bulkHead = Entity.BulkheadsLeftA[requestSerial];

                    }
                    else
                    {
                        bulkHead = Entity.BulkheadsRightA[requestSerial];
                    }
                }
                else
                {
                    paraList = Entity.BulkheadParaB;
                    if (requestSource == (int)Robot.Left)
                    {
                        bulkHead = Entity.BulkheadsLeftB[requestSerial];
                    }
                    else
                    {
                        bulkHead = Entity.BulkheadsRightB[requestSerial];
                    }
                }
                //遍历PARA中的所有属性赋值给待传参数
                Type tPara = paraList.GetType();
                foreach (var propertity in tPara.GetProperties())
                {
                    tUpper.GetField(propertity.Name).SetValue(upperPara, Opc.Convert.ChangeType(propertity.GetValue(paraList), tUpper.GetField(propertity.Name).FieldType));
                }
                Type tBulkHead = bulkHead.GetType();
                //隔板属性
                foreach (var propertity in tBulkHead.GetProperties())
                {
                    if (propertity.Name != "Beads")
                    {
                        tUpper.GetField(propertity.Name).SetValue(upperPara, Opc.Convert.ChangeType(propertity.GetValue(bulkHead), tUpper.GetField(propertity.Name).FieldType));
                    }
                }
                //焊缝属性
                List<Bead> beads = tBulkHead.GetProperty("Beads").GetValue(bulkHead) as List<Bead>;
                for (int i = 0; i < beads.Count; i++)
                {
                    tUpper.GetField("IsWelding" + (i + 1)).SetValue(upperPara, Opc.Convert.ChangeType(beads[i].IsWeld, tUpper.GetField("IsWelding" + (i + 1)).FieldType));
                    tUpper.GetField("SerialNo" + (i + 1)).SetValue(upperPara, Opc.Convert.ChangeType(beads[i].SerialNo, tUpper.GetField("SerialNo" + (i + 1)).FieldType));
                    tUpper.GetField("JobNo" + (i + 1)).SetValue(upperPara, Opc.Convert.ChangeType(beads[i].JobNo, tUpper.GetField("JobNo" + (i + 1)).FieldType));
                }
                //写完成新号
                upperPara.WriteFinish = true;
                //写入数据
                if (!monitor.Write(upperPara))
                {
                    MessageBox.Show("写入环节失败，请检查OPC状态");
                    lbMessage.Text = "写入" + (requestSource == 1 ? "左":"右") + "侧第" + requestSerial.ToString() + "个隔板失败";
                }
                lbMessage.Text = "写入" + (requestSource == 1 ? "左" : "右") + "侧第" + requestSerial.ToString() + "个隔板成功,等待下一个请求";
                tBulkHead.GetField("IsSend").SetValue(bulkHead, true);
                isRequest = false;
                //判断是否全部发送完成
                int finishCount = 0;
                int totalCount = 0;
                foreach(var bulk in Entity.BulkheadsLeftA)
                {
                    totalCount++;
                    if(bulk.IsSend)
                    {
                        finishCount++;
                    }
                }
                foreach (var bulk in Entity.BulkheadsLeftB)
                {
                    totalCount++;
                    if (bulk.IsSend)
                    {
                        finishCount++;
                    }
                }
                foreach (var bulk in Entity.BulkheadsRightA)
                {
                    totalCount++;
                    if (bulk.IsSend)
                    {
                        finishCount++;
                    }
                }
                foreach (var bulk in Entity.BulkheadsRightB)
                {
                    totalCount++;
                    if (bulk.IsSend)
                    {
                        finishCount++;
                    }
                }
                if (totalCount == finishCount)
                {
                    lbMessage.Text = "全部隔板已经发送完毕";
                }
            }
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            isReady = true;
            lbMessage.Text = "已准备就绪，等待PLC发送请求";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string cachePath = System.IO.Directory.GetCurrentDirectory() + @"\cache";
            ExcelAdapter.SaveGirdersAsNewFile(cachePath, Entity);
        }
    }
}
