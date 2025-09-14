
{ pkgs, ... }: {
  # Ve a https://ryancunderwood.dev/2023/10/19/nix-dev-environments-for-dotnet/ para obtener más información.
  # Ten en cuenta que si cambias la versión del SDK aquí, también querrás cambiarla en el archivo `global.json` en la raíz de tu repositorio.
  packages = [
    pkgs.dotnet-sdk_8
    pkgs.unzip
  ];
  # Variables de entorno
  env.DOTNET_SDK_VERSION = "8.0.100";
}
