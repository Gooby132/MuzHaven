using Microsoft.Extensions.Options;
using StemService.Domain.Services;
using StemService.Infrastructure.FileServices.FFmpegFileService.Core;
using StemService.Infrastructure.FileServices.FFmpegFileService.Options;

namespace StemService.Unit.Tests.FFmpegFileServiceTests;

[TestClass]
public class SaveFileTest
{
    public static readonly DirectoryInfo Resources = 
        new DirectoryInfo("./Resources");

    public static readonly FileInfo Mp3_1 = 
        new FileInfo(Path.Join(Resources.FullName, "1.mp3"));

    private IFileService _fileService;

    [TestInitialize]
    public void Initialize()
    {
        _fileService = new FFmpegFileSystemService(Options.Create(new FFmpegFileServiceOptions
        {
            BaseDirectory = "test"
        }));
    }

    [TestMethod]
    public async Task SaveMp3FileTest_Successful()
    {
        var mediaFile = await _fileService.SaveMediaFile(Mp3_1.OpenRead(), "1.mp3");

        Assert.IsTrue(mediaFile.IsSuccess);
        Assert.IsNotNull(mediaFile.Value.AmplitudesPoints,"amplitude points were not generated");
        Assert.IsTrue(mediaFile.Value.AmplitudesPoints.Count == 100);
        Assert.IsTrue(mediaFile.Value.Format == "wav");
        Assert.IsNotNull(mediaFile.Value.Path);
    }

    [TestMethod]
    public async Task OpenFileTest_Successful()
    {
        var mediaFile = await _fileService.SaveMediaFile(Mp3_1.OpenRead(), "1.mp3");

        Assert.IsTrue(mediaFile.IsSuccess);
        Assert.IsNotNull(mediaFile.Value.AmplitudesPoints, "amplitude points were not generated");
        Assert.IsTrue(mediaFile.Value.AmplitudesPoints.Count == 100);
        Assert.IsTrue(mediaFile.Value.Format == "wav");
        Assert.IsNotNull(mediaFile.Value.Path);

        var stream = await _fileService.GetStem(mediaFile.Value.Path);
        
        Assert.IsTrue(stream.IsSuccess);
    }
}
