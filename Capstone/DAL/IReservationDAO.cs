using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        IList<Reservations> GetReservationBySites(int siteId);

        int AddReservation(Reservations newReservations);

        IList<Reservations> GetReservationByCampground(int campgroundID);

        int DifferenceInDates();
    }
}
