using Google.Protobuf.WellKnownTypes;

namespace SampleGrpcClient.DataTransferObjects;

public class SampleDto
{
    public Guid Id { get; init; }
    public Timestamp? CreateDate { get; set; }
    public Timestamp? UpdateDate { get; set; }
    public string? SampleStringData { get; set; }
    public int SampleIntData { get; set; }

}
