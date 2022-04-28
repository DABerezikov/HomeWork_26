using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26
{
    internal abstract class Account
    {
        public Account()
        {

        }
        public Account(uint ID, uint DepositRate, double Amount)
        {
            ClientID = ID;
            ClientDespositRate = DepositRate;
            this.amount = Amount;
            this.openDate = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// Используемые поля счета
        /// </summary>
        private double amount;
        private string openDate;

        /// <summary>
        /// Индивидуальный номер клиента
        /// </summary>
        public uint ClientID { get; set; }

        /// <summary>
        /// Процентная ставка по счету клиента
        /// </summary>
        public uint ClientDespositRate { get; set; }

        /// <summary>
        /// Сумма на счете
        /// </summary>
        public double Amount { get => this.amount; set => this.amount = value; }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public string OpenDate { get => this.openDate; set => this.openDate = value; }

        



    }

    internal class Account<T> : Account
        where T : Client
    {
        public Account(uint ClientID, uint ClientDepositRate, double Amount)
        {

        }
    }
}
