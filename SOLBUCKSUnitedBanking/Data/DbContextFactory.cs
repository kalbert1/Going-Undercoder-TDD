using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SOLBUCKSUnitedBanking.Data
{
    public class DbContextFactory : Disposable
    {
        ApplicationDbContext db;

        public ApplicationDbContext Create()
        {
            return db ?? (db = new ApplicationDbContext());
        }

        protected override void DisposeCore()
        {
            if (db != null)
                db.Dispose();
        }
    }
}
