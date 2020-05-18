using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteligentWelding.Entity;
using Opc.Da;

namespace InteligentWelding
{
    static class AccessAdapter
    {
        static string driverName = ConfigurationManager.AppSettings["DriverName"];
        static string deviceName = ConfigurationManager.AppSettings["Welding"];
        /// <summary>
        /// 获取OPC中的Item
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetOPCItemList()
        {
            List<Item> ls = new List<Item>();
            string query = @"SELECT * FROM OPCPara";
            DataTable dt = AccessHelper.ExecuteDataSet(query, null).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Item item = new Item();
                item.ClientHandle = Guid.NewGuid().GetHashCode();//使用int类型， 效率可能有提高
                //item.ItemPath = dr["OPCAddress"].ToString(); //该数据项在服务器中的路径。
                item.ItemName = driverName + '.' + deviceName + '.' + dr["OPCName"].ToString(); //该数据项在服务器中的名字。
                item.MaxAgeSpecified = true;
                item.MaxAge = -1;
                ls.Add(item);
            }
            return ls;
        }
        /// <summary>
        /// 记录项目开始时间
        /// </summary>
        /// <param name="girder"></param>
        public static void GirdStart(Girders girder)
        {
            string command = @"INSERT INTO Girders (Name,Type,WokkNo,StartTime) VALUES (@name,@type,@workNo,@startTime)";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("name", girder.Name));
            paras.Add(new OleDbParameter("type", girder.Type));
            paras.Add(new OleDbParameter("workNo", girder.WorkNo));
            paras.Add(new OleDbParameter("startTime", DateTime.Now));
            AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        }
        /// <summary>
        /// 项目完成时更新项目的结束时间
        /// </summary>
        /// <param name="girder"></param>
        public static void GirdEnd(Girders girder)
        {
            string command = @"UPDATE Girders SET EndTime = @endtime WHERE workNo = @workNo";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("workNo", girder.WorkNo));
            paras.Add(new OleDbParameter("endtime", DateTime.Now));
            AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        }
        /// <summary>
        /// 项目完成时更新项目的结束时间
        /// </summary>
        /// <param name="girder"></param>
        public static void BulkheadRecord(string workNo,BulkheadA bulkhead)
        {
            string command = @"INSERT INTO BulkheadRecord (WorkNo,SerialNo,DateTime) VALUES (@workNo,@serialNo,@dateTime)";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("workNo", workNo));
            paras.Add(new OleDbParameter("serialNo", bulkhead.SerialNo));
            paras.Add(new OleDbParameter("dateTime", DateTime.Now));
            AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        }
        /// <summary>
        /// 记录格挡生产情况
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="bulkhead"></param>
        public static void BulkheadRecord(string workNo, BulkheadB bulkhead)
        {
            string command = @"INSERT INTO BulkheadRecord (WorkNo,SerialNo,DateTime) VALUES (@workNo,@serialNo,@dateTime)";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("workNo", workNo));
            paras.Add(new OleDbParameter("serialNo", bulkhead.SerialNo));
            paras.Add(new OleDbParameter("dateTime", DateTime.Now));
            AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        }
        /// <summary>
        /// 记录设备故障日志
        /// </summary>
        /// <param name="equipment"></param>
        public static void ErrorLog(string equipment)
        {
            string command = @"INSERT INTO ErrorLog (Equipment,DateTime) VALUES (@equipment,@dateTime)";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("equipment", equipment));
            paras.Add(new OleDbParameter("dateTime", DateTime.Now));
            AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        }
        /// <summary>
        /// 根据日期获取当天生产的格挡数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetBulkheadCountByDate(DateTime startTime, DateTime endTime)
        {
            string command = @"SELECT COUNT(1) FROM BulkheadRecord WHERE DateTime > @startTime AND DateTime < @endTime";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("startTime", startTime));
            paras.Add(new OleDbParameter("endTime", endTime));
            DataSet ds = AccessHelper.ExecuteDataSet(command, paras.ToArray());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 根据日期获取当天生产的格挡信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetBulkheadByDate(DateTime startTime, DateTime endTime)
        {
            string command = @"SELECT * FROM BulkheadRecord WHERE DateTime > @startTime AND DateTime < @endTime";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("startTime", startTime));
            paras.Add(new OleDbParameter("endTime", endTime));
            DataSet ds = AccessHelper.ExecuteDataSet(command, paras.ToArray());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据日期获取当天生产的项目数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetGirderCountByDate(DateTime startTime, DateTime endTime)
        {
            string command = @"SELECT COUNT(1) FROM GirdersRecord WHERE StartTime > @startTime AND EndTime < @endTime";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("startTime", startTime));
            paras.Add(new OleDbParameter("endTime", endTime));
            DataSet ds = AccessHelper.ExecuteDataSet(command, paras.ToArray());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 根据日期获取当天生产的项目信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetGirderByDate(DateTime startTime, DateTime endTime)
        {
            string command = @"SELECT * FROM GirdersRecord WHERE StartTime > @startTime AND EndTime < @endTime";
            List<OleDbParameter> paras = new List<OleDbParameter>();
            paras.Add(new OleDbParameter("startTime", startTime));
            paras.Add(new OleDbParameter("endTime", endTime));
            DataSet ds = AccessHelper.ExecuteDataSet(command, paras.ToArray());
            return ds.Tables[0];
        }
        public static DataTable GetErrorLog()
        {
            string query = @"SELECT * FROM ErrorLog";
            return AccessHelper.ExecuteDataSet(query, null).Tables[0];
        }
        //public static void AddCache()
        //{
        //    string command = @"INSERT INTO Cache (CacheJSON) VALUES (@cache)";
        //    List<OleDbParameter> paras = new List<OleDbParameter>();
        //    paras.Add(new OleDbParameter("cache", "1"));
        //    AccessHelper.ExecuteNonQuery(command, paras.ToArray());
        //}
    }
}
