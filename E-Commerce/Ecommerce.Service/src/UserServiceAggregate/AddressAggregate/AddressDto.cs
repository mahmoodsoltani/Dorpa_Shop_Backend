using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.AddressAggregate
{
    public class AddressReadDto : BaseReadDto<Address>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public override void FromEntity(Address entity)
        {
            Street = entity.Street;
            City = entity.City;
            PostalCode = entity.PostalCode;
            State = entity.State;
            base.FromEntity(entity);
        }
    }

    public class AddressCreateDto : ICreateDto<Address>
    {
        public string Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int UserId { get; set; }

        public void ToEntity(Address entity)
        {
            entity.Street = Street;
            entity.State = State;
            entity.City = City;
            entity.PostalCode = PostalCode;
            entity.UserId = UserId;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class AddressUpdateDto : IUpdateDto<Address>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public void UpdateEntity(Address entity)
        {
            entity.Street = Street ?? entity.Street;
            entity.State = State ?? entity.State;
            entity.City = City ?? entity.City;
            entity.PostalCode = PostalCode ?? entity.PostalCode;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class AddressUpdateValidator : IDataValidator<AddressUpdateDto>
    {
        public AddressUpdateValidator() { }
    }

    public class AddressCreateValidator : IDataValidator<AddressCreateDto>
    {
        public AddressCreateValidator() { }
    }
}
