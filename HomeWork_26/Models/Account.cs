using System;

namespace HomeWork_26.Models
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
            amount = Amount;
            openDate = DateTime.Now.ToShortDateString();
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
        public double Amount { get => amount; set => amount = value; }

        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public string OpenDate { get => openDate; set => openDate = value; }

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
                    year = (DateTime.Now - DateTime.Parse(openDate)).Days / 365.25;

                }
                else
                {
                    year = (DateTime.Now - DateTime.Parse(refillDate)).Days / 365.25;

                }
                Interest = Amount * year * ClientDepositRate / 100;

                return Math.Round(interest, 2);

            }
            set { interest = value; }

        }

        /// <summary>
        /// Дата пополнения счета
        /// </summary>
        public string RefillDate { get => refillDate; set => refillDate = value; }

        /// <summary>
        /// Метод для закрытия счета
        /// </summary>
        /// <returns></returns>
        public string Close()
        {
            var r = Amount += Interest + TempInterest;
            Amount = 0;
            return r.ToString();
        }

        /// <summary>
        /// Метод пополнения счета
        /// </summary>
        /// <param name="SumRefill">Сумма пополнения</param>
        public void Refill(double SumRefill)
        {
            TempInterest += Interest;                          // Сохранение текущих процентов
            Amount += SumRefill;                               // Увеличение суммы на счете на величину пополнения и количества процентов на данный момент            
            RefillDate = DateTime.Now.ToShortDateString();     // Установление даты пополнения счета для дальнейшего расчета процентов 
        }

        /// <summary>
        /// Метод для перевода со счета
        /// </summary>
        /// <param name="Transfer">Сумма перевода</param>
        private void Transfer(double Transfer)
        {
            TempInterest += Interest;                          // Сохранение текущих процентов
            Amount -= Transfer;                                // Уменьшение суммы на счете на величину перевода            
            RefillDate = DateTime.Now.ToShortDateString();     // Установление даты пополнения счета для дальнейшего расчета процентов             
        }

        /// <summary>
        /// Метод для перевода между счетами
        /// </summary>
        /// <param name="Sender">Счет отправитель</param>
        /// <param name="Recipient">Счет получатель</param>
        /// <param name="Amount">Сумма перевода</param>
        public static void TransferAccount(Account Sender, Account Recipient, double Amount)
        {
            Sender.Transfer(Amount);
            Recipient.Refill(Amount);

        }
    }

}
