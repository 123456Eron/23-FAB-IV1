using System;
using System.Collections.Generic;

namespace MMKMFP_LR2
{
    // Состояния автомата (на русском)
    enum State
    {
        Начало, // Start
        КартаВставлена, // CardInserted
        ПИНВведён, // PinEntered
        Авторизован, // Authenticated
        СуммаВведена, // AmountEntered
        ДеньгиВыданы // Dispensed
    }

    // Цифровое соответствие действиям (1–6)
    enum Event
    {
        ВставитьКарту = 1,
        ВвестиПИН,
        Авторизоваться,
        ВвестиСумму,
        ВыдатьДеньги,
        Сброс
    }

    class ATMStateMachine
    {
        private static readonly Dictionary<(State, Event), State> transitions = new()
    {
        { (State.Начало, Event.ВставитьКарту), State.КартаВставлена },
        { (State.КартаВставлена, Event.ВвестиПИН), State.ПИНВведён },
        { (State.ПИНВведён, Event.Авторизоваться), State.Авторизован },
        { (State.Авторизован, Event.ВвестиСумму), State.СуммаВведена },
        { (State.СуммаВведена, Event.ВыдатьДеньги), State.ДеньгиВыданы },
        { (State.ДеньгиВыданы, Event.Сброс), State.Начало }
    };

        public State CurrentState { get; private set; } = State.Начало;

        public void HandleEvent(Event evt)
        {
            if (transitions.TryGetValue((CurrentState, evt), out var next))
            {
                CurrentState = next;
                Console.WriteLine($"Переход: {CurrentState}\n");
            }
            else
            {
                Console.WriteLine("Недопустимое действие.\n");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var atm = new ATMStateMachine();
            Console.WriteLine("Модель банкомата (конечный автомат)");
            while (true)
            {
                Console.WriteLine($"Текущее состояние: {atm.CurrentState}");
                Console.WriteLine("Действия (введите номер):");
                Console.WriteLine("1 — Вставить карту");
                Console.WriteLine("2 — Ввести ПИН");
                Console.WriteLine("3 — Авторизоваться");
                Console.WriteLine("4 — Ввести сумму");
                Console.WriteLine("5 — Выдать деньги");
                Console.WriteLine("6 — Сброс");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var code) && code >= 1 && code <= 6)
                {
                    atm.HandleEvent((Event)code);
                }
                else
                {
                    Console.WriteLine("Неверный ввод.\n");
                }
            }
        }
    }
}
