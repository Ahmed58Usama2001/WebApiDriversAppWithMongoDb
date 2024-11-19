using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiDriversAppWithMongoDb.Configurations;
using WebApiDriversAppWithMongoDb.Models;

namespace WebApiDriversAppWithMongoDb.Services;

public class DriverService
{
    private readonly IMongoCollection<Driver> _driverCollection;

    public DriverService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _driverCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
    }

    public async Task<List<Driver>> GetDriversAsync()=>await _driverCollection.Find(_=>true).ToListAsync();
    public async Task<Driver> GetDriverByIdAsync(string id)=>await _driverCollection.Find(d=>d.Id==id).FirstOrDefaultAsync();

    public async Task CreateDriverAsync(Driver driver)=> await _driverCollection.InsertOneAsync(driver);

    public async Task UpdataDriverAsync(Driver driver) => await _driverCollection.ReplaceOneAsync(d => d.Id == driver.Id, driver);

    public async Task DeleteDriverAsync(string id)=>await _driverCollection.DeleteOneAsync(d=>d.Id==id);
}
