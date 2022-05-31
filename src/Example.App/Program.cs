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

    using (var input = File.Open(filePath, FileMode.Open))
    {
        using (var output = File.OpenWrite(outputFilePath))
        {
            await service.OptimizeAsync(input, output, contentType, 800, 600);

            output.Close();
        }

        input.Close();
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

