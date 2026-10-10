using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PR2_JSON
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Показать задачи пользователя");
                Console.WriteLine("2. Найти задачу по ID");
                Console.WriteLine("3. Создать задачу");
                Console.WriteLine("4. Изменить статус задачи");
                Console.WriteLine("5. Удалить задачу");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string? choice = Console.ReadLine();

                if (choice == "0")
                    break;

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ShowTodos();
                            break;

                        case "2":
                            await FindTodo();
                            break;

                        case "3":
                            await CreateTodo();
                            break;

                        case "4":
                            await UpdateTodo();
                            break;

                        case "5":
                            await DeleteTodo();
                            break;

                        default:
                            Console.WriteLine("Неверный пункт меню!");
                            break;
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Ошибка сети: " + ex.Message);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Время ожидания истекло.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }

            client.Dispose();
        }
        private static async Task ShowTodos()
        {
            Console.Write("Введите ID пользователя: ");
            int userId = int.Parse(Console.ReadLine()!);

            HttpResponseMessage response =
                await client.GetAsync(
                    $"https://jsonplaceholder.typicode.com/todos?userId={userId}"
                );

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка получения задач!");
                return;
            }

            List<Todo>? todos =
                await response.Content.ReadFromJsonAsync<List<Todo>>();

            if (todos == null)
            {
                Console.WriteLine("Нет данных");
                return;
            }

            foreach (Todo todo in todos)
            {
                Console.WriteLine(
                    $"ID: {todo.Id}, Название: {todo.Title}, Выполнена: {todo.Completed}"
                );
            }
        }
        private static async Task FindTodo()
        {

        }
        private static async Task CreateTodo()
        {

        }
        private static async Task UpdateTodo()
        {

        }
        private static async Task DeleteTodo()
        {

        }
    }
}