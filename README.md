# RefOverloadsPerfTests
Perf tests for https://github.com/dotnet/corefx/issues/157

## Getting Started

Install the latest daily build of the .NET Core SDK from [here](https://github.com/dotnet/cli#installers-and-binaries).
 - I don't use the native installer, and instead use the .zip/.tar.gz and extract it to some location on disk, say `D:\dotnet`. You can easily delete the location when done.

Add the above `dotnet` executable to your `PATH`.

`$ dotnet restore RefOverloadsPerfTests.csproj`
- This is necessary to download the Microsoft.NETCore.App NuGet package you will need.

In order to use the new `ref` overloads on the System.Numerics types, you need to patch your installation with the proposed changes.

1. git checkout https://github.com/eerhardt/corefx/tree/RefOverloads
2. build -release
3. Copy the built `corefx/bin/AnyOS.AnyCPU.Release/System.Numerics.Vectors/netcoreapp/System.Numerics.Vectors.dll` to `WHERE_YOU_INSTALLED_DOTNET/shared/Microsoft.NETCore.App/2.1.0-version/`
4. Copy the built `corefx/bin/ref/System.Numerics.Vectors/4.1.4.0/netcoreapp/System.Numerics.Vectors.dll` to `NUGET_PACKAGES_ROOT/packages/microsoft.netcore.app/2.1.0-version/ref/netcoreapp2.1/`.
 - `NUGET_PACKAGES_ROOT` is typically `C:\Users\USER\.nuget\` on Windows or `~/.nuget/` on non-Windows.

 `$ dotnet run -c Release`

 to run the perf tests.
