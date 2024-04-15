﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Categories
{
    public record CreateCategoryDto
    {
        public string Title { get; init; } = null!;
    }
}
