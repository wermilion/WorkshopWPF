using System.Collections.Generic;

namespace WorkshopApp.Models.Validators
{
    public static class OrderValidator
    {
        /// <summary>
        /// Валидирует словарь данных
        /// </summary>
        /// <param name="data">Словарь данных для валидации</param>
        /// <returns>Кортеж, где первый элемент - флаг валидности, второй - список ошибок</returns>
        public static (bool IsValid, List<string> Errors) Validate(Dictionary<string, dynamic> data)
        {
            List<string> errors = new List<string>();

            foreach (var item in data)
            {
                if (string.IsNullOrWhiteSpace(item.Value.ToString()))
                {
                    errors.Add($"Поле {item.Key} пустое");
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
