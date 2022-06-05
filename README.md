![Image Unity HDRP Template](https://github.com/DrebotArtem/ResourcesForGitHub/blob/main/Unity%20HDRP%20Template/UnityHDRPTemplateHeader.jpg)

<h1 align="center">Unity HDRP Template</h1>
<p align="center">Unity HDRP template for new projects. Version 2021.3.0f1</p>

## Built With
- C#
- ECS
- DI Container
- Git

## Packages contained
### Unity packages
- Cinemachine
- High Definition RP
- Input System (new)
- Post Processing
- Test Framework
- TextMeshPro
- Timeline
- Unity UI
- Visual Studio Code Editor
- Visual Studio Editor
### Other packages
- [Entitas.](https://github.com/sschmid/Entitas-CSharp#download-entitas) The Entity Component System Framework for C# and Unity.
- [Extenject.](https://github.com/Mathijs-Bakker/Extenject) Extenject is a lightweight highly performant dependency injection framework.
- [Fluent Assertions.](https://github.com/BoundfoxStudios/fluentassertions-unity) A very extensive set of extension methods that allow you to more naturally specify the expected outcome unit tests.
- [NSubstitute.](https://github.com/Thundernerd/Unity3D-NSubstitute) NSubstitute is designed as a friendly substitute for .NET mocking libraries.
- [More Effective Coroutines.](http://trinary.tech/category/mec/) More Effective Coroutines (MEC) is an improved implementation of coroutines. It is a free asset on the Unity asset store.

## Documentation
### Loading System
You can use one of the three base providers to load scenes.
+ **LoadingEmptyProvider.** LoadingEmptyProvider is used to load the scene, without intermediate objects. This provider is used when you don't want to load additional elements such as scenes or prefabs. When using it, you immediately proceed to the loading of operations.
+ **LoadingScreenProvider** LoadingScreenProvider is used as an prefab while we wait for the next scene to load. This provider is used when you want to load an additional prefab before loading the next scene.

    + 📃LoadingScreenProvider is abstract. You must create a new class that inherits from it.
    + ⚠Injection in prefab happens after the Start method is called.
+ **LoadingSceneProvider.** LoadingSceneProvider is used as an intermediate scene while we wait for the next scene to load.  This provider is used when you want to load an additional scene before loading the next scene.
### UI System
Added main menu and basic display settings.
<img src="https://github.com/DrebotArtem/ResourcesForGitHub/blob/main/Unity%20HDRP%20Template/UI%20demonstration.gif" width="921" height="516">

## Future updates
- [ ] Input System
- [x] UI
- [ ] Save System
- [x] Loading System
- [x] Unit Tests

## Author
**Artem Drebot**

- [Profile](https://github.com/DrebotArtem "Artem Drebot")
- [Email](mailto:drebotgs@gmail.com?subject=Hi% "Hi!")
- [LinkedIn](https://linkedin.com/in/drebot-artem "Hire me!")
