
# Location Reporter
Location Reporter Microservice. This is part of the suite of services used for the Event Sourcing / CQRS sample in the **Microservices with ASP.NET Core** book from O'Reilly Media. This sample fills the role of the _command submitter_. Applications (mobile, web, embedded, etc) will consume this service to indicate that a new location of a team member has been determined. 

While this is a simple service with a single command to submit (location report), real-world samples often have multiple command services responsible for ingesting a wide variety of commands. 

This service will then convert a command into an event, which is typically done by turning the payload of the command (an expression of intent, e.g. _please record this team member's location_, to something that has occurred in the past, e.g. _member location recorded_).

The API for this service is quite simple:

|Resource|Method|Description|
|---|---|---|
|/api/members/{memberId}/locationreports|POST|Submits a new location report to the service|


