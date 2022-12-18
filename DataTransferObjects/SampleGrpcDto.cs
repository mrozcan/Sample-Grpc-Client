namespace SampleGrpcClient.DataTransferObjects;

public class SampleGrpcDto
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? SampleStringData { get; set; }
    public int SampleIntData { get; set; }

    public SampleGrpcDto(string id, string createDate, string updateDate, string? sampleStringDate, int? sampleIntDate)
    {
        Id = Guid.Parse(id);
        CreateDate = DateTime.Parse(createDate);
        SampleStringData = sampleStringDate;
        SampleIntData = sampleIntDate ?? 0;

        if (updateDate != null)
            UpdateDate = DateTime.Parse(updateDate);
    }
}
