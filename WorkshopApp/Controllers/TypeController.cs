using System.Collections.Generic;
using System;
using WorkshopApp.Models;
using System.Linq;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с типами заказа, CRUD `Типы заказа`
    /// </summary>
    public static class TypeController
    {
        /// <summary>
        /// Получает список типов заказа из базы данных
        /// </summary>
        /// <returns >Список типов заказа</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении типов заказа</exception>
        public static List<Models.Type> Index()
        {
            try
            {
                return Connection.db.Types.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт тип заказа
        /// </summary>
        /// <param name="data">Данные для создания типа заказа</param>
        /// <returns>Созданный тип заказа</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при создании типа заказа</exception>
        public static Models.Type Create(Dictionary<string, string> data)
        {
            try
            {
                Models.Type type = new Models.Type
                {
                    Name = data["Name"],
                };

                Connection.db.Types.Add(type);
                Connection.db.SaveChanges();

                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет тип заказа из базы данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор типа заказа, который нужно удалить</param>
        /// <returns>Возвращает `true`, если тип заказа был успешно удален; в противном случае возвращает `false`</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при удалении типа заказа</exception>
        public static bool Delete(int id)
        {
            try
            {
                Models.Type type = Connection.db.Types.FirstOrDefault(x => x.TypeID == id);

                if (type != null)
                {
                    Connection.db.Types.Remove(type);
                    Connection.db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
