using Study.Parking.Models;

namespace Study.Parking.DataAccess
{
    public interface ITicketDataMongo : IDataAccessMongo<Ticket>
    {
        Task<Ticket> Patch(string Id, Ticket Model);
    }
}