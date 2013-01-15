#Sitecore-Mvc-Contrib

This project contains a set of common classes and helpers you may need while using `Sitecore MVC`

##Building

* Place your `Sitecore.Kernel.dll` and `Sitecore.Mvc.dll` in the `\lib` directory.
* Run `.\build`  from the root of thee checkout folder to invoke an automated release build of the project. The outputs from this automated build are detailed in the build folder `README.md` file.

##Installation

Options for installation are:

* Include `Sitecore.Mvc.Contrib.dll` as part of your project 

* Add the project as part of your solution

*   Use NuGet package

    `PM> Install-Package Sitecore.Mvc.Contrib`


## Requirements

Requires `Sitecore v6.6` or above. MVC also need to be configured (see docs or SDN for more info).

## Usage

### Model
Create a class and inherit from `Sitecore.Mvc.Contrib.Models.ItemModel`. (from John West blog)

### Controllers
`Sitecore.Mvc.Contrib.Controllers.ViewInjectingController`.. Automatically inserts view into placeholder (useful when using routing)

### Views 
Contains `WebPageView` helper. Add `@inherits Sitecore.Mvc.Contrib.Views.WebViewPage<MODELTYPEHERE>` to the top of your view.
(from John West blog)

### Other
`IRegisterRoutes` allows controllers to self-register their routes. 