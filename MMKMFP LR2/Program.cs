namespace MMKMFP_LR2
{
    // Состояния автомата
    public enum State
    {
        Неавторизованное = 1,
        Авторизация = 2,
        Меню = 3,
        ОтображениеБаланса = 4,
        СнятьДеньги = 5
    }

    // События (входные сигналы)
    public enum Event
    {
        ВводНомераКарты,
        УспешныйВводПароля,
        НеуспешныйВводПароля,
        УзнатьБаланс,
        СнятьДеньги,
        Выйти,
        ВМеню,
        Сумма
    }

    class Program
    {
        // Таблица переходов: (текущее состояние, событие) -> следующее состояние
        private static Dictionary<(State, Event), State> transitionTable = new Dictionary<(State, Event), State>
        {
            {(State.Неавторизованное, Event.ВводНомераКарты), State.Авторизация},
            {(State.Авторизация, Event.УспешныйВводПароля), State.Меню},
            {(State.Авторизация, Event.НеуспешныйВводПароля), State.Авторизация},
            {(State.Меню, Event.УзнатьБаланс), State.ОтображениеБаланса},
            {(State.Меню, Event.СнятьДеньги), State.СнятьДеньги},
            {(State.Меню, Event.Выйти), State.Неавторизованное},
            {(State.ОтображениеБаланса, Event.ВМеню), State.Меню},
            {(State.СнятьДеньги, Event.Сумма), State.ОтображениеБаланса},
            {(State.СнятьДеньги, Event.ВМеню), State.Меню}
        };

        // Текущее состояние
        private static State currentState = State.Неавторизованное;

        // Баланс для демонстрации
        private static decimal balance = 1000.0m;

        static void Main(string[] args)
        {
            Console.WriteLine("БАНКОМАТ - Конечный автомат");
            Console.WriteLine("===========================");

            while (true)
            {
                switch (currentState)
                {
                    case State.Неавторизованное:
                        HandleUnauthorizedState();
                        break;
                    case State.Авторизация:
                        HandleAuthorizationState();
                        break;
                    case State.Меню:
                        HandleMenuState();
                        break;
                    case State.ОтображениеБаланса:
                        HandleBalanceState();
                        break;
                    case State.СнятьДеньги:
                        HandleWithdrawState();
                        break;
                }
            }
        }

        static void HandleUnauthorizedState()
        {
            Console.WriteLine("\n--- НЕАВТОРИЗОВАННОЕ СОСТОЯНИЕ ---");
            Console.WriteLine("1. Вставить карту (ввод номера карты)");

            string input = Console.ReadLine();
            if (input == "1")
            {
                ExecuteTransition(Event.ВводНомераКарты, "Карта принята");
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }

        static void HandleAuthorizationState()
        {
            Console.WriteLine("\n--- АВТОРИЗАЦИЯ ---");
            Console.WriteLine("Введите пароль (1 - правильный, 2 - неправильный):");

            string input = Console.ReadLine();
            if (input == "1")
            {
                ExecuteTransition(Event.УспешныйВводПароля, "Авторизация успешна");
            }
            else if (input == "2")
            {
                ExecuteTransition(Event.НеуспешныйВводПароля, "Неверный пароль");
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }

        static void HandleMenuState()
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Узнать баланс");
            Console.WriteLine("2. Снять деньги");
            Console.WriteLine("3. Выйти");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ExecuteTransition(Event.УзнатьБаланс, "Переход к балансу");
                    break;
                case "2":
                    ExecuteTransition(Event.СнятьДеньги, "Переход к снятию денег");
                    break;
                case "3":
                    ExecuteTransition(Event.Выйти, "Карта возвращена");
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }

        static void HandleBalanceState()
        {
            Console.WriteLine("\n--- БАЛАНС ---");
            Console.WriteLine($"Ваш баланс: {balance} руб.");
            Console.WriteLine("1. Вернуться в меню");

            string input = Console.ReadLine();
            if (input == "1")
            {
                ExecuteTransition(Event.ВМеню, "Возврат в меню");
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }

        static void HandleWithdrawState()
        {
            Console.WriteLine("\n--- СНЯТИЕ ДЕНЕГ ---");
            Console.WriteLine("1. Ввести сумму для снятия");
            Console.WriteLine("2. Вернуться в меню");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Введите сумму:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0 && amount <= balance)
                {
                    balance -= amount;
                    ExecuteTransition(Event.Сумма, $"Выдано {amount} руб.");
                }
                else
                {
                    Console.WriteLine("Неверная сумма");
                }
            }
            else if (input == "2")
            {
                ExecuteTransition(Event.ВМеню, "Возврат в меню");
            }
            else
            {
                Console.WriteLine("Неверный ввод");
            }
        }

        static void ExecuteTransition(Event ev, string message)
        {
            Console.WriteLine($"> {message}");

            if (transitionTable.TryGetValue((currentState, ev), out State nextState))
            {
                currentState = nextState;
            }
            else
            {
                Console.WriteLine("Недопустимый переход!");
            }
        }
    }
}
