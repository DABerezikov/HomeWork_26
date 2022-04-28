using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_26
{
    internal abstract class Client
    {
        string Name { get => this.name; set => this.name = value; }

        uint ID { get => this.id; set => this.id = value; }

        Account? Deposit { get; set; }

        Account? NotDeposit { get; set; }

        private string name;
        private uint id;

    }
}
