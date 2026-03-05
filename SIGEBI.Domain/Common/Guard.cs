namespace SIGEBI.Domain.Common
{
    public static class Guard
    {
        public static void NotNull<TValue>(TValue value, string fieldName)
        {
            if (value is null)
                throw new DomainException($"{fieldName} es requerido.");
        }

        public static void NotNullOrWhiteSpace(string? value, string fieldName, int? maxLen = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException($"{fieldName} es requerido.");

            if (maxLen.HasValue && value!.Length > maxLen.Value)
                throw new DomainException($"{fieldName} no puede exceder {maxLen.Value} caracteres.");
        }

        public static void GreaterThan(int value, int minExclusive, string fieldName)
        {
            if (value <= minExclusive)
                throw new DomainException($"{fieldName} debe ser mayor a {minExclusive}.");
        }

        public static void NotFutureDate(DateTime date, string fieldName)
        {
            if (date > DateTime.UtcNow)
                throw new DomainException($"{fieldName} no puede ser una fecha futura.");
        }

        public static void DateRange(DateTime start, DateTime end, string fieldName)
        {
            if (end <= start)
                throw new DomainException($"{fieldName}: la fecha de fin debe ser posterior a la de inicio.");
        }
    }
}
