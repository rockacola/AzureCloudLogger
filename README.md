AzureCloudLogger
=====

Overview
-----
A cloud logger using Azure Table Storage



Dependencies
-----
This library requires [WindowsAzure.Storage](https://github.com/WindowsAzure/azure-sdk-for-net) (version 2.0+) and its dependencies.



Usages
-----
Simpliest form of its usage.

```
var storageName = "AZURE_STORAGE_NAME";
var accessKey = "AZURE_STORAGE_ACCESS_KEY";
var tableName = "AZURE_TABLE_STORAGE_NAME";
AzureCloudTableLogger logger = new AzureCloudTableLogger(storageName, accessKey, tableName, LogLevel.DEBUG);
   
logger.Debug("Hello World");
```


Limitations
-----
There are some limitations of current version of AzureCloudLogger you should be aware of. These are the known issues that are bound to be addressed in future versions:

* Does not verify correctness of input configruation details.
* There is no default logging level, hence it is compulsory to always provide a logging level upon its construction.
* Compiled DLL binary is only available in .NET 4.5.


License
-----
Copyright (c) Travis Lin licensed under the [MIT License](https://github.com/rockacola/AzureCloudLogger/blob/master/LICENSE.txt). You are free to use/modify the source code whatever you want, as long as you retain names of contributor(s) of this project.



Changelog
-----
### 1.0.0
* First release.


