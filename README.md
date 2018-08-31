# fsharp-common
Collection of common F# functions and types

## Files
- [DotEnv.fs](./DotEnv.fs)
  - F# functions for working with environment variables and secrets
- [Result.fs](./Result.fs)
  - F# types and functions for Railway Oriented Programming from Scott Wlashin's [Domain Modeling Made Functional](https://github.com/swlaschin/DomainModelingMadeFunctional).
  - See specific [LICENSE](https://github.com/swlaschin/DomainModelingMadeFunctional/blob/master/LICENSE) for his
  repository.
  - I highly recommend buying the book [Domain Modeling Made Functional](https://pragprog.com/book/swdddf/domain-modeling-made-functional)
- [SimpleType.fs](./SimpleType.fs)
  - F# types and functions for creating simple types mostly from Scott Wlashin's [Domain Modeling Made Functional](https://github.com/swlaschin/DomainModelingMadeFunctional).
  - See specific [LICENSE](https://github.com/swlaschin/DomainModelingMadeFunctional/blob/master/LICENSE) for his
  repository.

## Usage
1) Add [Paket](https://fsprojects.github.io/Paket/) to your project.
2) Add [paket.dependencies](https://fsprojects.github.io/Paket/dependencies-file.html)
file to your project.
3) Add a reference to the file you would like to use. For example, 
add the [Result.fs](./Result.fs) file in the paket.dependencies file:
    ```
    source https://www.nuget.org/api/v2

    github ameier38/fsharp-common Result.fs
    ```
4) Install dependencies
    ```
    $ ./.paket/paket.exe install
    ```

## Similar Projects
- [haf/YoLo](https://github.com/haf/YoLo)
- [FSharpPlus](https://github.com/fsprojects/FSharpPlus)