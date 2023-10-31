
using CsvHelper.Configuration.Attributes;

namespace MedicalService.Data.Models;

public class VaccinesData
{
    [Name("COUNTRY")]
    public string? Country { get; set; }
    [Name("ISO3")]
    public string? Iso3 { get; set; }
    [Name("WHO_REGION")]
    public string? Region { get; set; }
    [Name("DATA_SOURCE")]
    public string? DataSource { get; set; }
    [Name("DATE_UPDATED")]
    public string? DateUpdated { get; set; }
    [Name("TOTAL_VACCINATIONS")]
    public double? TotalVaccinations { get; set; }
    [Name("PERSONS_VACCINATED_1PLUS_DOSE")]
    public double OnePlusDose { get; set; }
    [Name("TOTAL_VACCINATIONS_PER100")]
    public double? TotalVaccinationsPerHundred { get; set; }
    [Name("PERSONS_VACCINATED_1PLUS_DOSE_PER100")]
    public double? OnePlusDosePerHundred { get; set; }
    [Name("PERSONS_LAST_DOSE")]
    public double? PersonsLastDose { get; set; }
    [Name("PERSONS_LAST_DOSE_PER100")]
    public double? LastDosePerHundred { get; set; }
    [Name("VACCINES_USED")]
    public string? VaccinesUsed { get; set; }
    [Name("FIRST_VACCINE_DATE")]
    public string? FirstVaccineDate { get; set; }
    [Name("NUMBER_VACCINES_TYPES_USED")]
    public double? VaccinesTypesUsed { get; set; }
    [Name("PERSONS_BOOSTER_ADD_DOSE")]
    public double? PersonsBoosterDose { get; set; }
    [Name("PERSONS_BOOSTER_ADD_DOSE_PER100")]
    public double? PersonsBoosterDosePerHundred { get; set; }
}