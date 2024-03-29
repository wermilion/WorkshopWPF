using System.Collections.Generic;
using System;
using WorkshopApp.Models;
using System.Linq;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы со статусами заказа, CRUD `Статусы заказа`
    /// </summary>
    public static class StatusController
    {
        /// <summary>
        /// Получает список статусов из базы данных
        /// </summary>
        /// <returns >Список статусов</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении статусов</exception>
        public static List<Status> Index()
        {
            try
            {
                return Connection.db.Statuses.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
