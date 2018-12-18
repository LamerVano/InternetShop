﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICategory: IModel
    {
        int CategoryId { get; set; }
        string Name { get; set; }
    }
}
