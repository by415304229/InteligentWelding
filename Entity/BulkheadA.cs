using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 隔板参数
    /// </summary>
    public class BulkheadA: INotifyPropertyChanged
    {
        public BulkheadA()
        {
            Beads = new List<Bead>();
            for(int i =0; i< 40; i++)
            {
                Beads.Add(new Bead()
                {
                    BeadNo = i + 1
                });
            }
        }
        private int _bulkheadNo;
        private int _serialNo;
        private double _bulkheadSpace;
        private bool _isWelding;
        private bool _isSection1;
        private bool _isSection2;
        private bool _isSection3;
        private int _robot;
        private List<Bead> _beads;
        /// <summary>
        /// 隔板编号
        /// </summary>
        public int BulkHeadNo
        {
            get
            {
                return _bulkheadNo;
            }
            set
            {
                _bulkheadNo = value;
                this.SendChangeInfo("BulkHeadNo");
            }
        }
        public int SerialNo
        {
            get
            {
                return _serialNo;
            }
            set
            {
                _serialNo = value;
                this.SendChangeInfo("SerialNo");
            }
        }
        /// <summary>
        /// 隔板间距
        /// </summary>
        public double BulkheadSpace
        {
            get
            {
                return _bulkheadSpace;
            }
            set
            {
                _bulkheadSpace = value;
                this.SendChangeInfo("BulkheadSpace");
            }
        }
        /// <summary>
        /// 是否焊接
        /// </summary>
        public bool IsWelding
        {
            get
            {
                return _isWelding;
            }
            set
            {
                _isWelding = value;
                this.SendChangeInfo("IsWelding");
            }
        }
        /// <summary>
        /// 区域1是否进入
        /// </summary>
        public bool IsSection1
        {
            get
            {
                return _isSection1;
            }
            set
            {
                _isSection1 = value;
                this.SendChangeInfo("IsSection1");
            }
        }
        /// <summary>
        /// 区域2是否进入
        /// </summary>
        public bool IsSection2
        {
            get
            {
                return _isSection2;
            }
            set
            {
                _isSection2 = value;
                this.SendChangeInfo("IsSection2");
            }
        }
        /// <summary>
        /// 区域3是否进入
        /// </summary>
        public bool IsSection3
        {
            get
            {
                return _isSection3;
            }
            set
            {
                _isSection3 = value;
                this.SendChangeInfo("IsSection3");
            }
        }
        /// <summary>
        /// 机器人选择
        /// </summary>
        public int Robot
        {
            get
            {
                return _robot;
            }
            set
            {
                _robot = value;
                this.SendChangeInfo("Robot");
            }
        }
        /// <summary>
        /// 焊缝信息 key:焊缝编号，value:焊缝信息
        /// </summary>
        public List<Bead> Beads
        {
            get
            {
                return _beads;
            }
            set
            {
                _beads = value;
                this.SendChangeInfo("Beads");
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
