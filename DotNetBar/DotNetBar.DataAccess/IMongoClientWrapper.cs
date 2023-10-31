using MongoDB.Driver;

namespace DotNetBar.DataAccess;

public interface IMongoClientWrapper
{
    IMongoClient GetMongoClient();
}
