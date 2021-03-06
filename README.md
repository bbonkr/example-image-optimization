Resize image file using Stream.

## Requirements

* .NET 6+

## Overview

* input image file: 3982x2240 (3,285,713 btyes)
* output image file: 800x450 (189,442 bytes)

## Usages

### Debug 

```bash
$ cd src/Example.App
$ dotnet run -- <image file path>
```

### Comments

Get content-type using file extension.

> It needs image file encoder.


```csharp
var contentType = service.GetContentType(fileInfo.Extension?.ToLower());
```

Resize image file.

> Resizes the image to the closest ratio to the size entered.

```csharp
using (var input = File.Open(filePath, FileMode.Open))
{
    using (var output = File.OpenWrite(outputFilePath))
    {
        await service.OptimizeAsync(input, output, contentType, 800, 600);

        output.Close();
    }

    input.Close();
}
```