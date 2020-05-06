using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 焊缝(AB型通用)
    /// </summary>
    public class Bead : INotifyPropertyChanged
    {
        /// <summary>
        /// 焊缝编号
        /// </summary>
        public int BeadNo;
        /// <summary>
        /// 隔板内焊接顺序
        /// </summary>
        public int SerialNo;
        /// <summary>
        /// 是否焊接
        /// </summary>
        public bool IsWeld;
        /// <summary>
        /// JOB号
        /// </summary>
        public int JobNo;
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
