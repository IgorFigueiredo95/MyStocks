using Dapper;
using Microsoft.Extensions.Configuration;
using MyStocks.Application.Shares;
using MyStocks.Application.Shares.Queries;
using MyStocks.Domain.SharesAggregate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Repositories.Query;

public class ShareQueryRepository : IShareQueryRepository
{
    private string ConnectionString { get;}
    public ShareQueryRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("Default") 
            ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<ShareDTO?> GetShareById(Guid id)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            connection.Open();
            var result =  await connection.QueryAsync<ShareDTO>(
                $@"SELECT 
                    ""Shares"".""Code"" Code,
                    ""Shares"".""Name"" Name,
                    ""Shares"".""Description"" Description,
                    ""Shares"".""ShareType"" ShareType,
                    ""CurrencyTypes"".""Code"" CurrencyType,
                    ""Shares"".""TotalValueInvested_Value"" TotalValueInvested,
                    ""Shares"".""TotalShares"" TotalShares,
                    ""Shares"".""AveragePrice_Value"" AveragePrice
                  FROM ""Shares""
                  LEFT JOIN ""ShareDetails"" on ""Shares"".""Id"" = ""ShareDetails"".""ShareId""
                  INNER JOIN ""CurrencyTypes"" on ""CurrencyTypes"".""Id"" = ""Shares"".""TotalValueInvested_CurrencyTypeId""
                  WHERE ""Shares"".""Id"" = '{id}'");
            //Todo: inserir o id como parametro da query. evita SQl injection

            return result.FirstOrDefault();
        }
    }
}
