using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 大梁参数
    /// </summary>
    class Girders
    {
        /// <summary>
        /// 隔板数量
        /// </summary>
        public int bulkheadCount;
        /// <summary>
        /// 大梁总长度
        /// </summary>
        public int length;
        /// <summary>
        /// 斜板数量
        /// </summary>
        public int inclinedCount;
        /// <summary>
        /// 大梁种类
        /// </summary>
        public string type;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string name;
        /// <summary>
        /// 产品工号
        /// </summary>
        public string workNo;
        /// <summary>
        /// 构件名称
        /// </summary>
        public string componentName;
        /// <summary>
        /// 机器人选择
        /// </summary>
        public int robotNo;
        /// <summary>
        /// 工位选择
        /// </summary>
        public int stationNo;
        /// <summary>
        /// 焊丝种类
        /// </summary>
        public string wireType;
        /// <summary>
        /// 焊接顺序-隔板参数
        /// </summary>
        public Dictionary<int, Bulkhead> bulkheads;
        
    }
}
