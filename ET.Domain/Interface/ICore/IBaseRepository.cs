namespace ET.Domain.Interface.ICore;
public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> Insert(TEntity obj);
    Task Update(TEntity obj);
    Task Delete(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> FindById(Guid id);
}
