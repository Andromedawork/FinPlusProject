namespace WebApplication1.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(int errorCode)
        {
            ErrorCode = errorCode;
            ErrorMessage = GetErrorMessageForCode(errorCode);
        }

        public int ErrorCode { get; }

        public string ErrorMessage { get; }

        private string GetErrorMessageForCode(int errorCode)
        {
            switch (errorCode)
            {
                case 404:
                    return "Страница не найдена";
                default:
                    return "Произошла ошибка";
            }
        }
    }
}
