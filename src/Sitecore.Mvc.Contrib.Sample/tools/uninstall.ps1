# Please notice that uninstall.ps1 will only be execute, if the NuGet /content folder is not empty.
 
param($rootPath, $toolsPath, $package, $project)

# Sitecore package management
if ((test-path (join-path $toolsPath uninstall.log)) -or (test-path (join-path $toolsPath uninstall.items.log)))
{
  $module = (join-path $env:temp Sitecore.NuGet.1.0.dll);
  if ((test-path $module) -ne $true) { copy-item (join-path $toolsPath Sitecore.NuGet.1.0.dll) $module; }
  import-module $module;

  uninstall-items -toolspath $toolsPath -project $project -dte $dte;
  uninstall-files -toolspath $toolsPath;
  uninstall-serverfiles -toolspath $toolsPath -project $project -dte $dte;
}