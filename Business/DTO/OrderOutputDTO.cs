using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTO
{
    public class OrderOutputDTO
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}