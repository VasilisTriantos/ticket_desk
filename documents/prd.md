# TicketDesk — PRD

## Goal

Build a small C# console app from scratch.

Practice:

* classes and enums
* collections
* LINQ
* validation
* exceptions
* writing code without AI generating the first version

## Product

A simple internal ticket tracker.

Users can:

* create tickets
* set priority
* close tickets
* list/filter tickets
* search by title
* view simple statistics

## Ticket

```text
Ticket
- Id
- Title
- Description
- Priority
- Status
- CreatedAt
- ClosedAt?
```

### Priority

* Low
* Medium
* High

### Status

* Open
* Closed

## Core Features

### Create Ticket

Input:

* title
* description
* priority

Rules:

* title required
* description required

### List Tickets

Show:

* id
* title
* priority
* status
* created date

### Close Ticket

Rules:

* ticket must exist
* closed ticket cannot be closed again

### Filter

Filter by:

* status
* priority

### Search

Search title by partial text.

### Statistics

Show:

* total
* open
* closed
* count by priority

## Decisions Made During Creation

* Ticket creation is designed for fast entry with minimal interaction. No
  extra confirmation step is required after entering the ticket details.
* Users may enter any substring of a ticket ID for any ticket-ID lookup or
  action.
* Closing a ticket refuses to proceed when the substring matches more than one
  ticket, preventing an ambiguous destructive action.
* Viewing a specific ticket by ID provides a detailed view with its
  description and closure date when available.
* Viewing all tickets may include those additional details to improve the
  end-user experience.

## Constraints

Use:

* C#
* console app
* `List<T>` or `Dictionary<TKey,TValue>`
* LINQ
* classes
* enums

Do not use:

* database
* EF Core
* MediatR
* Clean Architecture
* web API

## Suggested Structure

```text
TicketDesk/
  Program.cs
  Ticket.cs
  TicketPriority.cs
  TicketStatus.cs
  TicketService.cs
```

## Definition of Done

* all features work
* bad input does not crash normal flows
* code is readable
* you can explain every class and method
* first implementation was written by you
