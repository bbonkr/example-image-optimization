using System.Diagnostics;
using System.Runtime.InteropServices;
using Example.Service;

if (args.Length == 0)
{
    Console.WriteLine("Please input file path");
}
else
{
    var filePath = args[0];
    var fileInfo = new FileInfo(filePath);
    var outputPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
    var outputFilePath = Path.Join(outputPath, fileInfo.Name);

    var service = new ImageOptimizationService();

    var contentType = service.GetContentType(fileInfo.Extension?.ToLower());

    using (var stream = File.Open(filePath, FileMode.Open))
    {
        using (var outputFileStream = File.OpenWrite(outputFilePath))
        {
            await service.OptimizeAsync(stream, outputFileStream, contentType, 800, 600);

            outputFileStream.Close();
        }

        stream.Close();
    }

    try
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", outputPath);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start("start", outputPath);
        }
    }
    catch (Exception ex)
    {
        Debug.WriteLine("Could not recognize current OS platform.", ex.Message);
    }

}

