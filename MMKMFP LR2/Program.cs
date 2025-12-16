using System;

namespace ATMFiniteStateMachine
{
    // Состояния автомата
    public enum State
    {
        a1, // Неавторизовано
        a2, // Авторизация
        a3, // Меню
        a4, // Показать баланс
        a5  // Снять деньги
    }

    // Входные сигналы
    public enum Input
    {
        z1, // вставка карты
        z2, // успешный ввод
        z3, // неуспешный ввод
        z4, // узнать баланс
        z5, // снять деньги
        z6, // назад
        z7  // сумма
    }

    // Выходные сигналы
    public enum Output
    {
        w1, // ничего
        w2, // показ баланса
        w3, // возврат карты
        w4  // выдача денег
    }

    public class ATMMachine
    {
        private State _currentState;

        public ATMMachine()
        {
            _currentState = State.a1; // Начальное состояние - неавторизовано
        }

        /// <summary>
        /// Обработка входного сигнала и переход в новое состояние
        /// </summary>
        public Output ProcessInput(Input input)
        {
            Output output;

            // Обрабатываем значимые переходы через switch согласно обновленным таблицам
            switch (_currentState)
            {
                case State.a1 when input == Input.z1:
                    _currentState = State.a2;
                    output = Output.w1;
                    break;

                case State.a2 when input == Input.z2:
                    _currentState = State.a3;
                    output = Output.w1;
                    break;

                case State.a3:
                    switch (input)
                    {
                        case Input.z4: // узнать баланс
                            _currentState = State.a4;
                            output = Output.w2; // показ баланса при переходе a3->a4
                            break;
                        case Input.z5: // снять деньги
                            _currentState = State.a5;
                            output = Output.w1;
                            break;
                        case Input.z6: // назад
                            _currentState = State.a1;
                            output = Output.w3; // возврат карты
                            break;
                        default:
                            // Остальные входы не меняют состояние
                            output = Output.w1;
                            break;
                    }
                    break;

                case State.a4 when input == Input.z6: // назад
                    _currentState = State.a3;
                    output = Output.w1;
                    break;

                case State.a5:
                    switch (input)
                    {
                        case Input.z6: // назад
                            _currentState = State.a3;
                            output = Output.w1;
                            break;
                        case Input.z7: // сумма
                            _currentState = State.a3;
                            output = Output.w4; // выдача денег
                            break;
                        default:
                            // Остальные входы не меняют состояние
                            output = Output.w1;
                            break;
                    }
                    break;

                default:
                    // Все остальные комбинации не меняют состояние
                    output = Output.w1;
                    break;
            }

            return output;
        }

        /// <summary>
        /// Получить текущее состояние автомата
        /// </summary>
        public State CurrentState => _currentState;

        /// <summary>
        /// Получить описание состояния
        /// </summary>
        public string GetStateDescription()
        {
            return _currentState switch
            {
                State.a1 => "Неавторизовано",
                State.a2 => "Авторизация",
                State.a3 => "Меню",
                State.a4 => "Показать баланс",
                State.a5 => "Снять деньги",
                _ => "Неизвестное состояние"
            };
        }

        /// <summary>
        /// Получить описание входного сигнала
        /// </summary>
        public string GetInputDescription(Input input)
        {
            return input switch
            {
                Input.z1 => "вставка карты",
                Input.z2 => "успешный ввод",
                Input.z3 => "неуспешный ввод",
                Input.z4 => "узнать баланс",
                Input.z5 => "снять деньги",
                Input.z6 => "назад",
                Input.z7 => "сумма",
                _ => "неизвестный сигнал"
            };
        }

        /// <summary>
        /// Получить описание выходного действия
        /// </summary>
        public string GetOutputDescription(Output output)
        {
            return output switch
            {
                Output.w1 => "Ничего",
                Output.w2 => "Показать баланс",
                Output.w3 => "Возврат карты",
                Output.w4 => "Выдача денег",
                _ => "Неизвестное действие"
            };
        }

        /// <summary>
        /// Показать все доступные команды
        /// </summary>
        public void ShowAllCommands()
        {
            Console.WriteLine("\nВсе доступные команды:");
            Console.WriteLine("z1 - вставка карты");
            Console.WriteLine("z2 - успешный ввод");
            Console.WriteLine("z3 - неуспешный ввод");
            Console.WriteLine("z4 - узнать баланс");
            Console.WriteLine("z5 - снять деньги");
            Console.WriteLine("z6 - назад");
            Console.WriteLine("z7 - сумма");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var atm = new ATMMachine();

            Console.WriteLine("Модель банкомата на основе конечного автомата");
            Console.WriteLine("=============================================");
            Console.WriteLine("Введите команду (z1-z7)");

            while (true)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"\nТекущее состояние: {atm.GetStateDescription()}");
                atm.ShowAllCommands();

                Console.Write("\nВведите команду: ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                // Парсинг входной команды
                Input? parsedInput = ParseInput(input);

                if (!parsedInput.HasValue)
                {
                    Console.WriteLine("Ошибка: некорректная команда. Попробуйте снова.");
                    continue;
                }

                // Обработка входа
                Console.WriteLine($"Входной сигнал: {atm.GetInputDescription(parsedInput.Value)}");
                var output = atm.ProcessInput(parsedInput.Value);
                    
                Console.WriteLine($"Выходное действие: {atm.GetOutputDescription(output)}");
                Console.WriteLine($"Новое состояние: {atm.GetStateDescription()}");
            }
        }

        /// <summary>
        /// Преобразование строки в Input enum
        /// </summary>
        private static Input? ParseInput(string input)
        {
            return input switch
            {
                "1" => Input.z1,
                "2" => Input.z2,
                "3" => Input.z3,
                "4" => Input.z4,
                "5" => Input.z5,
                "6" => Input.z6,
                "7" => Input.z7,
                _ => null
            };
        }
    }
}