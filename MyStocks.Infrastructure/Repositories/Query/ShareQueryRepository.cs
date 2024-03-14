using Dapper;
using Microsoft.Extensions.Configuration;
using MyStocks.Application.Shares;
using MyStocks.Application.Shares.Queries;
using MyStocks.Domain.SharesAggregate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

    public async Task<ShareDetailListDTO?> GetShareDetailListByCode(string Code, int? Limit = 15, int? Offset = 0)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            connection.Open();

            var result = await connection.QueryMultipleAsync(
               $@"select
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
	                ""Shares"".""Code"" = '{Code}';

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
	                ""Shares"".""Code"" = '{Code}'
                order by
	                sd.""CreatedAt"" desc
                limit {Limit}
	                offset {Offset};",
                new { ShareCode = Code, QueryLimit = Limit, QueryOffset = Offset });

            var Share = await result.ReadFirstAsync<ShareDetailListDTO>();
            Share.SharesDetails = result.ReadAsync<ShareDetailDTO>().Result.ToList();

            return Share;
        }
    }
}
