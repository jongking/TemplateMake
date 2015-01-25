using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMake
{
    public static class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            string conStr = Program.DbConfig.Trim();
            var cn = new SqlConnection(conStr);
            cn.Open();
            return cn;
        }

        public static void CloseConnection(SqlConnection cn)
        {
            if (cn == null) return;
            cn.Close();
            cn.Dispose();
            cn = null;
        }

        public static SqlTransaction StartTransaction(SqlConnection cn)
        {
            SqlTransaction tc = null;
            if (cn != null) tc = cn.BeginTransaction();
            return tc;
        }

        public static void CommitTransaction(SqlTransaction tc)
        {
            if (tc == null) return;
            tc.Commit();
            tc = null;
        }


        public static void RollBackTransaction(SqlTransaction tc)
        {
            if (tc == null) return;
            tc.Rollback();
            tc = null;
        }

        public static object ExeScalar(SqlConnection cn, SqlTransaction tc, string sql)
        {
            SqlCommand cmd = null;
            object result = null;
            try
            {
                cmd = new SqlCommand(sql, cn);
                if (tc != null) cmd.Transaction = tc;
                result = cmd.ExecuteScalar();
            }
            catch (SqlException sqlEx)
            {
                if (tc != null) DbHelper.RollBackTransaction(tc);
                throw sqlEx;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            if (result != null && result.Equals(DBNull.Value)) result = null;
            return result;
        }


        public static DataTable ExeQueryToDataTable(SqlConnection cn, SqlTransaction tc, string sql)
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand(sql, cn);
                if (tc != null) cmd.Transaction = tc;
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException sqlEx)
            {
                //DbHelper.rollBackTransaction(tc);
                throw sqlEx;
            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
            }
            return dt;
        }

        public static DataSet ExeQueryToDataSet(SqlConnection cn, SqlTransaction tc, string sql)
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand(sql, cn);
                if (tc != null) cmd.Transaction = tc;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
            }
            return ds;
        }

        public static DataSet ExeQueryToDataSet(IList<SqlTableNameEntity> l, SqlConnection cn, SqlTransaction tc)
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            for (int i = 0; i < l.Count; i++)
            {
                try
                {
                    cmd = new SqlCommand(l[i].SqlStr, cn);
                    if (tc != null) cmd.Transaction = tc;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, l[i].TableName);
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                finally
                {
                    if (da != null)
                    {

                        da.Dispose();
                        da = null;
                    }
                }
            }
            return ds;
        }

        public static DataSet ExeQueryToDataSet(Dictionary<string, string> dict, SqlConnection cn, SqlTransaction tc)
        {
            IDbCommand cmd = null;
            DbDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                foreach (KeyValuePair<string, string> kv in dict)
                {
                    cmd = new SqlCommand();
                    cmd.CommandTimeout = 99999;
                    cmd.Connection = cn;
                    cmd.Transaction = tc;
                    cmd.CommandText = kv.Value;
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    da.Fill(ds, kv.Key);
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null)
                {
                    da.Dispose();
                    da = null;
                }
                if (null != cmd)
                {
                    cmd.Dispose();
                }
            }
            return ds;
        }
        public static DataTable GetTableMsg(SqlConnection cn, SqlTransaction tc, string tablename)
        {
            var sql = string.Format("Select * from " +
                                    "(SELECT sys.syscolumns.name, sys.syscolumns.id, sys.syscolumns.xtype, sys.syscolumns.length, sys.syscolumns.isnullable FROM sys.syscolumns )" +
                                    " AS TableMsgV Where ID=OBJECT_ID('{0}')", tablename);
            return ExeQueryToDataTable(cn, tc, sql);
        }
    }

    public class SqlTableNameEntity
    {
        private string sqlStr;

        public string SqlStr
        {
            get { return sqlStr; }
            set { sqlStr = value; }
        }
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
    }
}
