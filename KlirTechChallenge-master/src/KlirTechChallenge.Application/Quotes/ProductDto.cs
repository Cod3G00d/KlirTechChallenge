﻿using System;
using System.ComponentModel.DataAnnotations;

namespace KlirTechChallenge.Application.Quotes
{
    public  class ProductDto
    {
        [Required(ErrorMessage = "The {0} field is mandatory")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "The {0} field is mandatory")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Price value is invalid")]
        public int Quantity { get; init; }

        public ProductDto(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}