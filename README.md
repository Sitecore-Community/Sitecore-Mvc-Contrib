# Sitecore.Mvc.Contrib

This project extends the basse MVC functionality introduced in Sitecore 6.6, providing a set of common classes and helpers that you may need while using Sitecore MVC.


## Building

* Place your `Sitecore.Kernel.dll`, `Sitecore.Mvc.dll` and the `Sitecore.Logging.dll` assmeblies in the `\lib` directory.
* Run `.\build`  from the root of the checkout folder to invoke an automated debug build of the project. The outputs from this automated build are detailed in the build folder `README.md` file.


## Requirements

* .NET Framework 4

* MVC 3 

* Sitecore v6.6 or above. MVC also need to be configured (see docs or SDN for more info).


## Installation

Options for installation are:

* Include `Sitecore.Mvc.Contrib.dll` as part of your project 

* Add the project as part of your solution

*   Install one of the contrib NuGet packages

    `PM> Install-Package Sitecore.Mvc.Contrib`

	Refer to the Detailed instructions for using the NuGet packages section.

    
### Detailed instructions for using the NuGet packages

* Ensure that the Sitecore website has been configured correctly for MVC operation

* Connect website project to Sitecore.Rocks connection for your website

    Note: Currently Sitecore.Nuget fails to find connections located within the <Local IIS Sites> folder. Ensure that your site exists as a direct child of Connections in the Sitecore Explorer.

* Configure a Package source in Visual Studio to point to http://54.228.12.123:8080/nuget

* From the Package Manager Console in Visual Studio execute one of the following command:

    `Install-Package Sitecore.Mvc.Contrib`

	or

	`Install-Package Sitecore.Mvc.Contrib.Sample`

	The sample package installs both the contrib project plus some sample content within Sitecore that can be used to better understand how to use the contrib project code.

* Run a build once the package has been fully installed. Once the package installation has been completed a readme.txt file will be displayed in the Visual Studio IDE.

* Publish the site from within Sitecore.

* If you installed the sample package  you can now browse the sample content using the url:

    `http://domain/sample`

## Contributions

Please ensure that any assemblies added to the solution are compatible with Visual Studio 2010. 