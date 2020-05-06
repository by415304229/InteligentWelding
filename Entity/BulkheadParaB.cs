using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// B型隔板参数
    /// </summary>
    /*
     * 项目中每个隔板参数相同不考虑隔板参数不同的情况
     * 因此直接放在上一个层级（Girders）中方便处理
     */
    public class BulkheadParaB : INotifyPropertyChanged
    {
        private double _h1;
        private double _w1;
        private double _h2;
        private double _l1;
        private double _l2;
        private double _g1;
        private double _g2;
        private double _r1;
        private double _r2;
        private double _r3;
        private double _r4;
        private double _r5;
        private double _r6;
        private double _t1;
        public double H1
        {
            get
            {
                return this._h1;
            }
            set
            {
                this._h1 = value;
                this.SendChangeInfo("H1");
            }
        }
        public double W1
        {
            get
            {
                return this._w1;
            }
            set
            {
                this._w1 = value;
                this.SendChangeInfo("W1");
            }
        }
        public double H2
        {
            get
            {
                return this._h2;
            }
            set
            {
                this._h2 = value;
                this.SendChangeInfo("H2");
            }
        }
        public double L1
        {
            get
            {
                return this._l1;
            }
            set
            {
                this._l1 = value;
                this.SendChangeInfo("L1");
            }
        }
        public double L2
        {
            get
            {
                return this._l2;
            }
            set
            {
                this._l2 = value;
                this.SendChangeInfo("L2");
            }
        }
        public double G1
        {
            get
            {
                return this._g1;
            }
            set
            {
                this._g1 = value;
                this.SendChangeInfo("G1");
            }
        }
        public double G2
        {
            get
            {
                return this._g2;
            }
            set
            {
                this._g2 = value;
                this.SendChangeInfo("G2");
            }
        }
        public double R1
        {
            get
            {
                return this._r1;
            }
            set
            {
                this._r1 = value;
                this.SendChangeInfo("R1");
            }
        }
        public double R2
        {
            get
            {
                return this._r2;
            }
            set
            {
                this._r2 = value;
                this.SendChangeInfo("R2");
            }
        }
        public double R3
        {
            get
            {
                return this._r3;
            }
            set
            {
                this._r3 = value;
                this.SendChangeInfo("R3");
            }
        }
        public double R4
        {
            get
            {
                return this._r4;
            }
            set
            {
                this._r4 = value;
                this.SendChangeInfo("R4");
            }
        }
        public double R5
        {
            get
            {
                return this._r5;
            }
            set
            {
                this._r5 = value;
                this.SendChangeInfo("R5");
            }
        }
        public double R6
        {
            get
            {
                return this._r6;
            }
            set
            {
                this._r6 = value;
                this.SendChangeInfo("R6");
            }
        }
        public double T1
        {
            get
            {
                return this._t1;
            }
            set
            {
                this._t1 = value;
                this.SendChangeInfo("T1");
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
