using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Metering.Api.TableStorageEntities;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace Metering.Api.Services;

public class TableStorageService
{
    private const string TableName = "Measurements";

    private readonly IConfiguration configuration;

    public TableStorageService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    // public async Task<List<Measurement>> RetrieveAsync(DateTimeOffset from, DateTimeOffset to)
    // {
    //     var retrieveOperation = TableOperation.Retrieve<Measurement>(category, id);
    //     return await ExecuteTableOperation(retrieveOperation) as GroceryItemEntity;
    // }
    
    public async Task<Measurement> InsertOrMergeAsync(Measurement entity)
    {
        var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
        return await ExecuteTableOperation(insertOrMergeOperation) as Measurement;
    }
    
    private async Task<object> ExecuteTableOperation(TableOperation tableOperation)
    {
        var table = await GetCloudTable();
        var tableResult = await table.ExecuteAsync(tableOperation);
        return tableResult.Result;
    }

    private async Task<CloudTable> GetCloudTable()
    {
        var storageAccount = CloudStorageAccount.Parse(configuration["StorageConnectionString"]);
        var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        var table = tableClient.GetTableReference(TableName);
        await table.CreateIfNotExistsAsync();
        return table;
    }
}