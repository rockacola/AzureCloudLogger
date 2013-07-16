AzureCloudLogger
=====
A cloud logger using Azure Table Storage.



Dependencies
-----
This library requires [WindowsAzure.Storage](https://github.com/WindowsAzure/azure-sdk-for-net) (version 2.0+) and its dependencies.



Installation
-----
Installation is easy through [NuGet](https://nuget.org/packages/ClickView.AzureCloudLogger). Search for *"azurecloudlogger"* in NuGet manager, or install through Package Manager Console:

```
PM> Install-Package ClickView.AzureCloudLogger 
```

Alternatively, compiled binaries of all versions are available in [Release](https://github.com/rockacola/AzureCloudLogger/tree/master/Releases) directory.



Usages
-----
Simpliest form of its usage.

```cs
string storageName = "AZURE_STORAGE_NAME";
string accessKey = "AZURE_STORAGE_ACCESS_KEY";
string tableName = "AZURE_TABLE_STORAGE_NAME";
AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName);

logger.Warn("Hello World");
```

Check out [wiki](https://github.com/rockacola/AzureCloudLogger/wiki) for detailed documentations.



Inspect Logs
-----
One of the easiest ways to inspect your logs is by using [Neudesic's Azure Storage Explorer](http://azurestorageexplorer.codeplex.com/). Recommended using its latest beta release, [version 5 preview 1](http://azurestorageexplorer.codeplex.com/releases/view/89713).



License
-----
Copyright (c) Travis Lin licensed under the [MIT License](https://github.com/rockacola/AzureCloudLogger/blob/master/LICENSE.txt). You are free to use/modify the source code whatever you want, as long as you retain names of contributor(s) of this project.
