using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Models;

namespace CommandAPI.Data{

    public class CommandContext : DbContext{
        // public CommandContext(DbContextOptions<CommandContext> options)
        // :base(options)
        // {

        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        {     
        

                optionsBuilder.UseCosmos(
                    accountEndpoint: "https://testingreactaccount.documents.azure.com:443/",
                    accountKey:"xaSL0CouGKfn8hzM1sNlwnkYOHEPk2FQwnuSyxLilObxwc43kWwK843deJoUbeeWlnDZ6SXMYvTCECI8jQvDzA==",
                    databaseName:"Commands"

                    );
        
        }  

        public DbSet<Command> CommandItems { get; set; }

    }

}