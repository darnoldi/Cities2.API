using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cities2.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<Models.CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<Models.CityDto>()
            {
                new Models.CityDto()
                    {
                        Id = 1,
                        Name ="New York City",
                        Description = "The Big Apple"
                    },
             new Models.CityDto()
                    {
                        Id = 2,
                        Name ="Johannesburg",
                        Description = "Gatenger"
                    },
             new Models.CityDto()
                    {
                        Id = 3,
                        Name ="Adelaide",
                        Description = "Down Under"
                    }
            };

        }
    }
}
