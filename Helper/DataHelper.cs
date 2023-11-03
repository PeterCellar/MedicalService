using MedicalService.Data.Models;

namespace MedicalService.Helper;

class DataHelper
{
    /// <summary>
    /// Fills gRPC VaccinesDataReply with values
    /// </summary>
    /// <param name="data">Data to fill a reply with</param>
    /// <returns>Filled gRPC TotalVaccinesReply</returns>
    public static VaccinesDataReply FillTotalVaccinesReply(VaccinesData data)
    {
        var reply = new VaccinesDataReply
        {
            Country = data.Country,
            Iso3 = data.Iso3,
            Region = data.Region,
            DataSource = data.DataSource,
            DateUpdated = data.DateUpdated,
            TotalVaccinations = data.TotalVaccinations.HasValue ? (double)data.TotalVaccinations : 0.0,
            OnePlusDose = data.OnePlusDose,
            TotalVaccinationsPerHundred = data.TotalVaccinationsPerHundred.HasValue ? (double)data.TotalVaccinationsPerHundred : 0.0,
            OnePlusDosePerHundred = data.OnePlusDosePerHundred.HasValue ? (double)data.OnePlusDosePerHundred : 0.0,
            PersonsLastDose = data.PersonsLastDose.HasValue ? (double)data.PersonsLastDose : 0.0,
            LastDosePerHundred = data.LastDosePerHundred.HasValue ? (double)data.LastDosePerHundred : 0.0,
            VaccinesUsed = data.VaccinesUsed,
            FirstVaccineDate = data.FirstVaccineDate,
            VaccinesTypesUsed = data.VaccinesTypesUsed.HasValue ? (double)data.VaccinesTypesUsed : 0.0,
            PersonsBoosterDose = data.PersonsBoosterDose.HasValue ? (double)data.PersonsBoosterDose : 0.0,
            PersonsBoosterDosePerHundred = data.PersonsBoosterDosePerHundred.HasValue ? (double)data.PersonsBoosterDosePerHundred : 0.0
        };

        return reply;
    }
}