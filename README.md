<a id="readme-top"></a>
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![Unlicense License][license-shield]][license-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/kinaterme/anti-mindblock">
    <img src="img/logo_rounded.png" alt="Logo" width="256" height="256">
  </a>
  <h3 align="center">Anti Mindblock</h3>

  <p align="center">
    Beat osu! mindblock once and for all!
    <br />
    <a href="#getting-started"><strong>Get started »</strong></a>
    <br />
    <br />
    <a href="https://github.com/kinaterme/anti-mindblock/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/kinaterme/anti-mindblock/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about">About</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#building">Building</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>



<!-- ABOUT -->
## About

**Anti Mindblock** is a utility designed to help osu! players overcome mental barriers by flipping both the screen and in-game skin upside down.

### Features
* **Input flipping**: Supports flipping tablet and mouse input.
* **Cross-platform**: Works on Windows, macOS, and Linux.
* **Supports both clients**: Compatible with osu!(stable) and osu!(lazer).

Interested? Jump to the <a href="#installation">installation instructions</a>.

<!-- GETTING STARTED -->
## Getting Started

To get Anti Mindblock up and running follow these simple steps.

### Installation
#### Windows
_Instructions coming soon._

#### macOS
_Instructions coming soon._

#### Linux
_Instructions coming soon._

### Building

To compile Anti Mindblock from source, follow the steps below.

#### Windows
  1. Install [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
  2. Clone the repository
     ```sh
     git clone https://github.com/kinaterme/anti-mindblock
     ```
  3. Restore dependencies
     ```sh
     cd anti-mindblock
     dotnet restore
     ```
  4. Build the project
     ```sh
     dotnet publish -c Release
     ```
  5. Run the executable
     ```sh
     .\bin\Release\net9.0\anti-mindblock.exe
     ```
#### Linux
  1. Install [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
  2. Clone the repository
     ```sh
     git clone https://github.com/kinaterme/anti-mindblock
     ```
  3. Restore dependencies
     ```sh
     cd anti-mindblock
     dotnet restore
     ```
  4. Build the project
     ```sh
     dotnet publish -c Release
     ```
  5. Run the executable
     ```sh
     ./bin/Release/net9.0/anti-mindblock
     ```

<!-- ROADMAP -->
## Roadmap
- [ ] Windows
  - [ ] Skin flipping
    - [ ] osu!(stable)
    - [X] osu!(lazer)
  - [ ] Input flipping
  - [ ] Screen flipping
- [ ] macOS
  - [ ] Skin flipping
    - [X] osu!(lazer)
  - [ ] Input flipping
  - [ ] Screen flipping
- [ ] Linux
  - [ ] Skin flipping
    - [ ] osu!(stable)
    - [X] osu!(lazer)
  - [ ] Input flipping
  - [ ] Screen flipping
    - [X] x11
    - [ ] Wayland

<!--See the [open issues](https://github.com/kinaterme/anti-mindblock/issues) for a full list of proposed features (and known issues).-->

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request.
<!--
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
-->

<!--
### Top contributors:

<a href="https://github.com/kinaterme/anti-mindblock/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=kinaterme/anti-mindblock" alt="contrib.rocks image" />
</a>

<p align="right">(<a href="#readme-top">back to top</a>)</p>
-->


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/kinaterme/anti-mindblock.svg?style=for-the-badge
[contributors-url]: https://github.com/kinaterme/anti-mindblock/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/kinaterme/anti-mindblock.svg?style=for-the-badge
[forks-url]: https://github.com/kinaterme/anti-mindblock/network/members
[stars-shield]: https://img.shields.io/github/stars/kinaterme/anti-mindblock.svg?style=for-the-badge
[stars-url]: https://github.com/kinaterme/anti-mindblock/stargazers
[issues-shield]: https://img.shields.io/github/issues/kinaterme/anti-mindblock.svg?style=for-the-badge
[issues-url]: https://github.com/kinaterme/anti-mindblock/issues
[license-shield]: https://img.shields.io/github/license/kinaterme/anti-mindblock.svg?style=for-the-badge
[license-url]: https://github.com/kinaterme/anti-mindblock/blob/master/LICENSE
[product-screenshot]: img/screenshot.png
