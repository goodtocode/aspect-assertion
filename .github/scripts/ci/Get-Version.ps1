#-----------------------------------------------------------------------
<#
Get-Version [-VersionToReplace <String>] [-Major <String>] [-Minor <String>] [-Revision <String>] [-Build <String>] [-Patch <String>] [-PreRelease <String>] [-CommitHash <String>]

Example: .\Get-Version.ps1 -Major 1 -Minor 0
#>
#-----------------------------------------------------------------------

# ***
# *** Parameters
# ***
param(
    [Version] $VersionToReplace = '1.0.0',
    [String] $Major = '-1',
    [String] $Minor = '-1',
    [String] $Revision = '-1',
    [String] $Build = '-1',
    [String] $Patch = '-1',
    [String] $PreRelease = '-1',
    [String] $CommitHash = '-1'
)

# ***
# *** Initialize
# ***


# ***
# *** Locals
# ***

# ***
# *** Execute
# ***

# Calculate version parts with defaults
if ($Major -eq '-1') {
    $Major = $VersionToReplace.ToString().Split('.')[0]
}
if ($Minor -eq '-1') {
    $Minor = $VersionToReplace.ToString().Split('.')[1]
}
if ($Revision -eq '-1') {
    $Revision = (Get-Date -UFormat '%j').ToString() # DayOfYear 1-365
}
if ($Build -eq '-1') {
    $Build = (Get-Date -UFormat '%H%M').ToString() # HrMin 0000-2359
}
if ($Patch -eq '-1') {
    $Patch = (Get-Date -UFormat '%m').ToString() # Month 01-12
}
if ($PreRelease -eq '-1') {
    $PreRelease = ''
}
if ($CommitHash -eq '-1') {
    $CommitHash = ''
}

# Version Formats
$FileVersion = "$Major.$Minor.$Revision.$Build" # e.g. 1.0.0.0
$AssemblyVersion = "$Major.$Minor.0.0"
$InformationalVersion = "$Major.$Minor.$Revision$PreRelease$CommitHash"
$SemanticVersion = "$Major.$Minor.$Patch$PreRelease"

$result = [PSCustomObject]@{
	FileVersion = $FileVersion
	AssemblyVersion = $AssemblyVersion
	InformationalVersion = $InformationalVersion
	SemanticVersion = $SemanticVersion
}

$result | ConvertTo-Json -Compress
