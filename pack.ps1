
Function Build([string] $target)
{
	$solutionPath = "FmodForFoxes\FmodForFoxes.sln"
	msbuild -t:$target -p:Configuration=Release -nologo $solutionPath
}

Build("restore")
Build("pack")
