## <img src="icon.png" width="32"> &nbsp; PostSharp.Community.Virtuosity 
Allows you to make all target methods virtual without adding the virtual keyword to every one of them.

*This is an add-in for [PostSharp](https://postsharp.net). It modifies your assembly during compilation by using IL weaving. The add-in functionality is in preview, and not yet public. This add-in might not work and is unsupported.*
 
#### Example
Your code:
```csharp
[assembly: Virtual]
class A 
{
  public void M1() { }
  protected void M2() { }
  private void M3() { }
}
```
What gets compiled:
```csharp
class A 
{
  public virtual void M1() { }
  protected virtual void M2() { }
  private void M3() { }
}
```

#### Installation (as a user of this plugin)
1. Install the NuGet package: `PM> Install-Package PostSharp.Community.Virtuosity`
2. Get a free PostSharp Community license at https://www.postsharp.net/essentials
3. When you compile for the first time, you'll be asked to enter the license key.

#### How to use
Add `[Virtual]` to the classes, namespaces or assemblies where you want it to apply. We call this [multicasting](https://doc.postsharp.net/attribute-multicasting).

By annotating a class with `[Virtual]`, you make all methods in that class `virtual` or `override`. By annotating the assembly, you make ALL methods virtual. As something in between, you can use the various properties of the `VirtualAttribute` to specify what kinds of methods of what classes should be made virtual.

For more details, read [Details and examples](Details_and_examples.md).

#### Copyright notices
Released under the [MIT license](LICENSE.md).

* Copyright by Simon Cropp, PostSharp Technologies, and other contributors.
* Icon by BomSymbols, https://www.iconfinder.com/korawan_m
