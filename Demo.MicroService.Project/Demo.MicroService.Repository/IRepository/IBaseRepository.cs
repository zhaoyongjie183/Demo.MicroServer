using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.Model;
using SqlSugar;
using System.Data;
using System.Dynamic;
using System.Linq.Expressions;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 10:17:20                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Repository.IRepository                              
*│　类    名： IBaseRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Repository.IRepository
{
    public interface IBaseRepository<T> : IDependency where T : BaseEntity,new()
    {
        #region add
        Task<bool> InsertAsync(T t, bool IgnoreNullColumn = true);
        Task<bool> InsertIgnoreNullColumnAsync(T t, params string[] columns);
        bool Insert(T t, bool IgnoreNullColumn = true);

        bool InsertIgnoreNullColumn(T t);

        bool InsertIgnoreNullColumn(T t, params string[] columns);

        bool Insert(SqlSugarClient client, T t);

        long InsertBigIdentity(T t);

        bool Insert(List<T> t);

        bool InsertIgnoreNullColumn(List<T> t);

        bool InsertIgnoreNullColumn(List<T> t, params string[] columns);

        DbResult<bool> InsertTran(T t);

        DbResult<bool> InsertTran(List<T> t);

        T InsertReturnEntity(T t);
        Task<T> InsertReturnEntityAsync(T t);

        T InsertReturnEntity(T t, string sqlWith = SqlWith.UpdLock);

        bool ExecuteCommand(string sql, object parameters);

        bool ExecuteCommand(string sql, params SugarParameter[] parameters);

        bool ExecuteCommand(string sql, List<SugarParameter> parameters);

        #endregion add

        #region update

        bool UpdateEntity(T entity);

        bool Update(T entity, Expression<Func<T, bool>> expression);

        /// <summary>
        /// 只更新表达式的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Update(T entity, Expression<Func<T, object>> expression);

        bool Update(T entity, Expression<Func<T, object>> expression, Expression<Func<T, bool>> where);

        bool Update(SqlSugarClient client, T entity, Expression<Func<T, object>> expression, Expression<Func<T, bool>> where);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity">T</param>
        /// <param name="list">忽略更新</param>
        /// <param name="isNull">Null不更新</param>
        /// <returns></returns>
        bool Update(T entity, List<string> list = null, bool isNull = true);

        bool Update(List<T> entity);

        #endregion update

        #region Tran
        DbResult<bool> UseTran(Action action);

        DbResult<bool> UseTran(SqlSugarClient client, Action action);

        bool UseTran2(Action action);
        #endregion

        #region delete

        bool Delete(Expression<Func<T, bool>> expression);

        bool Delete<PkType>(PkType[] primaryKeyValues);

        bool Delete(object obj);

        #endregion delete

        #region query

        bool IsAny(Expression<Func<T, bool>> expression);

        ISugarQueryable<T> Queryable();

        ISugarQueryable<ExpandoObject> Queryable(string tableName, string shortName);

        //ISugarQueryable<T, T1, T2> Queryable<T1, T2>() where T1 : class where T2 : new();

        List<T> QueryableToList(Expression<Func<T, bool>> expression);

        Task<List<T>> QueryableToListAsync(Expression<Func<T, bool>> expression);

        string QueryableToJson(string select, Expression<Func<T, bool>> expressionWhere);

        List<T> QueryableToList(string tableName);

        T QueryableToEntity(Expression<Func<T, bool>> expression);

        List<T> QueryableToList(string tableName, Expression<Func<T, bool>> expression);

        (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, int pageIndex = 0, int pageSize = 10);

        (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, string order, int pageIndex = 0, int pageSize = 10);

        (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderFiled, string orderBy, int pageIndex = 0, int pageSize = 10);

        (List<T>, int) QueryableToPage(Expression<Func<T, bool>> expression, PageParams bootstrap);

        List<T> SqlQueryToList(string sql, object obj = null);

        #endregion query

        #region Procedure

        DataTable UseStoredProcedureToDataTable(string procedureName, List<SugarParameter> parameters);

        (DataTable, List<SugarParameter>) UseStoredProcedureToTuple(string procedureName, List<SugarParameter> parameters);

        #endregion Procedure

        #region Async
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> DeleteAsync(T deleteObj);
        Task<bool> DeleteAsync(List<T> deleteObjs);
        Task<bool> DeleteByIdAsync(dynamic id);
        Task<bool> DeleteByIdsAsync(dynamic[] ids);
        Task<T> GetByIdAsync(dynamic id);
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page);
        Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> InsertAsync(T insertObj);
        Task<bool> InsertOrUpdateAsync(T data);
        Task<bool> InsertOrUpdateAsync(List<T> datas);
        Task<bool> InsertRangeAsync(List<T> insertObjs);
        Task<bool> InsertRangeAsync(T[] insertObjs);
        Task<int> InsertReturnIdentityAsync(T insertObj);
        Task<long> InsertReturnBigIdentityAsync(T insertObj);
        Task<long> InsertReturnSnowflakeIdAsync(T insertObj);
        Task<List<long>> InsertReturnSnowflakeIdAsync(List<T> insertObjs);
       

        Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression);
        Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        Task<bool> UpdateAsync(T updateObj);
        Task<bool> UpdateRangeAsync(List<T> updateObjs);
        Task<bool> UpdateRangeAsync(T[] updateObjs);
        #endregion
    }
}
