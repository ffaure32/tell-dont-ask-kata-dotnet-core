This is a fork of the original Tell Don't Ask Kata of [@rachelcarmena](https://github.com/rachelcarmena/tell-dont-ask-kata). I've only added C#/.NetCore version. 

# tell don't ask kata
A legacy refactor kata, focused on the violation of the [tell don't ask](https://pragprog.com/articles/tell-dont-ask) principle and the [anemic domain model](https://martinfowler.com/bliki/AnemicDomainModel.html).
A second part is dedicated to async/await optimisations

## Part 1
### Instructions
Here you find a simple order flow application. It's able to create orders, do some calculation (totals and taxes), and manage them (approval/reject and shipment).

The old development team did not find the time to build a proper domain model but instead preferred to use a procedural style, building this anemic domain model.
Fortunately, they did at least take the time to write unit tests for the code.

Your new CTO, after many bugs caused by this application, asked you to refactor this code to make it more maintainable and reliable.

### What to focus on
As the title of the kata says, of course, the tell don't ask principle.
You should be able to remove all the setters moving the behavior into the domain objects.

But don't stop there.

If you can remove some test cases because they don't make sense anymore (eg: you cannot compile the code to do the wrong thing) feel free to do it!

## Part 2: Creating

### Instructions
The file [Constants.cs](TellDontAsk.Tests/Doubles/Constants.cs) defines a Latency to simulate the time to execute the external services.
By default, the value of this constant is set to a very low value and as almost no impact on the tests.
Try to set greater values, for example, 1000 then 2000 and observe the impact on the tests time execution. You should see a linear increase in the time to execute.
The culprits are the async executions in the [OrderCreationUseCase](TellDontAsk/UseCase/OrderCreationUseCase.cs) and [OrderValidationUseCase](TellDontAsk/UseCase/OrderValidationUseCase.cs)
Find a way to improve performance and make parallel calls to independant dependencies.