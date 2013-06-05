AzureCloudLogger
=====
A cloud logger using Azure Table Storage.



Dependencies
-----
This library requires [WindowsAzure.Storage](https://github.com/WindowsAzure/azure-sdk-for-net) (version 2.0+) and its dependencies.



Usages
-----
Simpliest form of its usage.

```cs
string storageName = "AZURE_STORAGE_NAME";
string accessKey = "AZURE_STORAGE_ACCESS_KEY";
string tableName = "AZURE_TABLE_STORAGE_NAME";
string logLevel = "DEBUG";
AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, logLevel);

logger.Debug("Hello World");
```

Check out [wiki](https://github.com/rockacola/AzureCloudLogger/wiki) for detailed documentations.



Inspect Logs
-----
One of the easiest ways to inspect your logs is by using [Neudesic's Azure Storage Explorer](http://azurestorageexplorer.codeplex.com/). Recommended using its latest stable release, version 4.0.0.9.



Limitations
-----
There are some limitations of current version of AzureCloudLogger you should be aware of. These are the known issues that are bound to be addressed in future versions:

* There is no default logging level, hence it is compulsory to always provide a logging level upon its construction.




License
-----
Copyright (c) Travis Lin licensed under the [MIT License](https://github.com/rockacola/AzureCloudLogger/blob/master/LICENSE.txt). You are free to use/modify the source code whatever you want, as long as you retain names of contributor(s) of this project.
