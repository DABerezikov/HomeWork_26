using HomeWork_26.Services;
using System;
using System.IO;

namespace HomeWork_26.Models
{
    internal class Client
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Client()
        {
            name = default;
            Id = default;
            depositRate = default;
            deposit = null;
            notDeposit = null;
        }

        /// <summary>
        /// Конструктор для создания клиента
        /// </summary>
        /// <param name="name">Имя клиента</param>
        public Client(string name)
        {
            this.LogAction += LoadSave<Client>.Log;
            this.name = name;
            Id = GetId();
            depositRate = 3;
            deposit = null;
            notDeposit = null;

        }
        /// <summary>
        /// Конструктор для создания клиента и открытия счета
        /// </summary>
        /// <param name="name">Имя клиента</param>
        /// <param name="tapeAccount">Тип счета</param>
        public Client(string name, string tapeAccount) : this(name)
        {
            OpenAccount(tapeAccount);
        }

        /// <summary>
        /// Конструктор для создания клиента, открытия и пополнения счета
        /// </summary>
        /// <param name="name">Имя клиента</param>
        /// <param name="tapeAccount">Тип счета</param>
        /// <param name="amount">Сумма пополнения</param>
        public Client(string name, string tapeAccount, double amount) : this(name)
        {
            OpenAccount(tapeAccount, amount);
        }

        /// <summary>
        /// Используемые поля
        /// </summary>
        private string? name;
        private uint id;
        private uint depositRate;
        private Account? deposit;
        private Account? notDeposit;

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string? Name { get => name; set => name = value; }

        /// <summary>
        /// Уникальный номер клиента
        /// </summary>
        public uint Id { get => id; set => id = value; }

        /// <summary>
        /// Процентная ставка для клиента
        /// </summary>
        public uint DepositRate { get => depositRate; set => depositRate = value; }

        /// <summary>
        /// Депозитный счет клиента
        /// </summary>
        public Account? Deposit { get => deposit; set => deposit = value; }

        /// <summary>
        /// Недепозитный счет клиента
        /// </summary>
        public Account? NotDeposit { get => notDeposit; set => notDeposit = value; }

        public event Action<string>? LogAction;



        /// <summary>
        /// Метод для открытия счета клиента и его пополнения
        /// </summary>
        /// <param name="typeAccount">Тип счета</param>
        /// <param name="amount">Сумма пополнения</param>
        public void OpenAccount(string typeAccount, double amount = 0)
        {
            switch (typeAccount)
            {
                case "Депозитный счет":
                    Deposit = new Account(Id, DepositRate, amount);
                    break;
                default:
                    NotDeposit = new Account(Id, DepositRate, amount);
                    break;
            }
            LogAction?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {Id} открыт {typeAccount} на сумму {amount} руб.");
        }


        /// <summary>
        /// Метод для закрытия счета
        /// </summary>
        /// <param name="typeAccount">Тип счета</param>
        /// <returns></returns>
        public string CloseAccount(string typeAccount)
        {

            string? result;
            switch (typeAccount)
            {
                case "Депозитный счет":
                    result = Deposit?.Close();
                    Deposit = null;
                    break;

                default:
                    result = NotDeposit?.Close();
                    NotDeposit = null;
                    break;

            }
            LogAction?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {Id} закрыт {typeAccount}, выплачено {result} руб.");
            return $"К выплате {result} руб.";
        }

        /// <summary>
        /// Метод для пополнения счета
        /// </summary>
        /// <param name="typeAccount">Тип счета</param>
        /// <param name="sumRefill">Сумма пополнения</param>
        public void RefillAccount(string typeAccount, double sumRefill)
        {
            switch (typeAccount)
            {
                case "Депозитный счет":
                    Deposit?.Refill(sumRefill);
                    break;

                default:
                    NotDeposit?.Refill(sumRefill);
                    break;

            }
            LogAction?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиенту {Id} пополнен {typeAccount} на сумму {sumRefill} руб.");
        }
        public void Transfer(string typeAccountSender, Account recipient, double amount)
        {
            switch (typeAccountSender)
            {
                case "Депозитный счет":
                    if (Deposit != null)
                    {
                        Account.TransferAccount(Deposit, recipient, amount);
                    }
                    break;

                default:
                    if (NotDeposit != null)
                    {
                        Account.TransferAccount(NotDeposit, recipient, amount);
                    }
                    break;

            }
            LogAction?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} клиент {Id} перевел c {typeAccountSender} на счет {recipient}" +
                $" клиента {recipient.ClientId} сумму {amount} руб.");

        }

        /// <summary>
        /// Метод для генерации нового ID клиента
        /// </summary>
        /// <returns></returns>
        private uint GetId()
        {
            uint getId = 202200;
            if (File.Exists("_id.txt"))
            {
                getId = uint.Parse(File.ReadAllText("_id.txt"));
            }
            getId++;
            File.WriteAllText("_id.txt", getId.ToString());
            LogAction?.Invoke($"{DateTime.Now.ToShortDateString()} в {DateTime.Now.ToShortTimeString()} создан клиент {getId}");
            return getId;
        }

    }
    internal class EntityClient : Client
    {
        public EntityClient() : base()
        {

        }
        public EntityClient(string name) : base(name)
        {
            DepositRate = 6;
        }

        public EntityClient(string name, string tapeAccount) : base(name, tapeAccount) { DepositRate = 6; }

        public EntityClient(string name, string tapeAccount, double amount) : base(name, tapeAccount, amount) { DepositRate = 6; }

    }

    internal class VipClient : Client
    {
        public VipClient() : base()
        {

        }
        public VipClient(string name) : base(name)
        {
            DepositRate = 12;

        }

        public VipClient(string name, string tapeAccount) : base(name, tapeAccount) { DepositRate = 12; }

        public VipClient(string name, string tapeAccount, double amount) : base(name, tapeAccount, amount) { DepositRate = 12; }
    }
}
