using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DBConfiguration.EFCore
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {            
            context.Database.EnsureCreated();       
        }
    }
}
