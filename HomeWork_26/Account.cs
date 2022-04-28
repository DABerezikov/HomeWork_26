using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26
{
    internal abstract class Account
    {
        /// <summary>
        /// Используемые поля счета
        /// </summary>
        private double amount;

        /// <summary>
        /// Индивидуальный номер клиента
        /// </summary>
        public uint ClientID { get; set; }
        /// <summary>
        /// Процентная ставка по счету клиента
        /// </summary>
        public uint ClientDespositRate { get; set; }

        public double Ammount { get => this.amount; set => this.amount = value; }

    }

    internal class Account<T> : Account
        where T : Client
    {
        public Account()
        {

        }
    }
}
