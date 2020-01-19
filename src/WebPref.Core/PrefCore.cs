using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebPref.Core.Lobby;
using WebPref.Core.Playing;

namespace WebPref.Core
{
    public static class PrefCore
    {
        private static TableManager tableManager;
        private static PlayerManager playerManager;
        private static PrefContext prefContext;

        static PrefCore()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(AppContext.BaseDirectory);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            var dbOptionsBuilder = new DbContextOptionsBuilder<PrefContext>();
            var options = dbOptionsBuilder.UseSqlServer(connectionString).Options;

            //prefContext = new PrefContext(options);
            prefContext = new PrefContext();
            playerManager = new PlayerManager(prefContext);
            tableManager = new TableManager(prefContext, playerManager);
        }

        public static TableManager GetTableManager()
        {            
            return tableManager;
        }

        public static PlayerManager GetPlayerManager()
        {
            return playerManager;
        }
    }
}
