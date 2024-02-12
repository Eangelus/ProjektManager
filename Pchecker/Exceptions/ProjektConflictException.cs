using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Exceptions
{
    internal class ProjektConflictException : BaseException
    {

        public Projekt ExistingProjekt { get;  }
        public Projekt IncomingProjekt { get; }

        public ProjektConflictException(Projekt existingProjekt, Projekt incomingProjekt)
        {
            ExistingProjekt = existingProjekt;
            IncomingProjekt = incomingProjekt;
        }

        public ProjektConflictException(string? message, Projekt existingProjekt, Projekt incomingProjekt) : base(message)
        {
            ExistingProjekt = existingProjekt;
            IncomingProjekt = incomingProjekt;
        }

        public ProjektConflictException(string? message, Exception? innerException, Projekt existingProjekt, Projekt incomingProjekt) : base(message, innerException)
        {
            ExistingProjekt = existingProjekt;
            IncomingProjekt = incomingProjekt;
        }


    }
}
