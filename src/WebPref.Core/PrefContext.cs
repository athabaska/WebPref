using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebPref.Core
{
    internal class PrefContext: DbContext
    {
        public PrefContext(DbContextOptions<PrefContext> options) : base(options)
        {
            
        }

        
    }
}
