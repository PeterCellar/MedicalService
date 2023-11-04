using CsvHelper.Configuration.Attributes;

namespace MedicalService.Data.Models.VaccinesMetadata;
class VaccinesMetadata
{
  [Name("ISO3")]
  public string Iso3 { get; set; }
  
  [Name("VACCINE_NAME")]
  public string VaccineName { get; set; }
  
  [Name("PRODUCT_NAME")]
  public string ProductName { get; set; }
  
  [Name("COMPANY_NAME")]
  public string CompanyName { get; set; }

  [Name("FIRST_VACCINE_DATE")]
  public string FirstVaccineDate { get; set; }

  [Name("AUTHORIZATION_DATE")]
  public string AuthorizationDate { get; set; }

  [Name("START_DATE")]
  public string StartDate { get; set; }

  [Name("END_DATE")]
  public string EndDate { get; set; }

  [Name("COMMENT")]
  public string Comment { get; set; }

  [Name("DATA_SOURCE")]
  public string DataSource { get; set; }
}