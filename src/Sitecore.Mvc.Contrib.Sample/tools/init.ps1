param($rootPath, $toolsPath, $package, $project)

# Sitecore package management
if ((test-path (join-path $toolsPath uninstall.log)) -ne $true) 
{
  $module = (join-path $env:temp Sitecore.NuGet.1.0.dll);
  if ((test-path $module) -ne $true) { copy-item (join-path $toolsPath Sitecore.NuGet.1.0.dll) $module; }
  import-module $module;
                   
  install-rocksplugin -toolspath $toolsPath -dte $dte;
  install-itemtemplates -toolspath $toolsPath;
  install-appdata -toolspath $toolsPath;
  install-programfiles -toolspath $toolsPath;
}