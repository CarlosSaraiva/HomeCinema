using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    internal class GenreConfiguration : EntityBaseConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
    }
}