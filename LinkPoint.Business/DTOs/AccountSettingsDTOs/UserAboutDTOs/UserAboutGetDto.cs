﻿namespace LinkPoint.Business.DTOs.AccountSettingsDTOs.UserAboutDTOs;

public class UserAboutGetDto
{
    public int UserAboutId { get; set; }
    public string UserId { get; set; }
    public string? AboutMe { get; set; }
    public string? CityName { get; set; }
    public string? CountryName { get; set; }
    public bool Male { get; set; }
    public bool Female { get; set; }
    public DateTime BirthDate { get; set; }
}
