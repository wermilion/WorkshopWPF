﻿using System.Collections.Generic;
using System;
using WorkshopApp.Models;
using System.Linq;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с доп. услугами, CRUD `Статусы заказа`
    /// </summary>
    public static class ServiceController
    {
        /// <summary>
        /// Получает список доп. услуг из базы данных
        /// </summary>
        /// <returns >Список доп. услуг</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении доп. услуг</exception>
        public static List<Service> Index()
        {
            try
            {
                return Connection.db.Services.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт доп. услугу
        /// </summary>
        /// <param name="data">Данные для создания доп. услуги</param>
        /// <returns>Созданную доп. услугу</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при создании доп. услуги</exception>
        public static Service Create(Dictionary<string, string> data)
        {
            try
            {
                Service service = new Service
                {
                    Name = data["Name"],
                    Price = decimal.Parse(data["Price"]),
                };

                Connection.db.Services.Add(service);
                Connection.db.SaveChanges();

                return service;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет доп. услугу из базы данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доп. услуги, которую нужно удалить</param>
        /// <returns>Возвращает `true`, если доп. услуга была успешно удалена; в противном случае возвращает `false`</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при удалении доп. услуги</exception>
        public static bool Delete(int id)
        {
            try
            {
                Service service = Connection.db.Services.FirstOrDefault(x => x.ServiceID == id);

                if (service != null)
                {
                    Connection.db.Services.Remove(service);
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
