call "c:\Program Files (x86)\Microsoft Visual Studio 8.0\VC\vcvarsall.bat"

pushd c:\src\sil\chorus\build
MSbuild /target:UnzipMercurial /property:teamcity_build_checkoutDir=c:\src\sil\chorus /property:teamcity_dotnet_nunitlauncher_msbuild_task="notthere" /property:BUILD_NUMBER="0.1.345.abcd" /property:Minor="1" build.win.proj
popd
PAUSE

#/verbosity:detailed