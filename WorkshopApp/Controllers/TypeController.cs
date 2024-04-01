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
        /// Обновляет тип заказа
        /// </summary>
        /// <param name="typeID">Идентификатор типа заказа, который нужно обновить</param>
        /// <param name="data">Данные для обновления типа заказа</param>
        /// <returns>Созданного пользователя</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при обновлении типа заказа</exception>
        public static Models.Type Update(int typeID, Dictionary<string, string> data)
        {
            try
            {
                Models.Type type = Connection.db.Types.FirstOrDefault(x => x.TypeID == typeID);

                if (type == null)
                {
                    throw new Exception($"Тип заказа с ID = {typeID} не был найден");
                }

                type.Name = data["Name"];

                Connection.db.SaveChanges();

                return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Поиск типа заказа по `id`
        /// </summary>
        /// <param name="typeId">Идентификатор типа заказа</param>
        /// <returns>Найденный тип заказа</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при поиске типа заказа</exception>
        public static Models.Type Find(int typeId)
        {
            try
            {
                return Connection.db.Types.Find(typeId) as Models.Type;
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
