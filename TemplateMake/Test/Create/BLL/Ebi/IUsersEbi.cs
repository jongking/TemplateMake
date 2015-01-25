using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Make.Model;

namespace Make.BLL.Ebi
{
    public interface IUsersEbi
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="users"></param>
        void add(UsersModel users);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="users"></param>
        void update(UsersModel users);
        /// <summary>
        /// 根据Id,删除
        /// </summary>
        /// <param name="usersId">操作员Id</param>
        void delete(IList<QueryModel> upmList);
        /// <summary>
        /// 根据qmList(查询条件模型)查询返回一个对象
        /// </summary>
UsersModel getModel(IList<QueryModel> qmlist);
        /// <summary>
        /// 根据qmList(查询条件模型)查询一组关于的数据,limit为正数，则查该数的记录数，为负数时，则查询全部。
        /// </summary>
        IList<UsersModel> getModelList(IList<QueryModel> qmList, int limit);
    }
}