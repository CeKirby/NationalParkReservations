using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        IList<Reservations> GetReservationBySites(int siteId);

        void AddReservation(Reservations newReservations);

        IList<Reservations> GetReservationByCampground(int campgroundID);
    }
}
