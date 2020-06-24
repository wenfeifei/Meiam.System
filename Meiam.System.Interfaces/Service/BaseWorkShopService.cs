//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
//     author MEIAM
// </auto-generated>
//------------------------------------------------------------------------------
using Meiam.System.Model;
using Meiam.System.Model.Dto;
using Meiam.System.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;

namespace Meiam.System.Interfaces
{
    public class BaseWorkShopService : BaseService<Base_WorkShop>, IBaseWorkShopService
    {

        #region CustomInterface 
        /// <summary>
        /// 查询车间（分页）
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<WorkShopVM> QueryWorkShopPages(WorkShopQueryDto parm)
        {
            var source = Db.Queryable<Base_WorkShop, Sys_DataRelation, Base_Factory>((a, b, c) => new object[] {
                JoinType.Inner,a.ID == b.Form,
                JoinType.Inner,b.To == c.ID,
            })
            .WhereIF(!string.IsNullOrEmpty(parm.QueryText), (a, b, c) => a.WorkShopNo.Contains(parm.QueryText) || a.WorkShopName.Contains(parm.QueryText))
            .Select((a, b, c) => new WorkShopVM
            {
                ID = a.ID,
                WorkShopNo = a.WorkShopNo,
                WorkShopName = a.WorkShopName,
                Remark = a.Remark,
                Enable = a.Enable,
                FactoryUID = c.ID,
                FactoryNo = c.FactoryNo,
                FactoryName = c.FactoryName,
                CreateTime = a.CreateTime,
                UpdateTime = a.UpdateTime,
                CreateID = a.CreateID,
                CreateName = a.CreateName,
                UpdateID = a.UpdateID,
                UpdateName = a.UpdateName
            })
            .MergeTable()
            .OrderByIF(!string.IsNullOrEmpty(parm.Sort), $"{parm.OrderBy} {(parm.Sort == "descending" ? "desc" : "asc")}");

            return source.ToPage(parm.PageIndex, parm.PageSize);
        }

        /// <summary>
        /// 根据ID查询车间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkShopVM GetWorkShop(string id)
        {
            var source = Db.Queryable<Base_WorkShop, Sys_DataRelation, Base_Factory>((a, b, c) => new object[] {
                JoinType.Inner,a.ID == b.Form,
                JoinType.Inner,b.To == c.ID,
            }).Where((a, b, c) => a.ID == id)
            .Select((a, b, c) => new WorkShopVM
            {
                ID = a.ID,
                WorkShopNo = a.WorkShopNo,
                WorkShopName = a.WorkShopName,
                Remark = a.Remark,
                Enable = a.Enable,
                FactoryUID = c.ID,
                FactoryNo = c.FactoryNo,
                FactoryName = c.FactoryName,
                CreateTime = a.CreateTime,
                UpdateTime = a.UpdateTime,
                CreateID = a.CreateID,
                CreateName = a.CreateName,
                UpdateID = a.UpdateID,
                UpdateName = a.UpdateName
            })
            .MergeTable();

            return source.First();
        }

        /// <summary>
        /// 查询所有车间
        /// </summary>
        /// <returns></returns>
        public List<WorkShopVM> GetAllWorkShop(bool? enable = null)
        {
            var source = Db.Queryable<Base_WorkShop, Sys_DataRelation, Base_Factory>((a, b, c) => new object[] {
                JoinType.Inner,a.ID == b.Form,
                JoinType.Inner,b.To == c.ID,
            })
            .WhereIF(enable != null, (a, b, c) => a.Enable == enable)
            .Select((a, b, c) => new WorkShopVM
            {
                ID = a.ID,
                WorkShopNo = a.WorkShopNo,
                WorkShopName = a.WorkShopName,
                Remark = a.Remark,
                Enable = a.Enable,
                FactoryUID = c.ID,
                FactoryNo = c.FactoryNo,
                FactoryName = c.FactoryName,
                CreateTime = a.CreateTime,
                UpdateTime = a.UpdateTime,
                CreateID = a.CreateID,
                CreateName = a.CreateName,
                UpdateID = a.UpdateID,
                UpdateName = a.UpdateName
            }).MergeTable().OrderBy(m => m.WorkShopNo);

            return source.ToList();
        }

       /// <summary>
       /// 查询同工厂下是否存在相同车间编码
       /// </summary>
       /// <param name="workShopNo"></param>
       /// <param name="factoryId"></param>
       /// <returns></returns>
        public bool Any(string Id , string workShopNo, string factoryUID)
        {
            return Db.Queryable<Base_WorkShop, Sys_DataRelation>((a, b) => new object[] {
                JoinType.Inner,a.ID == b.Form
            })
            .Any((a, b) => a.ID != Id && a.WorkShopNo == workShopNo && b.To == factoryUID && b.Type == DataRelationType.WorkShop_To_Factory.ToString());
        }
        #endregion

    }
}
