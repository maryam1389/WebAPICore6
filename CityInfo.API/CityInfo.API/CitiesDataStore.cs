using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDTO> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO() {CityID=1, Name ="Tehran",
                Description ="this is my City",
                PointOfInterests= new List<PointOfInterestDTO>()
                  {
                    new PointOfInterestDTO()
                    {
                        PointID=1,
                        Name="jaye didani",
                        Description="this is yaye didani 1"
                        
                    },

                    new PointOfInterestDTO()
                    {
                        PointID=2,
                        Name="jaye didani",
                        Description="this is yaye didani 2"

                    }
                 }
                },

                new CityDTO() {CityID=2, Name ="Shiraz",
                Description ="this is my City",
                PointOfInterests= new List<PointOfInterestDTO>()
                  {
                    new PointOfInterestDTO()
                    {
                        PointID=3,
                        Name="jaye didani",
                        Description="this is yaye didani 3"

                    },
                    new PointOfInterestDTO()
                    {
                        PointID=4,
                        Name="jaye didani",
                        Description="this is yaye didani 4"

                    }
                 }
                },

                new CityDTO() {CityID=3, Name ="Ahwaz",
                Description ="this is my City",
                PointOfInterests= new List<PointOfInterestDTO>()
                {
                    new PointOfInterestDTO()
                    {
                        PointID=5,
                        Name="jaye didani",
                        Description="this is yaye didani 5"

                    },
                    new PointOfInterestDTO()
                    {
                        PointID=6,
                        Name="jaye didani",
                        Description="this is yaye didani 6"

                    },
                 }
                }
            };
        }
    }
}
