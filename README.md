# F# Utilities
[![Codefresh build status]( https://g.codefresh.io/api/badges/pipeline/ameier38/ameier38%2Ffsharp-utilities%2Ffsharp-utilities?branch=master&key=eyJhbGciOiJIUzI1NiJ9.NWMzMjE0ODA3YTJkOGI3ZjkxMzVhZjlm.WFn4I6XuUDBfWsKEp6LIuG-IlDsT4JCDTjMzeH7kGu8&type=cf-1)]( https://g.codefresh.io/pipelines/fsharp-utilities/builds?repoOwner=ameier38&repoName=fsharp-utilities&serviceName=ameier38%2Ffsharp-utilities&filter=trigger:build~Build;branch:master;pipeline:5c7913b8b3d43d6068fa0cf8~fsharp-utilities)
____
Collection of F# utilities.

## Files
- [Env.fs](./DotEnv.fs)
  - Functions for working with environment variables and secrets
- [Result.fs](./Result.fs)
  - Types and functions for Railway Oriented Programming from Scott Wlaschin's 
  [Domain Modeling Made Functional](https://github.com/swlaschin/DomainModelingMadeFunctional).
  - See his specific [LICENSE](https://github.com/swlaschin/DomainModelingMadeFunctional/blob/master/LICENSE).
- [SimpleType.fs](./SimpleType.fs)
  - Types and functions for creating constrained types from Scott Wlaschin's 
  [Domain Modeling Made Functional](https://github.com/swlaschin/DomainModelingMadeFunctional).
  - See his specific [LICENSE](https://github.com/swlaschin/DomainModelingMadeFunctional/blob/master/LICENSE).
- [Currency.fs](./Currency.fs)
  - Types and functions for working with currencies
- [State.fs](./State.fs)
  - Implementation of State monad with builder

## Usage
1. Add [Paket](https://fsprojects.github.io/Paket/) to your project.
2. Add [paket.dependencies](https://fsprojects.github.io/Paket/dependencies-file.html)
file to your project.
3. Add a reference to the file you would like to use. For example, 
add the [Result.fs](./Result.fs) file in the `paket.dependencies` file:
    ```
    source https://www.nuget.org/api/v2

    github ameier38/fsharp-utilities Result.fs
    ```
4. Add the dependency to the `paket.references` file:
    ```
    File: Result.fs
    ```
5. Install dependencies
    ```
    $ ./.paket/paket.exe install
    ```

## Development
1. Install [FAKE](https://github.com/fsharp/FAKE). See 
[this post](https://andrewcmeier.com/how-to-fake) for a tutorial
on getting started with FAKE.
2. Clone the repo.
    ```
    > git clone https://github.com/ameier38/fsharp-utilities.git
    ```
3. Make your updates.
4. Run the tests.
    ```
    > fake build
    ```

## Resources
- [Domain Modeling Made Functional book](https://pragprog.com/book/swdddf/domain-modeling-made-functional)
- [haf/YoLo](https://github.com/haf/YoLo)
- [Paket GitHub dependencies](https://fsprojects.github.io/Paket/github-dependencies.html)