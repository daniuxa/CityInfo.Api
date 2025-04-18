﻿namespace CityInfo.Api.Models
{
    /// <summary>
    /// A DTO for a city without points of interest
    /// </summary>
    public class CityWithoutPointsOfInerestDto
    {
        /// <summary>
        /// The id of the city
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the city
        /// </summary>
        public string Name { get; set; } = String.Empty;
        /// <summary>
        /// The description of the city
        /// </summary>
        public string? Description { get; set; }
    }
}
