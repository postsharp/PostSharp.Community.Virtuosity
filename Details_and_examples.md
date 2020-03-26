## How Virtuosity works
You apply the `[Virtual]` attribute to methods using [multicasting](https://github.com/postsharp/Home/blob/master/multicasting.md). 

The methods and properties that you apply `[Virtual]` to this way are turned into `virtual` or `override` methods or properties, as appropriate. 

The following methods are excluded because they can't be virtual:
* static methods
* private methods
* methods in sealed classes

## More examples
Make all methods within class `A` virtual:
```csharp
[Virtual]
public class A { ... }
```
Make all public methods virtual (but not protected or internal methods):
```csharp
[assembly: Virtual(AttributeTargetMemberAttributes = MulticastAttributes.Public)]
```
Make all methods within the namespace `My.Namespace` virtual:
```
[assembly: Virtual(AttributeTargetTypes = "My.Namespace.*")]
```
Advanced case: If you apply `[Virtual]` to both a base method a `new` method with the same signature, it becomes an override.

So, if your code is:
```csharp
[assembly: Virtual]
class Base {
    public int ReturnAnswer() => 42;
}
class Derived {
    public new int ReturnAnswer() => 6*9;
}
```
Then what gets compiled is:

```csharp
class Base {
    public virtual int ReturnAnswer() => 42;
}
class Derived {
    public override int ReturnAnswer() => 6*9;
}
```

## Why is this useful

Maybe you're using one of the following tools:

 * [NHibernate](https://nhibernate.info/)
 * [Rhino Mocks](https://hibernatingrhinos.com/oss/rhino-mocks)
 * [Moq](https://github.com/moq/moq)
 * [Ninject](http://www.ninject.org/)
 * [NSubstitute](https://nsubstitute.github.io/)
 * [Entity Framework](https://docs.microsoft.com/en-us/ef/)

All these tools make use of [DynamicProxy](http://www.castleproject.org/projects/dynamicproxy/). DynamicProxy allows for runtime interception of members. The one caveat is that all intercepted members must be virtual. This means that that all the above tools, to some extent, require members to be virtual:

 * [EF: Lazy Loading](https://docs.microsoft.com/en-us/ef/ef6/querying/related-data#lazy-loading) "When using POCO entity types, lazy loading is achieved by creating instances of derived proxy types and then overriding virtual properties to add the loading hook"
 * [Must Everything Be Virtual With NHibernate?](http://davybrion.com/blog/2009/03/must-everything-be-virtual-with-nhibernate/)
 * [RhinoMocks: Why methods need to be declared virtual](http://groups.google.com/group/RhinoMocks/browse_thread/thread/a2cb93f1ba8d4735/37d377ddb92cb729?lnk=gst&q=virtual)
 * [Moq: My method needs to be virtual?](http://groups.google.com/group/moqdisc/browse_thread/thread/2e02e367d017f274)
 * ["NMock supports mocking of classes with virtual methods"](http://www.nmock.org/nmock1-documentation.html)
 * [Ninject Important Note:Your methods/properties to be intercepted must be virtual!](http://innovatian.com/2010/03/using-ninject-extensions-interception-part-1-the-basics/)
 * [NSubstitute:Some operations on non virtual members should throw an exception](http://groups.google.com/group/nsubstitute/browse_thread/thread/407cb0408ce97bfd)

When a member is not virtual these tools will not work and fail in sometimes very unhelpful ways. So rather than having to remember to use the virtual keyword, Virtuosity means members will be virtual by default.

You may also just prefer to work with all methods virtual by default, as in Java.