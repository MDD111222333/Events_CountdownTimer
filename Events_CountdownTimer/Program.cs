using System;

namespace Events_CountdownTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            CountdownTimer timer = new CountdownTimer();

            int initialSeconds = 6;

            EventHandler<int> handler1 = (sender, seconds) =>
            {
                Console.WriteLine($"[Обработчик 1] Оставлось секунд: {seconds}");
            };

            EventHandler<int> handler2 = null!;
            handler2 = (sender, seconds) =>
            {
                Console.WriteLine($"[Обработчик 2] Оставлось секунд: {seconds}");
                if (seconds < initialSeconds / 2)
                {
                    Console.WriteLine("[Обработчик 2] Отписывается от события Tick.");
                    timer.Tick -= handler2;
                }
            };

            timer.Tick += handler1;
            timer.Tick += handler2;

            timer.Finished += (sender, e) =>
            {
                Console.WriteLine("[Finished] Отсчет завершен. Событие Finished сработало.");
            };

            Console.WriteLine($"--- Запуск автоматического теста с таймером на {initialSeconds} секунд ---");
            timer.Start(initialSeconds);

            Console.WriteLine();
            Console.WriteLine("=== Интерактивный режим таймера обратного отсчета ===");

            while (true)
            {
                try
                {
                    Console.Write("Введите количество секунд для таймера (или отрицательное для выхода): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        continue;
                    }

                    int userSeconds = int.Parse(input);
                    if (userSeconds < 0)
                    {
                        break;
                    }

                    CountdownTimer userTimer = new CountdownTimer();
                    userTimer.Tick += (s, sec) => Console.WriteLine($"Тик: осталось {sec} сек.");
                    userTimer.Finished += (s, e) => Console.WriteLine("Таймер завершил работу!");

                    userTimer.Start(userSeconds);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введено не целое число.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}