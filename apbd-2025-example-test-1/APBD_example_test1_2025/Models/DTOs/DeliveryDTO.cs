namespace APBD_example_test1_2025.Models.DTOs;

public class ProductDto
{
    public int Product_Id { get; set; }
    public string Mane { get; set; }
    public decimal Price { get; set; }
}


public class DriverDTO
{
    public int Driver_Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string License_number { get; set; }
}

public class CustomerDTO
{
    public int Customer_Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string License_number { get; set; }
}

public class MergeDTO
{
    public int delivery_id { get; set; }
    public DateTime date { get; set; }
    public List<CustomerDTO> customers { get; set; }
    public List<DriverDTO> driveInfo { get; set; }
    public List<ProductDto> productInfo { get; set; }

}

public class PostDTO
{
    public int delivery_Id { get; set; }
    public int customer_id { get; set; }
    public string license_number { get; set; }
    public List<ProductDto> products { get; set; }
}