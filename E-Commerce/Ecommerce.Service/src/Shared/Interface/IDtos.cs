using Ecommerce.Model.src;

namespace Ecommerce.Service.src.Shared.Interface
{
    public interface IReadDto<T>
        where T : BaseEntity
    {
        public int Id { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }

        public void FromEntity(T entity);
    }

    public interface IUpdateDto<T>
    {
        public int Id { get; set; }

        public void UpdateEntity(T entity);
    }

    public interface ICreateDto<T>
    {
        public void ToEntity(T entity);
    }

    
}
