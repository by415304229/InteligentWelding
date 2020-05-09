using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// A型隔板参数
    /// </summary>
    /*
     * 项目中每个隔板参数相同不考虑隔板参数不同的情况
     * 因此直接放在上一个层级（Girders）中方便处理
     */
    public class BulkheadParaA : INotifyPropertyChanged
    {
        private double _h1;
        private double _w1;
        private double _h2;
        private double _l1;
        private double _l2;
        private double _l3;
        private double _r1;
        private double _r2;
        private double _r3;
        private double _r4;
        private double _h3;
        private double _w3;
        private double _h4;
        private double _h5;
        private double _h6;
        private double _r7;
        private double _r8;
        private double _r9;
        private double _r10;
        private double _t1;
        private double _t2;
        public double H1 {
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
        public double W1 {
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
        public double H2 {
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
        public double L1 {
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
        public double L2 {
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
        public double L3{
            get
            {
                return this._l3;
            }
            set
            {
                this._l3 = value;
                this.SendChangeInfo("L3");
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
        public double R2 {
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
        public double R3 {
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
        public double R4 {
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
        public double H3 {
            get
            {
                return this._h3;
            }
            set
            {
                this._h3 = value;
                this.SendChangeInfo("H3");
            }
        }
        public double W3 {
            get
            {
                return this._w3;
            }
            set
            {
                this._w3 = value;
                this.SendChangeInfo("W3");
            }
        }
        public double H4 {
            get
            {
                return this._h4;
            }
            set
            {
                this._h4 = value;
                this.SendChangeInfo("H4");
            }
        }
        public double H5 {
            get
            {
                return this._h5;
            }
            set
            {
                this._h5 = value;
                this.SendChangeInfo("H5");
            }
        }
        public double H6 {
            get
            {
                return this._h6;
            }
            set
            {
                this._h6 = value;
                this.SendChangeInfo("H6");
            }
        }
        public double R7
        {
            get
            {
                return this._r7;
            }
            set
            {
                this._r7 = value;
                this.SendChangeInfo("R7");
            }
        }
        public double R8
        {
            get
            {
                return this._r8;
            }
            set
            {
                this._r8 = value;
                this.SendChangeInfo("R8");
            }
        }
        public double R9
        {
            get
            {
                return this._r9;
            }
            set
            {
                this._r9 = value;
                this.SendChangeInfo("R9");
            }
        }
        public double R10
        {
            get
            {
                return this._r10;
            }
            set
            {
                this._r10 = value;
                this.SendChangeInfo("R10");
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
        public double T2
        {
            get
            {
                return this._t2;
            }
            set
            {
                this._t2 = value;
                this.SendChangeInfo("T2");
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
