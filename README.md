# StateGroup

[![Build status](https://ci.appveyor.com/api/projects/status/rowyp7giphmp8ofs?svg=true)](https://ci.appveyor.com/project/luiszimmermann/stategroup)
State Group and Order receiving a csv or json file. Remote or local.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

```
docker
docker-compose
```
OR
```
NET Core 2.1, 2.1 SDK
Visual Studio or Visual Studio Code (Optional)
```

### Build Image

Clone or Download this repository

```
git clone https://github.com/luiszimmermann/StateGroup.git
```

Build the image with docker-compose

```
docker-compose build
```

This will generate a image called `stategroup`

## Usage

#### Http

Run the container passing 1 argument with the path to the file

```
docker run --rm -ti stategroup http://endereco.url.com/arquivo.csv
```

Or mount a volume and pass the path to the file

```
docker run --rm -ti -v /filesfortest:/tests stategroup /tests/conteudojson
```

## Expected result

```
PR: 4
SC: 9
SP: 1
```

## Testing

Go to the root folder and run

```
dotnet test .\StateGroup.Test\StateGroup.Test.csproj
```

If you are using windows, ignore the only Linux tests

```
dotnet test .\StateGroup.Test\StateGroup.Test.csproj --filter Linux!=true
```

## Built With

* [.NET Core](https://github.com/dotnet/core) - general purpose development platform.
* [Docker](https://www.docker.com/) - container platform.
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) - high-performance JSON framework for .NET
* [CsvHelper](https://rometools.github.io/rome/) - library for reading and writing CSV files.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details