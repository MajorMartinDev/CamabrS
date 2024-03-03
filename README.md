# CamabrS

CamabrS stands for Condition assessment, maintenance and breakdown recovery System.

CamabrS is built with:

Methodology
Event Storming, Event Modeling ideas, see: https://miro.com/app/board/uXjVNtEKrog=/

Tech stack:
.NET Core and C# 8
PostgreSQL

The Critter stack:
Marten for Event Sourcing support. CamabrS is event sourced.
Wolverine for messaging.
Alba for integration testing.

CamabrS's initial idea is to serve as a sample application for workshop and educational materials, however it will be developed with mindeset and ambition of a real product meant for production.

Known issues:
Primitive obsession especially regarding Ids. I am waiting for upcomming better support for strongly typed Ids
in Marten, until then Guid shall do.
