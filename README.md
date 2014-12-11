![Sitecore logo](http://www.sitecore.net/images/logo.png)

# Sitecore.Mvc.Contrib

This project extends the base Sitecore MVC functionality, providing a set of common classes and helpers that you may need while using the Model-View-Controller paradigm with Sitecore.


## Requirements

* .NET Framework 4.5

* Visual Studio 2012 with NuGet Package Manager extension installed. 

    Ensure that the Package Restore checkbox has been ticked in the NuGet Package Manager Options dialog.

* MVC 5

* Sitecore 7.5 or later.


## Building

* Place your `Sitecore.Kernel.dll`, `Sitecore.Mvc.dll` and the `Sitecore.Logging.dll` assmeblies in the `\lib` directory. For more details refer to the [README.md](/lib/README.md) file in the lib folder.

* Run `.\build`  from the root of the checkout folder to invoke an automated debug build of the project. The outputs from this automated build are detailed in the build folder [README.md](/build/README.md) file.


## Installation

Options for installation are:

* Include `Sitecore.Mvc.Contrib.dll` as part of your project 

* Add the project as part of your solution

*   Install one of the contrib NuGet packages

    `PM> Install-Package Sitecore.Mvc.Contrib`

	Refer to the Detailed instructions for using the NuGet packages section.

    
### Detailed instructions for using the NuGet packages

* Ensure that the Sitecore website has been configured correctly for MVC operation. Your CSPROJ file should be set to target .NET framework 4.5.

* Connect website project to Sitecore.Rocks connection for your website

* From the Package Manager Console in Visual Studio execute one of the following commands:

    `Install-Package Sitecore.Mvc.Contrib`

	or

	`Install-Package Sitecore.Mvc.Contrib.Sample`

	The sample package installs both the contrib project plus some sample content within Sitecore that can be used to better understand how to use the contrib project code.

* Once the package installation has been completed a readme.txt file will be displayed in the Visual Studio IDE. Follow the instructions in this readme file to complete the installation of the contrib package.


## Versioning

For transparency and insight into our release cycle, and for striving to maintain backward compatibility, Sitecore.Mvc.Contrib will be maintained under the Semantic Versioning guidelines as much as possible.

Releases will be numbered with the following format:

    <major>.<minor>.<patch>

And constructed with the following guidelines:

* Breaking backward compatibility bumps the major (and resets the minor and patch)
* New additions without breaking backward compatibility bumps the minor (and resets the patch)
* Bug fixes and misc changes bumps the patch

For more information on SemVer, please visit http://semver.org/.

## Bug tracker

Have a bug or a feature request? [Please open a new issue](https://github.com/Sitecore-Community/Sitecore-Mvc-Contrib/issues). Before opening any issue, please search for existing issues and read the [Issue Guidelines](https://github.com/necolas/issue-guidelines), written by [Nicolas Gallagher](https://github.com/necolas/).

## Contributions

Please ensure that any assemblies added to the solution are compatible with Visual Studio 2012.

## Authors and credits

Project maintained by:

* [Kern Herskind](http://twitter.com/herskinduk)
* [Kevin Obee](http://twitter.com/kevinobee)

Contributors:

* [Kevin Buckley](https://twitter.com/KevinPBuckley)
* [Mike Edwards](https://twitter.com/mikeedwards83)
* [Stephen Pope](https://twitter.com/stephenpope)


## Copyright and license

Copyright 2014 Sitecore Corporation A/S

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with 
the License. You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for          
the specific language governing permissions and limitations under the License.      