namespace Pharmacy_Backend.DTOs
{
    public class IlaclarModelResponse: BaseApiResponse
    {
        public List<IlaclarModel> ilaclarModels {  get; set; }=new List<IlaclarModel>();
    }
}
