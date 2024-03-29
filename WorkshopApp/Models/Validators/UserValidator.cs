﻿using System.Collections.Generic;

namespace WorkshopApp.Models.Validators
{
    public static class UserValidator
    {
        /// <summary>
        /// Валидирует словарь данных
        /// </summary>
        /// <param name="data">Словарь данных для валидации</param>
        /// <returns>Кортеж, где первый элемент - флаг валидности, второй - список ошибок</returns>
        public static (bool IsValid, List<string> Errors) Validate(Dictionary<string, string> data)
        {
            List<string> errors = new List<string>();

            foreach (var item in data)
            {
                if (item.Key == "Patronymic") continue;

                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    errors.Add($"Поле {item.Key} пустое");
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
