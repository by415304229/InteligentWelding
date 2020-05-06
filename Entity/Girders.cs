using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 大梁参数
    /// </summary>
    public class Girders: INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Girders()
        {
        }
        private int _bulkheadCount;
        private string _type;
        private string _name;
        private string _workNo;
        private string _wireType;
        private Bitmap _picture;
        private Dictionary<int, Bulkhead> _bulkheadsLeft;
        private Dictionary<int, Bulkhead> _bulkheadsRight;
        private BulkheadParaA _bulkheadParaA;
        private BulkheadParaB _bulkheadParaB;

        /// <summary>
        /// 隔板数量
        /// </summary>
        public int BulkheadCount
        {
            get
            {
                return _bulkheadCount;
            }
            set
            {
                _bulkheadCount = value;
                this.SendChangeInfo("BulkheadCount");
            }
        }
        /// <summary>
        /// 大梁种类（A,B）
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                this.SendChangeInfo("Type");
            }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                this.SendChangeInfo("Name");
            }
        }
        /// <summary>
        /// 产品工号
        /// </summary>
        public string WorkNo
        {
            get
            {
                return _workNo;
            }
            set
            {
                _workNo = value;
                this.SendChangeInfo("WorkNo");
            }
        }
        /// <summary>
        /// <summary>
        /// 焊丝种类
        /// </summary>
        public string WireType
        {
            get
            {
                return _wireType;
            }
            set
            {
                _wireType = value;
                this.SendChangeInfo("WireType");
            }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public Bitmap Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                this.SendChangeInfo("Picture");
            }
        }
        /// <summary>
        /// key:焊接顺序-隔板参数(A,B) 左侧机器人
        /// </summary>
        public Dictionary<int, Bulkhead> BulkheadsLeft
        {
            get
            {
                return _bulkheadsLeft;
            }
            set
            {
                _bulkheadsLeft = value;
                this.SendChangeInfo("BulkheadsLeft");
            }
        }
        /// <summary>
        /// key:焊接顺序-隔板参数(A,B) 右侧机器人
        /// </summary>
        public Dictionary<int, Bulkhead> BulkheadsRight
        {
            get
            {
                return _bulkheadsRight;
            }
            set
            {
                _bulkheadsRight = value;
                this.SendChangeInfo("BulkheadsRight");
            }
        }
        /// <summary>
        /// A型隔板参数
        /// </summary>
        public BulkheadParaA BulkheadParaA
        {
            get
            {
                return _bulkheadParaA;
            }
            set
            {
                _bulkheadParaA = value;
                this.SendChangeInfo("BulkheadParaA");
            }
        }
        /// <summary>
        /// B型隔板参数
        /// </summary>
        public BulkheadParaB BulkheadParaB
        {
            get
            {
                return _bulkheadParaB;
            }
            set
            {
                _bulkheadParaB = value;
                this.SendChangeInfo("BulkheadParaB");
            }
        }

        /// <summary>
        /// 设置变化的属性
        /// </summary>
        /// <param name="propertyName"></param>
        private void SendChangeInfo(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 属性变化的事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
