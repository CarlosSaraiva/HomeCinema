using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HomeCinemaContext _dbContext;

        public HomeCinemaContext Init()
        {
            return _dbContext ?? (_dbContext = new HomeCinemaContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose(); ;
        }
    }
}