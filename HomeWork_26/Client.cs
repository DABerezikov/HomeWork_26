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
            this.depositRate = default;
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
            this.depositRate = 3;
            this.deposit = null;
            this.notDeposit = null;

        }
        /// <summary>
        /// Конструктор для создания клиента и открытия счета
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="TapeAccount">Тип счета</param>
        public Client(string Name, string TapeAccount)
        {
            this.name = Name;
            this.ID = GetID();
            this.depositRate = 3;
            this.deposit = null;
            this.notDeposit = null;
            OpenAccount(TapeAccount);

        }

        /// <summary>
        /// Конструктор для создания клиента, открытия и пополнения счета
        /// </summary>
        /// <param name="Name">Имя клиента</param>
        /// <param name="TapeAccount">Тип счета</param>
        /// <param name="Amount">Сумма пополнения</param>
        public Client(string Name, string TapeAccount, double Amount)
        {
            this.name = Name;
            this.ID = GetID();
            this.depositRate = 3;
            this.deposit = null;
            this.notDeposit = null;
            OpenAccount(TapeAccount, Amount);

        }

        /// <summary>
        /// Используемые поля
        /// </summary>
        private string name;
        private uint id;
        private uint depositRate;
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
        public uint DepositRate { get => this.depositRate; set => this.depositRate = value; }

        /// <summary>
        /// Депозитный счет клиента
        /// </summary>
        private Account? Deposit { get => this.deposit; set => this.deposit = value; }

        /// <summary>
        /// Недепозитный счет клиента
        /// </summary>
        private Account? NotDeposit { get => this.notDeposit; set => this.notDeposit = value; }

        

        /// <summary>
        /// Метод открытия счета клиента
        /// </summary>
        /// <param name="TypeAccount">Тип счета</param>
        public virtual void OpenAccount(string TypeAccount)
        {
            switch (TypeAccount)
            {
                case "Deposit":
                    Deposit = new Account<Client>(ID, DepositRate, 0);
                    break;
                default:
                    NotDeposit = new Account<Client>(ID, DepositRate, 0);
                    break;
            }
        }

        
        /// <summary>
        /// Метод для открытия счета клиента и его пополнения
        /// </summary>
        /// <param name="TypeAccount">Тип счета</param>
        /// <param name="Ammount">Сумма пополнения</param>
        public virtual void OpenAccount(string TypeAccount, double Amount)
        {
            switch (TypeAccount)
            {
                case "Deposit":
                    Deposit = new Account<Client>(ID, DepositRate, Amount);
                    break;
                default:
                    NotDeposit = new Account<Client>(ID, DepositRate, Amount);
                    break;
            }
        }

        private uint GetID()
        {
            uint id = 202200;
            if (File.Exists("_id.txt"))
            {
                id = uint.Parse(File.ReadAllText("_id.txt"));
            }            
            id++;
            File.WriteAllText("_id.txt", id.ToString());
            return id;
        }

    }
}
