using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26
{
    internal abstract class Client
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Client()
        {
            this.name = default;
            this.ID = default;
            depositRate = default;
            this.deposit = null;
            this.notDeposit = null;
        }

        /// <summary>
        /// Конструктор для создания клиента
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        public Client(string Name)
        {
            this.name = Name;
            this.ID = GetID();
            depositRate = 3;
            this.deposit = null;
            this.notDeposit = null;

        }
        /// <summary>
        /// Конструктор для создания клиента и открытия счета
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="TapeAccount">Тип счета</param>
        public Client(string Name, string TapeAccount): this(Name)
        {
            OpenAccount(TapeAccount);
        }

        /// <summary>
        /// Конструктор для создания клиента, открытия и пополнения счета
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="TapeAccount">Тип счета</param>
        /// <param name="Amount">Сумма пополнения</param>
        public Client(string Name, string TapeAccount, double Amount): this(Name)
        {            
            OpenAccount(TapeAccount, Amount);
        }

        /// <summary>
        /// Используемые поля
        /// </summary>
        private string? name;
        private uint id;
        private static uint depositRate;
        private Account? deposit;
        private Account? notDeposit;

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get => this.name; set => this.name = value; }

        /// <summary>
        /// Уникальный номер клиента
        /// </summary>
        public uint ID { get => this.id; set => this.id = value; }

        /// <summary>
        /// Процентная ставка для клиента
        /// </summary>
        public uint DepositRate { get => depositRate; set => depositRate = value; }

        /// <summary>
        /// Депозитный счет клиента
        /// </summary>
        private Account? Deposit { get => this.deposit; set => this.deposit = value; }

        /// <summary>
        /// Недепозитный счет клиента
        /// </summary>
        private Account? NotDeposit { get => this.notDeposit; set => this.notDeposit = value; }

        public event Action<string> Action;

        
               
        /// <summary>
        /// Метод для открытия счета клиента и его пополнения
        /// </summary>
        /// <param name="TypeAccount">Тип счета</param>
        /// <param name="Ammount">Сумма пополнения</param>
        public void OpenAccount(string TypeAccount, double Amount = 0)
        {
            switch (TypeAccount)
            {
                case "Deposit":
                    Deposit = new Account(ID, DepositRate, Amount);
                    break;
                default:
                    NotDeposit = new Account(ID, DepositRate, Amount);
                    break;
            }
            Action?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {ID} открыт {TypeAccount} на сумму {Amount} руб.");
        }

        
        /// <summary>
        /// Метод для закрытия счета
        /// </summary>
        /// <param name="TypeAccount">Тип счета</param>
        /// <returns></returns>
        public string? CloseAccount (string TypeAccount)
        {

            string? result;
            switch (TypeAccount)
            {
                case "Deposit":
                    result = Deposit?.Close();
                    break;
                   
                default:
                    result =  NotDeposit?.Close();
                    break;
                  
            }
            Action?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {ID} закрыт {TypeAccount}, выплачено {result} руб.");
            return $"К выплате {result} руб.";
        }

        /// <summary>
        /// Метод для пополнения счета
        /// </summary>
        /// <param name="TypeAccount">Тип счета</param>
        /// <param name="SumRefill">Сумма пополнения</param>
        public void RefillAccount(string TypeAccount, double SumRefill)
        {
            switch (TypeAccount)
            {
                case "Deposit":
                    Deposit?.Refill(SumRefill);
                    break;

                default:
                    NotDeposit?.Refill(SumRefill);
                    break;

            }
            Action?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {ID} пополнен {TypeAccount} на сумму {SumRefill} руб.");
        }
        public virtual void Transfer(string TypeAccountSender, Account Recipient, double Amount)
        {
            switch (TypeAccountSender)
            {
                case "Deposit":
                    if (Deposit!=null)
                    { 
                        Account.TransferAccount(Deposit, Recipient, Amount);
                    }
                    else
                    {
                        break;
                    }
                   
                    break;

                default:
                    if (NotDeposit != null)
                    {
                        Account.TransferAccount(NotDeposit, Recipient, Amount);
                    }
                    else
                    {
                        break;
                    }
                    break;

            }
            Action?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиент {ID} перевел c {TypeAccountSender} на счет {Recipient}" +
                $" клиента {Recipient.ClientID} сумму {Amount} руб.");
           
        }

        /// <summary>
        /// Метод для генерации нового ID клиента
        /// </summary>
        /// <returns></returns>
        private uint GetID()
        {
            uint id = 202200;
            if (File.Exists("_id.txt"))
            {
                id = uint.Parse(File.ReadAllText("_id.txt"));
            }            
            id++;
            File.WriteAllText("_id.txt", id.ToString());
            Action?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} создан клиент {id}");
            return id;
        }

    }
}
