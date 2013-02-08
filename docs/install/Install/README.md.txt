# Installation Instructions

- prerequisites
	- Visual Studio web project attached to Sitecore using the Sitecore.Rocks connector
		- right click on Website project -> Sitecore -> Connect...
		- ensure that the connected site exists outside of <Local IIS Sites>
			- image Site Connection Outside of Local IIS Sites
		- ensure Sitecore.Rocks is configured to use the Hard Rock Web Service
			- image Hard Rock Web Service Connector
			- browse to http://<site>/sitecore/shell/WebService/Service2.asmx, if this service is not available update server components using Sitecore.Rocks connections and check again.
		- Visual Studio project is targets .NETFramework Version v4.0
		- Visual Studio project contains references for Sitecore.Mvc, Sitecore.Logging and Sitecore.Kernal with Copy Local set to false
		- Web.config has the MVC configuration settings contained within Web.config.Mvc applied

	- A NuGet package source is configured within Visual Studio that contains the Sitecore.Mvc.Contrib packages.

	- An understanding of the operations of Sitecore.NuGet
		- ref. http://vsplugins.sitecore.net/Sitecore-NuGet.ashx
		
- From the package manager console inside Visual Studio
	PM> install-package Sitecore.Mvc.Contrib.Sample

- Build the solution