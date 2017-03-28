using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cities2.API.Models
{
    public class PointOfInterestForCreationDto
    {

        [Required(ErrorMessage = "Field: 'Name' is required.")]
        [MaxLength(50)] // has a default error message
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }



    }
}
