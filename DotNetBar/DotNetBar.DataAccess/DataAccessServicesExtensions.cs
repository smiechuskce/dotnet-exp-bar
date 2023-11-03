using System.Text;
using DotNetBar.DataAccess.Config;
using DotNetBar.DataAccess.Models;
using DotNetBar.DataAccess.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace DotNetBar.DataAccess;

public static class DataAccessServicesExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BarManagementDatabaseSettings>(
            configuration.GetSection("BarManagementDatabase"));

        services.AddSingleton<IMongoClientWrapper, MongoClientWrapper>();
        services.AddTransient<BarsService>();

        AdjustSerialization();
        InitDb(services);

        return services;
    }

    private static void AdjustSerialization()
    {
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention()
        };

        ConventionRegistry.Register("Camel case convention", pack, t => true);
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        // Required to correctly set UUID instead of CSUUID which won't work
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
    }

    private static void InitDb(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbOptions = serviceProvider.GetRequiredService<IOptions<BarManagementDatabaseSettings>>();

        if (dbOptions.Value.Init)
        {
            var mongoClientWrapper = new MongoClientWrapper(dbOptions);
            var mongoClient = mongoClientWrapper.GetMongoClient();

            var db = mongoClient.GetDatabase(dbOptions.Value.DatabaseName);
            var collection = db.GetCollection<Bar>("Bar");

            var documents = BsonSerializer.Deserialize<IEnumerable<Bar>>(File.ReadAllText("data/bars_collection.json", Encoding.UTF8));

            try
            {
                collection.InsertMany(documents);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
