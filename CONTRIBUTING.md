# WinReform Contributor Guide

Contributions to WinReform, whether new features or bug fixes, are deeply appreciated and benefit the whole user community.

The following guidelines help ensure the smooth running of the project, and keep a consistent standard across the codebase. They are guidelines only - should you feel a need to deviate from them it is probably for a good reason - but please adhere to them as closely as possible.

If you'd like to contribute code or documentation to WinReform, we welcome pull requests.

## Code of Conduct

It is expected that all contributors follow the [code of conduct](CODE_OF_CONDUCT.md).

## Process

- **File an issue.** Either suggest a feature or note a defect. If it's a feature, explain the challenge you're facing and how you think the feature should work. If it's a defect, include a description and reproduction.
- **Design discussion.** For new features, some discussion on the issue will take place to determine if it's something that should be included in WinReform. For defects, discussion may happen around whether the issue is truly a defect or if the behavior is correct.
- **Pull request.** Create [a pull request](https://help.github.com/articles/using-pull-requests/) on the `develop` branch of the repository to submit changes to the code based on the information in the issue. Pull requests need to pass the CI build and follow coding standards. See below for more on coding standards in use with WinReform. Note all pull requests should include accompanying unit tests to verify the work.
- **Code review.** Some iteration may take place requiring updates to the pull request (e.g., to fix a typo or add error handling).
- **Pull request acceptance.** The pull request will be accepted into the `develop` branch and pushed to `master` with the next release.

## License

By contributing to WinReform, you assert that:

1. The contribution is your own original work.
2. You have the right to assign the *copyright* for the work (it is not owned by your employer, or you have been given copyright assignment in writing).
3. You license it under the terms applied to the rest of the WinReform project.

## Coding

### Workflow

WinReform follows the [Gitflow workflow process](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow/) for handling releases. This means active development is done on the `develop` branch and we push to `master` when it's release time. **If you're creating a pull request or contribution, please do it on the `develop` branch.** We can then build, test, and release to when everything's verified.

We use [semantic versioning](https://semver.org/) for our project versions.

### Developer Environment

- Visual Studio 2019
- .NET Core SDK 3.1 or up

### Build / Test

- The build is executed by running it in Visual Studio or by executing `msbuild Solution.sln` on the solution in the codeline root.
- Unit tests can be run from the Visual Studio test explorer or by manually executing the command-line unit test runner from the `packages` folder against the built unit test assembly.

Unit tests are written in XUnit and Moq. **Code contributions should include tests that exercise/demonstrate the contribution.**


### Coding Standards

Normal .NET coding guidelines apply. See the [Framework Design Guidelines](https://msdn.microsoft.com/en-us/library/ms229042.aspx) for suggestions. Please try to fix warnings rather than suppressing the message. If you do need to suppress a false positive, use the `[SuppressMessage]` attribute.

WinReform source code uses four spaces for indents. We use [EditorConfig](https://editorconfig.org/) to ensure consistent formatting in code docs. Visual Studio has this built in since VS 2017. VS Code requires the EditorConfig extension. Many other editors also support EditorConfig.

### Breaking Changes

A breaking change can be a lot of things:

- Change a type's namespace.
- Remove or rename a method on a class or interface.
- Move an extension method from one static class to a different static class.
- Add an optional parameter to an existing method.
- Add a new abstract method to an existing abstract class.
- Add a new member on an interface.

### Dependencies

- Projects should be able to be built straight out of Git (no additional installation needs to take place on the developer's machine). This means NuGet package references, not installation of required components.
- Any third-party libraries consumed by WinReform integration must have licenses compatible with WinReform's license.

### Code Documentation

You should  include XML API comments in the code. These are used to generate API documentation as well as for IntelliSense.

**The Golden Rule of Documentation: Write the documentation you'd want to read.** Every developer has seen self explanatory docs and wondered why there wasn't more information. (Parameter: "index." Documentation: "The index.") Please write the documentation you'd want to read if you were a developer first trying to understand how to make use of a feature.
