using System.Collections.Generic;
using CommandAPI.Models;
using System.Linq;
  using Microsoft.Azure.Cosmos;

namespace CommandAPI.Data
{
    public class SqlCommandAPIRepo : ICommandAPIRepo
    {

 private Container _container;

        public SqlCommandAPIRepo(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public void CreateCommand(Command cmd)
        {
             throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
             throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {

            throw new System.NotImplementedException();
        }

        public Command GetCommandById(string id)
        {
             throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}