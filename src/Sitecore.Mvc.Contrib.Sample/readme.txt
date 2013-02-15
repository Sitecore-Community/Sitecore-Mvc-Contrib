Congratulations, the Sitecore.Mvc.Contrib.Sample package is now installed in your website project.

Next Steps
==========

1. Run a build on your Visual Studio solution

2. Publish the master database using Sitecore Rocks.

3. To view the sample site browse to:

   http://<domain>/sample



Fixing Installation Issues
==========================

If the sample package failed to install check the following:

*  Is your website projected configured to use a Sitecore.Rocks connection?

   Select the Sitecore -> Connect... option on the website project in the Solution Explorer.

*  Is the Sitecore.Rocks connection located outside of the <Local IIS Sites> folder in the Sitecore Explorer?

   Drag the website connection in the Sitecore.Explorer and move it so that it resides as a child of the Connections folder.

*  Serialised items fail to install after you've checked everything else?

   Update the Server Components for the Rocks connection to your website, i.e. reinstall the Sitecore.Rocks.Server component.

*  Does your solution build fail to restore NuGet packages?

   Ensure that the Package Restore checkbox has been ticked in the NuGet Package Manager Options dialog.


To retry the package installation first clean up the previous installation attempt by running the following commands in the Package Manager Console:

    Uninstall-Package Sitecore.Mvc.Contrib.Sample
    Uninstall-Package Sitecore.Mvc.Contrib.CastleWindsor
	Uninstall-Package Sitecore.Mvc.Contrib



Uninstalling the Package
========================

The package can be uninstalled by running the following commands in the Package Manager Console:

    Uninstall-Package Sitecore.Mvc.Contrib.Sample
    Uninstall-Package Sitecore.Mvc.Contrib.CastleWindsor
	Uninstall-Package Sitecore.Mvc.Contrib


Note: It is possible to just uninstall the sample package on it's own and leave the core contrib project and it's functionality in place.

