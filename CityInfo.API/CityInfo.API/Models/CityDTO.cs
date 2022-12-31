namespace CityInfo.API.Models
{
    public class CityDTO
    {
        public int CityID { get; set; }
        public string Name { get; set; } = string.Empty; //مقدار اولیه خالی میدهد
        public string? Description { get; set; } //اون فیلد را نال پذیر می کند

        public int  NumberOfPointsOfInterest 
        {
            get
            {
                return PointOfInterests.Count;
            }
        }    

        public ICollection<PointOfInterestDTO> PointOfInterests { get; set;}
            = new List<PointOfInterestDTO>();
    }

}
