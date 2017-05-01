using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indd.Contracts;

namespace Indd.Service.Commands
{
    class Notification
    {
        /// <summary>
        /// Current command
        /// </summary>
        Indd.Contracts.ICommand command;

        /// <summary>
        /// Notifies client
        /// </summary>
        /// <param name="command"></param>
        public Notification(ICommand command)
        {
            this.command = command;
        }




    }
}
