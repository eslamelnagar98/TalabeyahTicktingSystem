namespace Infrastructure.Data.EFConfigurations;
public class TicketsConfigurations : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> ticketBuilder)
    {
        ticketBuilder.ToTable("Tickets","dbo");
        ticketBuilder.Ignore(ticket => ticket.IsNullTicket);
        ticketBuilder.Property(ticket => ticket.District)
                     .IsRequired()
                     .HasMaxLength(30);
        ticketBuilder.Property(ticket => ticket.City)
                     .IsRequired()
                     .HasMaxLength(30);
        ticketBuilder.Property(ticket => ticket.Governorate)
                    .IsRequired()
                    .HasMaxLength(30);
        ticketBuilder.Property(ticket => ticket.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(30);
        ticketBuilder.Property(ticket => ticket.CreationDate)
                    .IsRequired();
    }
}
