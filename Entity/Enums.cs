using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligentWelding.Entity
{
    /// <summary>
    /// 隔板类型（A1,B2）
    /// </summary>
    public enum BulkheadType
    {
        A = 1,
        B = 2
    }
    /// <summary>
    /// 机器人(左1，右2)
    /// </summary>
    public enum Robot
    {                                                                                  
        Left = 1,
        Right = 2
    }
    /// <summary>
    /// PLC请求状态（请求中1，收到2）
    /// </summary>
    public enum PLCRequestState
    {
        Request = 1,
        Ok = 2
    }
    /// <summary>
    /// 工作站紧停（正常1，故障0）
    /// </summary>
    public enum WorkStationBreak
    {
        Normal = 1,
        Break = 0
    }
    /// <summary>
    /// 机器人工作中（工作中1，空闲0）
    /// </summary>
    public enum RobotWoring
    {
        Working = 1,
        Free = 0
    }
    /// <summary>
    /// 机器人状态（正常1，故障2，暂停3）
    /// </summary>
    public enum RobotState
    {
        Normal = 1,
        Break = 2,
        Suspend = 3
    }
    /// <summary>
    /// 气体压力（正常1，故障2）
    /// </summary>
    public enum GasPressure
    {
        Normal = 1,
        Break = 2
    }
}
