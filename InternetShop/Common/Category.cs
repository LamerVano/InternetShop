﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Category: IModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
