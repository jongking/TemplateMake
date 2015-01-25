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
    internal class ShopEbo : IShopEbi
    {
        private readonly IShopDal shopDal = DataAccess.CreateInstance<IShopDal>("ShopDal");

        public void add(ShopModel shop)
        {
            SqlConnection cn = null;
            SqlTransaction tc = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                tc = DbHelperSQL.startTransaction(cn);

                //执行新增写进数据库
shopDal.add(cn, tc, shop);

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

        public void update(ShopModel shop)
        {
            SqlConnection cn = null;
            SqlTransaction tc = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                tc = DbHelperSQL.startTransaction(cn);

shopDal.update(cn, tc, shop);

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

shopDal.del(cn, tc, qmList);

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

        public ShopModel getModel(IList<QueryModel> qmlist)
        {
            SqlConnection cn = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                return shopDal.getModel(cn, null, qmlist);
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

        public IList<ShopModel> getModelList(IList<QueryModel> qmList, int limit)
        {
            SqlConnection cn = null;
            try
            {
                cn = DbHelperSQL.getConnection();
                return shopDal.getModelList(cn, null, qmList, -1);
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