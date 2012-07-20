Sitecore-Mvc-Contrib
====================

This project contains a set of common classes and helpers you may need while using `Sitecore MVC`

Building
--------
* Place your `Sitecore.Kernel.dll` and `Sitecore.Mvc.dll` in the '\lib' directory.
* Open the solution and build! 

Installation
-----
Include `Sitecore.Mvc.Contrib.sll' as part of your project or add the project as part of your solution.

Requirements
----

Requires `Sitecore v6.6` or above. MVC also need to be configured (see docs or SDN for more info.)

Usage
-----
###Model###
Create a class and inherit from `Sitecore.Mvc.Contrib.Models.ItemModel`. (from JohnWest blog)

###Controllers###
Create a class and inherit from `Sitecore.Mvc.Contrib.Controllers.StandardController`.

###Views###
Contains 'WebPageView' helper. Add `@inherits Sitecore.Mvc.Contrib.Views.WebViewPage<MODELTYPEHERE>` to the top of your view.
(from JohnWest blog)
