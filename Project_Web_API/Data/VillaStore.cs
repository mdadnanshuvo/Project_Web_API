﻿using Project_Web_API.Models.DTO;

namespace Project_Web_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO {Id = 1, Name ="Pool View"},
             new VillaDTO {Id = 2, Name = "Beach View"}
        };
    }
}
