# AmbientContext.LogService.Serilog

The Serilog AmbientLogService provides a way to address the cross cutting concern of Logging throughout
an application, using the logging framework Serilog.

The use of such an ambient service removes the need for cross cutting concerns to pollute the constructors
of classes throughout a project.

It provides an ambient logging context behind the facade of an instance based reference.

# Usage

The AmbientLogService should be added to a class as a read only public property.

It should be read only to prevent the reference to the instance of the AmbientLogService being changed and
it should be a public property to allow setting of the ILogger instance for testing purposes.

For those who feel their "spidey sense" tingling at making a dependency such as this public, I suggest 
you remember the fact that you were probably passing it in on the constructor anyway, so you already had
access to the instance.

As Mark Seemaans states regarding TDD, 

People unused to TDD often react in one of two ways:

* "I don't like adding members only for testing"
* "It breaks encapsulation"

The excellent blog post of his on [Structural Inspection](http://blog.ploeh.dk/2013/04/04/structural-inspection/)
 is here for further reading

```CSharp
public class Foo
{
    public AmbientLogService Logger {get;} = new AmbientLogService();
}
```

# Testing

If a unit test is required to document logging requirements, the instance on the AmbientLogService can be set 
to a mock rather than the real Serilog instance.


```CSharp
// inside a unit test
sut.Logger.Instance = _mockLogger;
```


