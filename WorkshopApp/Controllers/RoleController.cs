using System;
using System.Collections.Generic;
using System.Linq;
using WorkshopApp.Models;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролями, CRUD `Роли`
    /// </summary>
    public static class RoleController
    {
        /// <summary>
        /// Получает список ролей из базы данных
        /// </summary>
        /// <returns >Список ролей</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении ролей</exception>
        public static List<Role> Index()
        {
            try
            {
                return Connection.db.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
