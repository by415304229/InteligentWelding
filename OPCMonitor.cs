using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteligentWelding.Entity;
using Opc;
using Opc.Da;

namespace InteligentWelding
{
    class OPCMonitor
    {
        private static string DriverName = "Driver1";
        private static string DeviceName = "Welding";
        private Opc.Da.Server m_server = null;//定义数据存取服务器
        public Opc.Da.Subscription subscription = null;//定义组对象（订阅者）
        private Opc.Da.SubscriptionState state = null;//定义组（订阅者）状态，相当于OPC规范中组的参数
        private Opc.IDiscovery m_discovery = new OpcCom.ServerEnumerator();//定义枚举基于COM服务器的接口，用来搜索所有的此类服务器。
        private readonly URL url = new URL("opcda://localhost/ZPMC.OPCServer.2");//本地服务器
        private List<Item> ls;
        private List<Item> GetItemList()
        {
            ls = new List<Item>();
            string query = @"SELECT * FROM OPCPara";
            DataTable dt = AccessHelper.ExecuteDataSet(query, null).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Item item = new Item();
                item.ClientHandle = Guid.NewGuid().GetHashCode();//使用int类型， 效率可能有提高
                //item.ItemPath = dr["OPCAddress"].ToString(); //该数据项在服务器中的路径。
                item.ItemName = DriverName + '.' + DeviceName + '.'  + dr["OPCName"].ToString(); //该数据项在服务器中的名字。
                item.MaxAgeSpecified = true;
                item.MaxAge = -1;
                ls.Add(item);
            }
            return ls;
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        private void Connect()
        {
            m_server = new Opc.Da.Server(new OpcCom.Factory(), url);
            m_server.Connect();
        }
        private void Disconnect()
        {
            m_server.Disconnect();
        }

        private void Subscript()
        {
            //设定组状态
            state = new Opc.Da.SubscriptionState();//组（订阅者）状态，相当于OPC规范中组的参数
            state.Name = "测试";//组名
            state.ServerHandle = null;//服务器给该组分配的句柄。
            //state.ClientHandle = Guid.NewGuid().ToString();//客户端给该组分配的句柄。
            state.ClientHandle = Guid.NewGuid().GetHashCode();//使用int类型， 效率可能有提高
            state.Active = true;//激活该组。
            state.UpdateRate = 1000;//刷新频率为1秒。
            state.Deadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组。
            state.Locale = null;//不设置地区值。

            //添加组
            subscription = (Opc.Da.Subscription)m_server.CreateSubscription(state);//创建组
            subscription.AddItems(GetItemList().ToArray());
            ////注册回调事件
            //subscription.DataChanged += new Opc.Da.DataChangedEventHandler(this.OnDataChange);
        }
        ////DataChange回调
        //private void OnDataChange(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        //{

        //}
        public void StartMonitor()
        {
            try
            {
                Connect();
                Subscript();
            }
            catch (Exception e)
            {
                MessageBox.Show(@"连接OPC服务器失败/n" + e.Message);
            }
        }
        public void Readall(ref PLCPara plcpara)
        {
            ItemValueResult[] res = subscription.Read(subscription.Items);
            System.Type tPLC = plcpara.GetType();
            foreach (ItemValueResult item in res)
            {
                string key = item.ItemName.Split('.')[2];
                //利用反射将更改的值绑定到实体
                //todo 可能存在异常情况
                if (null != tPLC.GetField(key))
                {
                    tPLC.GetField(key).SetValue(plcpara, Opc.Convert.ChangeType(item.Value, tPLC.GetField(key).FieldType));
                }
            }
        }
        public IdentifiedResult[] Write(UpperPara para)
        {
            System.Type t = para.GetType();
            FieldInfo[] infos = t.GetFields();
            List<ItemValue> items = new List<ItemValue>();
            foreach(FieldInfo info in infos)
            {
                ItemValue item = new ItemValue(subscription.Items.First(subItem => subItem.ItemName.Contains(info.Name)));
                item.Value = info.GetValue(para);
                items.Add(item);
            }
            try
            {
                return subscription.Write(items.ToArray());
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
