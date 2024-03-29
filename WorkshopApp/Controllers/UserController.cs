using System;
using System.Collections.Generic;
using System.Linq;
using WorkshopApp.Models;

namespace WorkshopApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователем, CRUD `Пользователи`
    /// </summary>
    public static class UserController
    {
        /// <summary>
        /// Получает список пользователей из базы данных
        /// </summary>
        /// <returns >Список пользователей</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при получении пользователей</exception>
        public static List<User> Index()
        {
            try
            {
                return Connection.db.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт пользователя
        /// </summary>
        /// <param name="data">Данные для создания пользователя</param>
        /// <returns>Созданного пользователя</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при создании пользователя</exception>
        public static User Create(Dictionary<string, string> data)
        {
            try
            {
                User user = new User
                {
                    Name = data["Name"],
                    Surname = data["Surname"],
                    Patronimyc = !string.IsNullOrEmpty(data["Patronymic"]) ? data["Patronymic"] : null,
                    Phone = data["Phone"],
                    Login = data["Login"],
                    Password = data["Password"],
                    RoleID = int.Parse(data["RoleID"]),
                };

                Connection.db.Users.Add(user);
                Connection.db.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет пользователя из базы данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя, которого нужно удалить</param>
        /// <returns>Возвращает `true`, если пользователь был успешно удален; в противном случае возвращает `false`</returns>
        /// <exception cref="Exception">Возникает, если возникла ошибка при удалении пользователя</exception>
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

        /// <summary>
        /// Выполняет попытку аутентификации пользователя с указанными учетными данными
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Аутентифицированный пользователь, если учетные данные корректны</returns>
        /// <exception cref="Exception">Возникает, если учетные данные некорректны</exception>
        public static User SignIn(string login, string password)
        {
            User user = Connection.db.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user != null)
            {
                App.authUser = user;
                return user;
            }

            throw new Exception("Некорректные данные");
        }
    }
}
