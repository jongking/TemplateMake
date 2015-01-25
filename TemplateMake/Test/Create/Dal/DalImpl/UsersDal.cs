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
    class UsersDal : IUsersDal
    {
        public bool add(SqlConnection cn, SqlTransaction tc, UsersModel model)
        {
            return DbHelperSQL.insertModel<UsersModel>(model, "Users", cn, tc);
        }

        public bool update(SqlConnection cn, SqlTransaction tc, UsersModel model)
        {
            return DbHelperSQL.updateModel<UsersModel>(model, "Users", "UserID", model.Evenid, "'", cn, tc);
        }

        public bool del(SqlConnection cn, SqlTransaction tc, IList<QueryModel> ilist)
        {
            return DbHelperSQL.delRecord(ilist, "Users", cn, tc);
        }

        public UsersModel getModel(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist)
        {
            return DbHelperSQL.getFromQueryModel<UsersModel>(qmlist, "Users", cn, tc);
        }

        public IList<UsersModel> getModelList(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist, int limitCount)
        {
            return DbHelperSQL.getListFromQueryModel<UsersModel>(qmlist, "Users", limitCount, cn, tc);
        }
    }
}
