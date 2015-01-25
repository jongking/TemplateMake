using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Make.Model;
using Make.DTO;

namespace Make.DAL.IDal
{
    public interface IShopDal
    {
        /// <summary>
        /// 增加资料
        /// </summary>
        bool add(SqlConnection cn, SqlTransaction tc, ShopModel model);
        /// <summary>
        /// 修改资料
        /// </summary>
        bool update(SqlConnection cn, SqlTransaction tc, ShopModel model);
        /// <summary>
        /// 删除资料
        /// </summary>
        bool del(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist);
        /// <summary>
        /// 查询资料
        /// </summary>
ShopModel getModel(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist);
        /// <summary>
        /// 查询资料集
        /// </summary>
        IList<ShopModel> getModelList(SqlConnection cn, SqlTransaction tc, IList<QueryModel> qmlist, int limitCount);
    }
}