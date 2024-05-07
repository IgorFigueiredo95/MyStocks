using Dapper;
using Microsoft.Extensions.Configuration;
using MyStocks.Application.Shares;
using MyStocks.Application.Shares.Queries;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.SharesAggregate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace MyStocks.Infrastructure.Repositories.Query;

public class ShareQueryRepository : IShareQueryRepository
{
    private string ConnectionString { get;}
    public ShareQueryRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("Default") 
            ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<ShareResponse?> GetShareByCode(Guid OwnerId, string Code)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            var query = $@"SELECT 
                    ""Shares"".""Code"" Code,
                    ""Shares"".""Name"" Name,
                    ""Shares"".""Description"" Description,
                    ""Shares"".""ShareType"" ShareType,
                    ""CurrencyTypes"".""Code"" CurrencyType,
                    ""Shares"".""TotalValueInvested_Value"" TotalValueInvested,
                    ""Shares"".""TotalShares"" TotalShares,
                    ""Shares"".""AveragePrice_Value"" AveragePrice
                  FROM ""Shares""
                  INNER JOIN ""CurrencyTypes"" on ""CurrencyTypes"".""Id"" = ""Shares"".""TotalValueInvested_CurrencyTypeId""
                  WHERE ""Shares"".""Code"" = @code AND
                        ""Shares"".""OwnerId"" = @ownerId";
            
            connection.Open();
            var queryExecuted = await connection.QueryAsync<ShareResponse>(query, new { ownerId = OwnerId, code = Code });
               

            return queryExecuted.FirstOrDefault();
        }
    }

    public async Task<ShareDetailListDTO?> GetShareDetailListByCode(Guid OwnerId, string Code, int? Limit, int? Offset)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            var query = @"select
	                    ""Shares"".""Code"" Code,
	                    ""Shares"".""Name"" Name,
	                    ""Shares"".""Description"" Description,
	                    ""Shares"".""ShareType"" ShareType,
	                    ""CurrencyTypes"".""Code"" CurrencyCode,
	                    ""Shares"".""TotalValueInvested_Value"" TotalValueInvested,
	                    ""Shares"".""TotalShares"" TotalShares,
	                    ""Shares"".""AveragePrice_Value"" AveragePrice
                from
	                ""Shares""
                left join ""CurrencyTypes"" on
	                (""CurrencyTypes"".""Id"" = ""Shares"".""TotalValueInvested_CurrencyTypeId"")
                where
	                ""Shares"".""Code"" = @code AND
                    ""Shares"".""OwnerId"" = @ownerId;

                select
	                sd.""Id"" ShareDetailId,
	                sd.""OperationType"" OperationTypeCode,
	                sd.""Note"" Note,
	                sd.""Quantity"" Quantity,
	                sd.""Price_Value"" Price,
	                SD.""CreatedAt"" CreatedAt
                from
	                ""ShareDetails"" sd
                inner join ""Shares"" on
	                (""Shares"".""Id"" = sd.""ShareId"")
                where
	                ""Shares"".""Code"" = @code AND
                    ""Shares"".""OwnerId"" = @ownerId
                order by
	                sd.""CreatedAt"" desc
                limit @limit
	            offset @offset;";

            connection.Open();
            var queryExecuted = connection.QueryMultiple(query, new { ownerId = OwnerId, code = Code, limit = Limit, offset = Offset });
          
            var Share = queryExecuted.ReadFirstOrDefault<ShareDetailListDTO>();

            if (Share is not null)
                Share.SharesDetails = queryExecuted.Read<ShareDetailDTO>().ToList();

            return Share;
        }
    }

    public async Task<List<ShareResponse?>> GetSharesList(Guid OwnerId, int? Limit = 15, int? Offset = 0)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            var query =
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
                  INNER JOIN ""CurrencyTypes"" on ""CurrencyTypes"".""Id"" = ""Shares"".""TotalValueInvested_CurrencyTypeId""
                  WHERE ""Shares"".""OwnerId"" = @ownerId
                  limit @limit
                  offset @offset";

            connection.Open();
            var queryExecuted = await connection.QueryAsync<ShareResponse>(query, new { ownerId = OwnerId, limit = Limit, offset = Offset });

            return queryExecuted.ToList();

        }
    }
}
