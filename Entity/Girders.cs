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
        /// 大梁种类（A,B）
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
        /// <summary>
        /// 焊丝种类
        /// </summary>
        public string wireType;
        /// <summary>
        /// key:焊接顺序-隔板参数(A,B) 左侧机器人
        /// </summary>
        public Dictionary<int, Bulkhead> bulkheadsLeft;
        /// <summary>
        /// key:焊接顺序-隔板参数(A,B) 右侧机器人
        /// </summary>
        public Dictionary<int, Bulkhead> bulkheadsRight;
    }
}
