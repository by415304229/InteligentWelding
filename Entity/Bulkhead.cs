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
        public Bulkhead(string type)
        {

        }
        /// <summary>
        /// 隔板高度
        /// </summary>
        public double H1;
        /// <summary>
        /// 隔板宽度

        /// </summary>
        public double W1;
        /// <summary>
        /// 隔板R孔1

        /// </summary>
        public double R1;
        /// <summary>
        /// 隔板R孔2

        /// </summary>
        public double R2;
        /// <summary>
        /// 隔板R孔3

        /// </summary>
        public double R3;
        /// <summary>
        /// 隔板R孔4

        /// </summary>
        public double R4;
        /// <summary>
        /// 隔板R孔5

        /// </summary>
        public double R5;
        /// <summary>
        /// 隔板R孔6

        /// </summary>
        public double R6;
        /// <summary>
        /// 隔板焊接高度

        /// </summary>
        public double H2;
        /// <summary>
        /// 隔板焊接宽度1

        /// </summary>
        public double L1;
        /// <summary>
        /// 隔板焊接宽度2

        /// </summary>
        public double L2;
        /// <summary>
        /// 隔板焊接宽度3

        /// </summary>
        public double L3;
        /// <summary>
        /// 隔板未焊接高度

        /// </summary>
        public double G1;
        /// <summary>
        /// 隔板未焊接高度

        /// </summary>
        public double G2;
        /// <summary>
        /// 加强板总高度

        /// </summary>
        public double H3;
        /// <summary>
        /// 加强板总宽度

        /// </summary>
        public double W3;
        /// <summary>
        /// 加强板R孔1

        /// </summary>
        public double R7;
        /// <summary>
        /// 加强板R孔2

        /// </summary>
        public double R8;
        /// <summary>
        /// 加强板R孔3

        /// </summary>
        public double R9;
        /// <summary>
        /// 加强板R孔4

        /// </summary>
        public double R10;
        /// <summary>
        /// 加强板焊接高度1

        /// </summary>
        public double H4;
        /// <summary>
        /// 加强板焊接高度2

        /// </summary>
        public double H5;
        /// <summary>
        /// 加强板焊接高度3

        /// </summary>
        public double H6;
        /// <summary>
        /// 隔板厚度
        /// </summary>
        public double T1;
        /// <summary>
        /// 加强板厚度
        /// </summary>
        public double T2;


        /// <summary>
        /// 焊缝信息 key:焊缝编号，value:焊缝信息
        /// </summary>
        public Dictionary<int, Bead> Beads;
    }
}
