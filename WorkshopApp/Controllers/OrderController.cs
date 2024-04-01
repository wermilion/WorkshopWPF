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
        public static Order Create(Dictionary<string, dynamic> data)
        {
            try
            {
                Order order = new Order
                {
                    UserID = data["UserID"],
                    TypeID = int.Parse(data["TypeID"]),
                    StatusID = int.Parse(data["StatusID"]),
                    Date = data["Date"],
                    CountServices = data["CountServices"],
                    TotalPrice = (decimal)data["TotalPrice"]
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
        /// Обновляет заказ
        /// </summary>
        /// <param name="data">Данные для обновления заказа</param>
        /// <returns>Созданного пользователя</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при создании заказа</exception>
        public static Order Update(int orderId, Dictionary<string, dynamic> data)
        {
            try
            {
                Order order = Connection.db.Orders.FirstOrDefault(x => x.OrderID == orderId);

                if (order == null)
                {
                    throw new Exception($"Заказ с ID = {orderId} не был найден");
                }

                order.TypeID = int.Parse(data["TypeID"]);
                order.StatusID = int.Parse(data["StatusID"]);
                order.Date = data["Date"];

                Connection.db.SaveChanges();

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Поиск заказа по `id`
        /// </summary>
        /// <param name="orderId">Идентификатор заказа</param>
        /// <returns>Найденный заказа</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при поиске заказа</exception>
        public static Order Find(int orderId)
        {
            try
            {
                return Connection.db.Orders.Find(orderId) as Order;
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
                Order order = Connection.db.Orders.FirstOrDefault(x => x.OrderID == id);

                if (order != null)
                {
                    Connection.db.Orders.Remove(order);
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
