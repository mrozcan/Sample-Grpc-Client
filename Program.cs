using System.Net;
using Grpc.Net.Client;
using SampleGrpcClient.DataTransferObjects;

namespace SampleGrpcClient;
class Program
{
    static void Main(string[] args)
    {
        #region Init

        // Configure Http client
        var httpClient = ConfigureHttpClient();

        // Create mapper
        var mapper = new MapsterMapper.Mapper();
        // Create new configuration for converting data
        mapper.Config.NewConfig<SampleData, SampleGrpcDto>()
            .ConstructUsing(src => new SampleGrpcDto(src.Id, src.CreateDate, src.UpdateDate, src.SampleStringData, src.SampleIntData));

        // Create sample grpc client
        var sampleClient = new Sample.SampleClient(
            GrpcChannel.ForAddress(
                address: "https://localhost:5111",
                channelOptions: new GrpcChannelOptions()
                {
                    HttpClient = httpClient,
                    MaxRetryAttempts = 5,
                }
            )
        );

        #endregion

        // Call
        var replyResult = sampleClient.GetAll(new GetAllRequest(), new Grpc.Core.Metadata());

        // Map and print out data
        var mappedSampleData = mapper.Map<List<SampleData>>(replyResult.Sample);
        var mappedValue = mapper.Map<List<SampleGrpcDto>>(mappedSampleData);

        foreach (var data in mappedValue)
        {
            System.Console.WriteLine("Guid: {0}, Create Date: {1}, Update Date: {2}, Sample String: {3}, Sample Int: {4}"
            , data.Id, data.CreateDate, data.UpdateDate, data.SampleStringData, data.SampleIntData);
        }

    }

    static HttpClient ConfigureHttpClient()
    {
        // Create new http client
        var httpClient = new HttpClient();
        // Set http request version to Http3
        httpClient.DefaultRequestVersion = HttpVersion.Version30;
        // Set default version policy to accept Http3 or lower version of http
        httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;

        return httpClient;
    }

}
