namespace FinPlus.Presentation
{
    using System.ComponentModel.DataAnnotations;

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                            .FirstOrDefault() as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();
        }
    }
}
