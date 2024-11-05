using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace ConsoleApp4
    {
        delegate void MyDelegate3(string message);

        class CreditCard
        {
            public event MyDelegate3 EventANM;

            public string Number { get; private set; }
            public string FIO { get; private set; }
            public string PIN { get; private set; }
            public int CreditLimit { get; private set; }
            public double Sum { get; private set; }

            public CreditCard(string number, string fio, string pin, int creditLimit, double sum)
            {
                Number = number;
                FIO = fio;
                PIN = pin;
                CreditLimit = creditLimit;
                Sum = sum;
            }

            public void Expense()
            {
                Console.WriteLine("Какую сумму снять со счета? : ");
                double amount = double.Parse(Console.ReadLine());
                if (amount > 0 && amount <= Sum)
                {
                    Sum -= amount;
                    Console.WriteLine($"Сумма снята! Остаток на счету: {Sum}");
                    EventANM?.Invoke($"Снятие {amount:C} с основного счета. Текущий баланс: {Sum:C}");
                }
                else
                {
                    Console.WriteLine("Недостаточно средств или некорректная сумма.");
                }
            }

            public void ExpenseFromLimit()
            {
                Console.WriteLine("Напишите сумму, которую снять с лимитного счета: ");
                double amount = double.Parse(Console.ReadLine());
                if (amount > 0 && amount <= CreditLimit)
                {
                    CreditLimit -= (int)amount;
                    Sum += amount;
                    Console.WriteLine($"Операция успешна! Остаток на лимитном счету: {CreditLimit}");
                    EventANM?.Invoke($"Снятие {amount:C} с лимитного счета. Остаток лимита: {CreditLimit:C}");
                }
                else
                {
                    Console.WriteLine("Лимитный счет исчерпан или некорректная сумма!");
                }
            }


            public void TopUpAccount()
            {
                Console.WriteLine("На какую сумму пополнить счет? : ");
                double amount = double.Parse(Console.ReadLine());
                if (amount > 0)
                {
                    Sum += amount;
                    Console.WriteLine($"Счет пополнен! Остаток на счету: {Sum}");
                    EventANM?.Invoke($"Пополнение на {amount:C}. Текущий баланс: {Sum:C}");
                }
                else
                {
                    Console.WriteLine("Ошибка");
                }
            }

            public void ChangePIN()
            {
                Console.WriteLine("Введите новый PIN-код: ");
                string newPin = Console.ReadLine();
                if (PIN == newPin)
                {
                    Console.WriteLine("PIN должен отличаться от текущего!");
                }
                else
                {
                    PIN = newPin;
                    Console.WriteLine("PIN изменён!");
                    EventANM?.Invoke("PIN-код успешно изменён.");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                CreditCard creditCard = new CreditCard("123356154", "Maria Shevchenko", "53623526", 500, 1000);

                creditCard.EventANM += message => Console.WriteLine($"[Уведомление]: {message}");

                creditCard.TopUpAccount();       
                creditCard.Expense();            
                creditCard.ExpenseFromLimit();  
                creditCard.ChangePIN();     
            }
        }
    }
