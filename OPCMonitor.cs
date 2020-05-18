using System;
using System.Collections.Generic;
using System.Configuration;
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
        static string DriverName = ConfigurationManager.AppSettings["DriverName"];
        static string DeviceName = ConfigurationManager.AppSettings["Welding"];
        static URL url = new URL(ConfigurationManager.AppSettings["URL"]);//本地服务器
        private Opc.Da.Server m_server = null;//定义数据存取服务器
        public Opc.Da.Subscription subscription = null;//定义组对象（订阅者）
        private Opc.Da.SubscriptionState state = null;//定义组（订阅者）状态，相当于OPC规范中组的参数
        private Opc.IDiscovery m_discovery = new OpcCom.ServerEnumerator();//定义枚举基于COM服务器的接口，用来搜索所有的此类服务器。

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
            state.Name = "Welding";//组名
            state.ServerHandle = null;//服务器给该组分配的句柄。
            //state.ClientHandle = Guid.NewGuid().ToString();//客户端给该组分配的句柄。
            state.ClientHandle = Guid.NewGuid().GetHashCode();//使用int类型， 效率可能有提高
            state.Active = true;//激活该组。
            state.UpdateRate = 1000;//刷新频率为1秒。
            state.Deadband = 0;// 死区值，设为0时，服务器端该组内任何数据变化都通知组。
            state.Locale = null;//不设置地区值。

            //添加组
            subscription = (Opc.Da.Subscription)m_server.CreateSubscription(state);//创建组
            subscription.AddItems(AccessAdapter.GetOPCItemList().ToArray());
            ////注册回调事件
            //subscription.DataChanged += new Opc.Da.DataChangedEventHandler(this.OnDataChange);
        }
        ////DataChange回调
        //private void OnDataChange(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        //{

        //}
        /// <summary>
        /// 启动OPC Client
        /// </summary>
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
        /// <summary>
        /// 读取PLC发送的全部数据
        /// </summary>
        /// <param name="plcpara"></param>
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
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="para">上位机需要发送的参数</param>
        /// <returns></returns>
        public bool Write(UpperPara para)
        {
            System.Type t = para.GetType();
            FieldInfo[] infos = t.GetFields();
            List<ItemValue> items = new List<ItemValue>();
            foreach(FieldInfo info in infos)
            {
                ItemValue item = new ItemValue(subscription.Items.First(subItem => subItem.ItemName.EndsWith(info.Name)))
                {
                    Value = info.GetValue(para)
                };
                items.Add(item);
            }
            try
            {
                IdentifiedResult[] res = subscription.Write(items.ToArray());
                foreach (var re in res)
                {
                    if (!re.ResultID.Equals(Opc.ResultID.S_OK))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
