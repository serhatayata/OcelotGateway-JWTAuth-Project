using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (result.IsValid) return;

            //var errorArray = result.Errors.Select(s => s.ErrorMessage).ToArray();
            //var errorMessages = string.Join(",", errorArray);

            //var jsonString = JsonConvert.SerializeObject(result.Errors.Select(s => s.ErrorMessage));

            //throw new BadHttpRequestException(errorMessages);
            //throw new BadHttpRequestException(jsonString);
            throw new ValidationException(result.Errors);
        }
    }
}