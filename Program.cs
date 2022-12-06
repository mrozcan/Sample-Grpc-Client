using Grpc.Net.Client;
using SampleGrpcClient.DataTransferObjects;

namespace SampleGrpcClient;
class Program
{
    static void Main(string[] args)
    {
        // Create client
        var sampleClient = new Sample.SampleClient(GrpcChannel.ForAddress("http://localhost:5111"));

        // Call
        var replyResult = sampleClient.GetAll(new GetAllRequest(), new Grpc.Core.Metadata());

        // Create mapper
        var mapper = new MapsterMapper.Mapper();

        // Map and print out data
        var mappedValue = mapper.Map<List<SampleDto>>(replyResult.Sample);

        foreach (var data in mappedValue)
        {
            System.Console.WriteLine("Guid: {0}, Create Date: {1}, Update Date: {2}, Sample String: {3}, Sample Int: {4}"
            , data.Id, data.CreateDate, data.UpdateDate, data.SampleStringData, data.SampleIntData);
        }

    }
}
