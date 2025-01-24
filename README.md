# 012 Dependency Inversion Principle

## Lecture

[![# Dependency Inversion Principle(Part 1)](https://img.youtube.com/vi/K_7HHvfSfiQ/0.jpg)](https://www.youtube.com/watch?v=K_7HHvfSfiQ)
[![# Dependency Inversion Principle(Part 2)](https://img.youtube.com/vi/fRwdC6VfEU8/0.jpg)](https://www.youtube.com/watch?v=fRwdC6VfEU8)

## Instructions

In this assignment you refactor HomeEnergyApi to apply the Dependency Inversion Principle to your code.

In `HomeEnergyApi/ApplicationFactory.cs`...
- Create a new public class `ApplicationFactory` implementing `IControllerFactory`.
  - This class should have a public static property `repository` of type `HomeRepository'.
  - This class should have a method `CreateController()`.
    - `CreateController()` should take one argument `context` of type `ControllerContext`.
    - `CreateController()` should initalize `repository` as a new `HomeRepository` if `repository` is currently null.
    - `CreateController()` should return a new `HomesController` with `repository` passed as an argument.
  - This class should have a method `ReleaseController()`
    - `ReleaseController()` should take two arguments `context` of type `ControllerContext`, and `controller` of type `object`
    - `ReleaseController()` should call `disposable.Dispose()` if `controller` is `IDisposable disposable`


In `HomeEnergyApi/Controllers/HomesController.cs`...
- Change the declaration of `repository` to no longer set the value to a new `HomeRepository`, and to make `repository` type `IRepository<int,Home>`
- Create a constructor
  - The constructor should take a `HomeRepository` as an argument
  - The constructor should set the value of `repository` to the passed `HomeRepository`.

In `HomeEnergyApi/Program.cs`
- Add the line `builder.Services.AddSingleton<IControllerFactory, ApplicationFactory>()` before calling `builder.build()`

In `HomeEnergyApi/Models/IRepository.cs`
- Create a public inteface `IRepository<TId, T>`
  - `IRepository<TId, T>` should contain the method `T Save(T entity);`
  - `IRepository<TId, T>` should contain the method `T Update(TId id, T entity);`
  - `IRepository<TId, T>` should contain the method `List<T> FindAll();`
  - `IRepository<TId, T>` should contain the method `T FindById(TId id);`
  - `IRepository<TId, T>` should contain the method `T RemoveById(TId id);`

In `HomeEnergyApi/Models/HomeRepository.cs`
- Have `HomeRepository` implement the interface `IRepository<Tid,T>`

## Building toward CSTA Standards:

- Explain how abstractions hide the underlying implementation details of computing systems embedded in everyday objects. (3A-CS-01) https://www.csteachers.org/page/standards
- Decompose problems into smaller components through systematic analysis, using constructs such as procedures, modules, and/or objects. (3A-AP-17) https://www.csteachers.org/page/standards
- Construct solutions to problems using student-created components, such as procedures, modules and/or objects. (3B-AP-14) https://www.csteachers.org/page/standards
- Compare levels of abstraction and interactions between application software, system software, and hardware layers. (3A-CS-02) https://www.csteachers.org/page/standards

## Resources

- https://en.wikipedia.org/wiki/Factory_(object-oriented_programming)
- https://en.wikipedia.org/wiki/Singleton_pattern
- https://en.wikipedia.org/wiki/Dependency_injection
- https://en.wikipedia.org/wiki/Dependency_inversion_principle

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
