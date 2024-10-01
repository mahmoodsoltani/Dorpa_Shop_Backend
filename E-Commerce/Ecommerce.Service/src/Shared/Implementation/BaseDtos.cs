using Ecommerce.Model.src;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.Shared.Implementation
{
    public class BaseReadDto<T> : IReadDto<T>
        where T : BaseEntity
    {
        public int Id { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }

        public virtual void FromEntity(T entity)
        {
            Id = entity.Id;
            Create_Date = entity.Create_Date;
            Update_Date = entity.Update_Date;
        }
    }

    public class BaseCreateDto<T> : ICreateDto<T>
        where T : BaseEntity, new()
    {
        public void ToEntity(T entity)
        {
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class BaseUpdateDto<T> : IUpdateDto<T>
        where T : BaseEntity, new()
    {
        public int Id { get; set; }

        public void UpdateEntity(T entity)
        {
            entity.Update_Date = DateTime.UtcNow;
        }
    }
}
