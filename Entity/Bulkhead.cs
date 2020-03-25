using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 隔板参数
    /// </summary>
    class Bulkhead
    {
        public Bulkhead()
        {

        }
        /// <summary>
        /// 隔板编号
        /// </summary>
        public int serialNo;
        /// <summary>
        /// 隔板上表面到下表面高度(h)
        /// </summary>
        public int h;
        /// <summary>
        /// T型钢到隔板上表面高度(h1)
        /// </summary>
        public int h1;
        /// <summary>
        /// 隔板上表面到下表面高度(h11)	
        /// </summary>
        public int h11;
        /// <summary>
        /// T型钢到隔板下表面高度(h2)	
        /// </summary>
        public int h2;
        /// <summary>
        /// T型钢到隔板上表面高度(h22)	
        /// </summary>
        public int h22;
        /// <summary>
        /// 隔板上宽度(w1)	
        /// </summary>
        public int w1;
        /// <summary>
        /// 隔板下宽度(w2)	
        /// </summary>
        public int w2;
        /// <summary>
        /// 底板角钢到斜腹板距离(L1)	
        /// </summary>
        public int L1;
        /// <summary>
        /// 底板角钢到直腹板距离(L2)	
        /// </summary>
        public int L2;
        /// <summary>
        /// 隔板厚度(d)	
        /// </summary>
        public int d;
        /// <summary>
        /// 阿尔法角度(α)	
        /// </summary>
        public int alpha;
        /// <summary>
        /// 工位号(h3)	
        /// </summary>
        public int h3;

        /// <summary>
        /// 焊缝信息
        /// </summary>
        public List<Bead> beads;
    }
}
