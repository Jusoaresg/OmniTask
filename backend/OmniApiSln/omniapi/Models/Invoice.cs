public class Invoice
{
    public required Guid Id { get; set; }
    public required User User { get; set; }
    public required float Value { get; set; }
    public required DateTime DueDate { get; set; }
    public required string Status { get; set; }
    public required int DaysBefore { get; set; }
    public required int EmailTemplateId = 1;
}

