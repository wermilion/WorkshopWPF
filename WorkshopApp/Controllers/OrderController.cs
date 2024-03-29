using System;
using System.Collections.Generic;
using System.Linq;
using WorkshopApp.Models;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с заказами, CRUD `Заказы`
    /// </summary>
    public static class OrderController
    {
        /// <summary>
        /// Получает список заказов из базы данных
        /// </summary>
        /// <returns >Список заказов</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении заказов</exception>
        public static List<Order> Index()
        {
            try
            {
                return Connection.db.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт заказ
        /// </summary>
        /// <param name="data">Данные для создания заказа</param>
        /// <returns>Созданного пользователя</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при создании заказа</exception>
        public static Order Create(Dictionary<string, string> data)
        {
            try
            {
                Order order = new Order
                {
                    UserID = int.Parse(data["UserID"]),
                    TypeID = int.Parse(data["TypeID"]),
                    StatusID = int.Parse(data["StatusID"]),
                    Date = DateTime.Parse(data["Date"]),
                    CountServices = int.Parse(data["CountServices"]),
                    TotalPrice = decimal.Parse(data["TotalPrice"]),
                };

                Connection.db.Orders.Add(order);
                Connection.db.SaveChanges();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет заказ из базы данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа, который нужно удалить</param>
        /// <returns>Возвращает `true`, если заказ был успешно удален; в противном случае возвращает `false`</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при удалении заказа</exception>
        public static bool Delete(int id)
        {
            try
            {
                User user = Connection.db.Users.FirstOrDefault(x => x.UserID == id);

                if (user != null)
                {
                    Connection.db.Users.Remove(user);
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
