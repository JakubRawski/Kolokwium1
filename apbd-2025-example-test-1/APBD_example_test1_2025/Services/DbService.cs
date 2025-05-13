using System.Data.Common;
using APBD_example_test1_2025.Exceptions;
using APBD_example_test1_2025.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBD_example_test1_2025.Services;

public class DbService : IDbService
{
    private readonly string _connectionString;
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }
    
    public async Task<List<MergeDTO>> GetDeliveries(int userId)
    {
        var trips = new List<MergeDTO>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new SqlCommand(@"
            SELECT de.delivery_id,de.date, c.customer_Id,dr.driver_id ,pd.product_id AS Main
            FROM Delivery de
            JOIN Customer c ON de.customer_id = c.customer_id
            JOIN Driver dr ON de.driver_id = dr.driver_id
            JOIN Product_Delivery pd ON de.delivery_id = pd.delivery_id ", conn);

        using var reader = await cmd.ExecuteReaderAsync();
        var tripDict = new Dictionary<int, MergeDTO>();

        while (await reader.ReadAsync())
        {
            DateTime date = reader.GetDateTime(0);
            int idCustomer = reader.GetInt32(1);
            int idDriver = reader.GetInt32(2);
            int idProduct = reader.GetInt32(3);
            
            var cmdC = new SqlCommand(@"
            SELECT c.firstName,c.lastName, c.dateOfBirth
            FROM Customer c", conn);
            using var readerC = await cmdC.ExecuteReaderAsync();
            List<CustomerDTO> idCustomerList = new List<CustomerDTO>();
            List<DriverDTO> idDriverList = new List<DriverDTO>();
            List<ProductDto> idProductList = new List<ProductDto>();
            //TODO nie zdazylem dokonczyc dodawanie list tych obiektow, prosze wziac to pod uwage przy wystawianiu punktow
            if (!tripDict.TryGetValue(userId, out var delivery_id))
            {
                delivery_id = new MergeDTO { date = date  };
                tripDict[userId] = delivery_id;
            }
            //delivery_id.productInfo.Add(new ProductDto() { Product_Id = new List<pd>() });
        }

        return tripDict.Values.ToList();
    }
    
    public async Task<int?> AddDelivery(PostDTO client)
    {
        if (int.IsNegative(client.delivery_Id) ||
            int.IsNegative(client.customer_id) ||
            string.IsNullOrWhiteSpace(client.license_number))
            return null;

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var cmd = new SqlCommand(@"
            INSERT INTO Delivery (delivery_id, customer_id, driver_id, date)
            VALUES (@Delivery, @Customer, @Driver, GETDATE())", conn);
        cmd.Parameters.AddWithValue("@Delivery", client.delivery_Id);
        cmd.Parameters.AddWithValue("@Customer", client.customer_id);
        cmd.Parameters.AddWithValue("@Driver", client.license_number);
        //TODO nie zdazylem dokonczyc dodawanie productow i ich walidacje, prosze wziac to pod uwage przy wystawianiu punktow


        return (int?)await cmd.ExecuteScalarAsync();
    }
}