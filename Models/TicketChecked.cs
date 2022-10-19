namespace Study.Parking.Models
{
    public class TicketChecked : Ticket
    {
        public TicketChecked(DateTime TimeIn, List<PriceTable> Prices) : base(TimeIn, Prices)
        {
            this.TimeOut = this.TimeIn.AddMinutes(this.Permanence);
        }
    }
}