
Function Build([string] $target)
{
	$solutionPath = "ChaiFoxes.FMODAudio\ChaiFoxes.FMODAudio.sln"
	msbuild -t:$target -p:Configuration=Release -nologo $solutionPath
}

Build("restore")
Build("pack")
