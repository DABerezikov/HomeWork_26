using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26
{
    internal class Account
    {
        public Account()
        {

        }
        public Account(uint ID, uint DepositRate, double Amount)
        {
            ClientID = ID;
            ClientDepositRate = DepositRate;
            this.amount = Amount;
            this.openDate = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// Используемые поля счета
        /// </summary>
        private double amount;
        private string openDate;
        private double interest;
        private string refillDate;

        /// <summary>
        /// Индивидуальный номер клиента
        /// </summary>
        public uint ClientID { get; set; }

        /// <summary>
        /// Процентная ставка по счету клиента
        /// </summary>
        public uint ClientDepositRate { get; set; }

        /// <summary>
        /// Хранение накопленных процентов
        /// </summary>
        public double TempInterest { get; set; }

        /// <summary>
        /// Сумма на счете
        /// </summary>
        public double Amount { get => this.amount; set => this.amount = value; }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public string OpenDate { get => this.openDate; set => this.openDate = value; }

        /// <summary>
        /// Процент по счету
        /// </summary>
        public double Interest
        {
            get
            {
                double year;
                if (RefillDate == null)
                {
                    year = (DateTime.Now - DateTime.Parse(this.openDate)).Days / 365.25;

                }
                else
                {
                    year = (DateTime.Now - DateTime.Parse(this.refillDate)).Days / 365.25;

                }
                Interest = Amount * year * ClientDepositRate / 100;

                return Math.Round(interest, 2);

            }
            set { interest = value; }

        }

        /// <summary>
        /// Дата пополнения счета
        /// </summary>
        public string RefillDate { get => this.refillDate; set => this.refillDate = value; }

        /// <summary>
        /// Метод для закрытия счета
        /// </summary>
        /// <returns></returns>
        public string Close()
        {
            return (Amount += Interest + TempInterest).ToString();
            
            
        }




    }

}
