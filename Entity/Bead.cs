using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 焊缝(AB型通用)
    /// </summary>
    class Bead
    {
        /// <summary>
        /// 隔板内焊接顺序
        /// </summary>
        public int serialNo;
        /// <summary>
        /// 是否焊接
        /// </summary>
        public bool isWeld;
        /// <summary>
        /// JOB号
        /// </summary>
        public int jobNo;
        /// <summary>
        /// 焊接位置
        /// </summary>
        public string position;
        /// <summary>
        /// 焊接模式
        /// </summary>
        public string mode;
        /// <summary>
        /// 焊脚尺寸
        /// </summary>
        public string size;
    }
}
