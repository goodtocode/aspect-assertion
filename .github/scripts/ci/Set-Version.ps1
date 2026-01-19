#-----------------------------------------------------------------------
# Set-Version [-Path [<String>]] [-VersionToReplace [<String>]]  [-Type [<String>]] 
#
# Example: .\Set-Version -Path \\source\path -Major 1
#-----------------------------------------------------------------------

# ***
# *** Parameters
# ***
param
(
	[Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
    [string] $Path=$(throw '-Path is a required parameter. i.e. $(Build.SourcesDirectory)'),
	[Version] $VersionToReplace='1.0.0',
	[String] $Major='-1',
	[String] $Minor='-1',
	[String] $Revision='-1',
	[String] $Build='-1',
	[String] $Patch='-1',
	[String] $PreRelease='-1',
	[String] $CommitHash='-1'
)

# ***
# *** Initialize
# ***
if ($IsWindows) { Set-ExecutionPolicy Unrestricted -Scope Process -Force }
$VerbosePreference = 'SilentlyContinue' #'Continue'
if ($MyInvocation.MyCommand -and $MyInvocation.MyCommand.Path) {
	[String]$ThisScript = $MyInvocation.MyCommand.Path
	[String]$ThisDir = Split-Path $ThisScript
	[DateTime]$Now = Get-Date
	Write-Debug "*****************************"
	Write-Debug "*** Starting: $ThisScript on $Now"
	Write-Debug "*****************************"
	# Imports
	Import-Module "$ThisDir/../System.psm1"
} else {
	Write-Verbose "No script file context detected. Skipping module import."
}

# ***
# *** Validate and cleanse
# ***
If($IsWindows){
	$Path = Set-Unc -Path $Path
}

# ***
# *** Locals
# ***

# ***
# *** Execute
# ***
# Calculate versions using Get-Version.ps1
$ThisDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$getVersionScript = Join-Path $ThisDir 'Get-Version.ps1'
$getVersionArgs = @()
if ($Major -ne '-1') { $getVersionArgs += '-Major'; $getVersionArgs += $Major }
if ($Minor -ne '-1') { $getVersionArgs += '-Minor'; $getVersionArgs += $Minor }
if ($Revision -ne '-1') { $getVersionArgs += '-Revision'; $getVersionArgs += $Revision }
if ($Build -ne '-1') { $getVersionArgs += '-Build'; $getVersionArgs += $Build }
if ($Patch -ne '-1') { $getVersionArgs += '-Patch'; $getVersionArgs += $Patch }
if ($PreRelease -ne '-1') { $getVersionArgs += '-PreRelease'; $getVersionArgs += $PreRelease }
if ($CommitHash -ne '-1') { $getVersionArgs += '-CommitHash'; $getVersionArgs += $CommitHash }
if ($VersionToReplace -ne '1.0.0') { $getVersionArgs += '-VersionToReplace'; $getVersionArgs += $VersionToReplace }

$versionJson = & $getVersionScript @getVersionArgs
$versionObj = $versionJson | ConvertFrom-Json

$FileVersion = $versionObj.FileVersion
$AssemblyVersion = $versionObj.AssemblyVersion
$InformationalVersion = $versionObj.InformationalVersion
$SemanticVersion = $versionObj.SemanticVersion

Write-Debug "FileVersion: $FileVersion SemanticVersion: $SemanticVersion AssemblyVersion: $AssemblyVersion InformationalVersion: $InformationalVersion"
# Calculate versions using Get-Version.ps1
$ThisDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$getVersionScript = Join-Path $ThisDir 'Get-Version.ps1'
$getVersionArgs = @()
if ($Major -ne '-1') { $getVersionArgs += '-Major'; $getVersionArgs += $Major }
if ($Minor -ne '-1') { $getVersionArgs += '-Minor'; $getVersionArgs += $Minor }
if ($Revision -ne '-1') { $getVersionArgs += '-Revision'; $getVersionArgs += $Revision }
if ($Build -ne '-1') { $getVersionArgs += '-Build'; $getVersionArgs += $Build }
if ($Patch -ne '-1') { $getVersionArgs += '-Patch'; $getVersionArgs += $Patch }
if ($PreRelease -ne '-1') { $getVersionArgs += '-PreRelease'; $getVersionArgs += $PreRelease }
if ($CommitHash -ne '-1') { $getVersionArgs += '-CommitHash'; $getVersionArgs += $CommitHash }
if ($VersionToReplace -ne '1.0.0') { $getVersionArgs += '-VersionToReplace'; $getVersionArgs += $VersionToReplace }

$versionJson = & $getVersionScript @getVersionArgs
$versionObj = $versionJson | ConvertFrom-Json

$FileVersion = $versionObj.FileVersion
$AssemblyVersion = $versionObj.AssemblyVersion
$InformationalVersion = $versionObj.InformationalVersion
$SemanticVersion = $versionObj.SemanticVersion

# Package.json version
Update-LineByContains -Path $Path -Contains 'version' -Line """version"": ""$FileVersion""," -Include package.json
# OpenApiConfigurationOptions.cs version
Update-LineByContains -Path $Path -Contains 'Version' -Line "Version = ""$AssemblyVersion""," -Include OpenApiConfigurationOptions.cs
# *.csproj C# Project files
Update-ContentsByTag -Path $Path -Value $FileVersion -Open '<Version>' -Close '</Version>' -Include *.csproj
# *.nuspec NuGet packages
Update-ContentsByTag -Path $Path -Value $SemanticVersion -Open '<version>' -Close '</version>' -Include *.nuspec
# Assembly.cs C# assembly manifest
Update-LineByContains -Path $Path -Contains "FileVersion(" -Line "[assembly: FileVersion(""$FileVersion"")]" -Include AssemblyInfo.cs
Update-LineByContains -Path $Path -Contains "AssemblyVersion(" -Line "[assembly: AssemblyVersion(""$AssemblyVersion"")]" -Include AssemblyInfo.cs
# *.vsixmanifest VSIX Visual Studio Templates
Update-TextByContains -Path $Path -Contains "<Identity Id" -Old $VersionToReplace -New $FileVersion -Include *.vsixmanifest

Write-Output $FileVersion
