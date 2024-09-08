using Domain.Constants;
using Domain.Enums;

namespace Application.ServiceHelpers;

public static class ServiceHelper
{
    public static string GetAuxiliaryBody(DateTime dob, Sex sex)
    {
        int age = CalculateAge(dob);

        if (sex is Sex.Male)
        {
            if (age >= 0 && age <= 15)
                return AuxiliaryBodyName.Atfal;
            else if (age >= 16 && age <= 40)
                return AuxiliaryBodyName.Khuddam;
            else if (age > 40)
                return AuxiliaryBodyName.Ansarullah;
        }

        if (sex == Sex.Female)
        {
            if (age >= 0 && age <= 15)
                return AuxiliaryBodyName.Ansarullah;
            else if (age >= 16 && age <= 40)
                return AuxiliaryBodyName.Lajna;
        }

        return null;
    }
    public static int CalculateAge(DateTime dob)
    {
        var today = DateTime.Today;
        int age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }
}


