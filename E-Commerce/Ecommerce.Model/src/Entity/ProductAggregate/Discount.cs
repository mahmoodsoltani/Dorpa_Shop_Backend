using System;
using System.Collections.Generic;

namespace Ecommerce.Model.src.Entity.ProductAggregate
{
    public class Discount : BaseEntity
    {
        // Foreign Key to Product
        public int ProductId { get; set; }

        public decimal DiscountPercentage { get; set; }

        // Start date of the discount
        public DateTime StartDate { get; set; }

        // End date of the discount
        public DateTime EndDate { get; set; }

        // Navigation property for the Product entity
        public Product Product { get; set; }
    }
}
