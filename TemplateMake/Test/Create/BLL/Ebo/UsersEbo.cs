using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Make.BLL.Ebi;
using Make.DAL.DalFactory;
using Make.DAL.Exception;
using Make.DAL.IDal;
using Make.DbHelper;
using Make.Exp;
using Make.Model;

namespace Make.BLL.Ebo
{
    internal class UsersEbo : IUsersEbi
    {
        private readonly IUsersDal usersDal = DataAccess.CreateInstance<IUsersDal>("UsersDal");

        public void add(UsersModel users)
        {
            SqlConnection cn = null;
            SqlTransaction tc = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                tc = DbHelperSQL.startTransaction(cn);

                //执行新增写进数据库
usersDal.add(cn, tc, users);

                DbHelperSQL.commitTransaction(tc);
            }
            catch (Exception dalEx)
            {
                DbHelperSQL.rollBackTransaction(tc);
                throw new MakeException(ExpSort.数据库, dalEx.Message);
            }
            finally
            {
                DbHelperSQL.closeConnection(cn);
            }
        }

        public void update(UsersModel users)
        {
            SqlConnection cn = null;
            SqlTransaction tc = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                tc = DbHelperSQL.startTransaction(cn);

usersDal.update(cn, tc, users);

                DbHelperSQL.commitTransaction(tc);
            }
            catch (Exception dalEx)
            {
                DbHelperSQL.rollBackTransaction(tc);
                throw new MakeException(ExpSort.数据库, dalEx.Message);
            }
            finally
            {
                DbHelperSQL.closeConnection(cn);
            }
        }

        public void delete(IList<QueryModel> qmList)
        {
            SqlConnection cn = null;
            SqlTransaction tc = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                tc = DbHelperSQL.startTransaction(cn);

usersDal.del(cn, tc, qmList);

                DbHelperSQL.commitTransaction(tc);
            }
            catch (Exception dalEx)
            {
                DbHelperSQL.rollBackTransaction(tc);

                throw new MakeException(ExpSort.数据库, dalEx.Message);
            }
            finally
            {
                DbHelperSQL.closeConnection(cn);
            }
        }

        public UsersModel getModel(IList<QueryModel> qmlist)
        {
            SqlConnection cn = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                return usersDal.getModel(cn, null, qmlist);
            }
            catch (Exception dalEx)
            {
                throw new MakeException(ExpSort.数据库, dalEx.Message);
            }
            finally
            {
                DbHelperSQL.closeConnection(cn);
            }
        }

        public IList<UsersModel> getModelList(IList<QueryModel> qmList, int limit)
        {
            SqlConnection cn = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                return usersDal.getModelList(cn, null, qmList, -1);
            }
            catch (Exception dalEx)
            {
                throw new MakeException(ExpSort.数据库, dalEx.Message);
            }
            finally
            {
                DbHelperSQL.closeConnection(cn);
            }
        }
    }
}