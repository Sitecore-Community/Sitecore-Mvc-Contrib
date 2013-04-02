param($target = "..\")

$header =  
"//-----------------------------------------------------------------------------------------------------------------------
//
// Copyright 2013 Sitecore Corporation A/S
//
// Licensed under the Apache License, Version 2.0 (the `"License`"); you may not use this file except in compliance with 
// the License. You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
// `"AS IS`" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for          
// the specific language governing permissions and limitations under the License.                                         
// 
//-----------------------------------------------------------------------------------------------------------------------`r`n"
 
function Write-Header ($file) 
{ 
    $content = Get-Content $file 
    $filename = Split-Path -Leaf $file 
    $fileheader = $header -f $filename,$companyname,$date 
    
    Set-Content $file $fileheader 
    Add-Content $file $content 
} 
 
$files = Get-ChildItem $target -Recurse | where { $_.Extension -like ".cs" } 

foreach ($file in $files)
{
	 Write-Header $file.FullName
}