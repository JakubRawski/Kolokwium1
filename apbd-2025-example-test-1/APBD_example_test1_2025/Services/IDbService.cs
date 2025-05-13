using APBD_example_test1_2025.Models.DTOs;

namespace APBD_example_test1_2025.Services;

public interface IDbService //get
{
    Task<List<MergeDTO>> GetDeliveries(int userId);
    Task<int?> AddDelivery(PostDTO client);}
