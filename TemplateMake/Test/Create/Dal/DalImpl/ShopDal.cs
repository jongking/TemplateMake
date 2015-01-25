using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Make.DbHelper;
using Make.Model;
using Make.DAL.IDal;

namespace Make.DAL.DalImpl
{
    class ShopDal : IShopDal
    {
        public bool add(SqlConnection cn, SqlTransaction tc, ShopModel model)
        {
            return DbHelperSQL.insertModel<ShopModel>(model, "Shop", cn, tc);
        }

        public bool update(SqlConnection cn, SqlTransaction tc, ShopModel model)
        {
            return DbHelperSQL.updateModel<ShopModel>(model, "Shop", "UserID", model.Evenid, "'", cn, tc);
        }

        public bool del(SqlConnection cn, SqlTransaction tc, IList<QueryModel> ilist)
        {
            return DbHelperSQL.delRecord(ilist, "Shop", cn, tc);
        }

        public ShopModel getModel(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist)
        {
            return DbHelperSQL.getFromQueryModel<ShopModel>(qmlist, "Shop", cn, tc);
        }

        public IList<ShopModel> getModelList(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist, int limitCount)
        {
            return DbHelperSQL.getListFromQueryModel<ShopModel>(qmlist, "Shop", limitCount, cn, tc);
        }
    }
}
