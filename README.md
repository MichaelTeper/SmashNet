# SmashNet
The smash.gg API is particularly unintuitive and difficult to use. Additionally, smash.gg provides minimal guidance in the form of a surface-level explanation of the API. The purpose of SmashNet is to remove the bulk of that complexity and provide a relatively easy to use interface for smash.gg's service. 
# Step By Step Usage
## Connecting to Smash.gg
The SmashClient class is responsible for connecting to and querying the the smash.gg API.

`SmashClient client = new SmashClient()`

Creating an instnace of SmashClient will connect it to smash.gg.
## Getting a Tournament
Getting a tournament requires a request to the API; once again, the SmashClient class is responsible.

`Tournament theBigHouse = await client.GetTournamentAsync("the-big-house-5)`

Calling the GetTournamentAsync method will grab a specific tournament's information and return it in a Tournament object. This method is async, as it must wait for I/O. 
## A Look at the Tournament Model
A tournament may contain many events. e.g. The Big House (tournament) has Melee Singles (phase) and Melee Doubles (phase).

A event may contain many phases. e.g. Melee Singles (event) has Round 1 Pools (phase) and Top 64 (phase).

A phase may contain many brackets. e.g. Round 1 Pools (phase) has Pool A1 (bracket) and Pool C4 (bracket).

A bracket may contain many sets. e.g. Pool A1 (bracket) has Round 1 Winners (set) and Round 2 Losers (set).


Brackets also contain a many entrants, which themselves contain one or more players.

This is useful because in Melee Doubles, one distinct entrant will contain two distinct players.
## Getting Bracket Information
At this point, all brackets in theBigHouse are devoid of entrants and sets. Getting this information requires a request to the API
**for each** bracket; once again, the SmashClient class is responsible.

`client.GetAllBracketInfoForTournamentAsync(theBigHouse)`

This will make a request to smash.gg for every pool, top 64, top 8, in every event of the tournament. This is a **lot** of overhead, assuming the tournament is broken up into many brackets.

`client.GetAllBracketInfoForGameAsync(theBigHouse, Games.Melee)`

This will make a request to smash.gg for every pool, top 64, etc., in every Melee event of the tournament. This is still a lot of overhead.

`client.GetAllBracketInfoForEventAsync(theBigHouse.Events.First())`

This will make a request to smash.gg for every pool, top 64, etc., in an event. Still significant overhead.
`client.GetAllBracketInfoForPhaseAsync(theBigHouse.Phases.First())`

Because phases directly contain brackets, the overhead on this call will vary depending on the phase. A pools phase will have many brackets (therefore many requests to the API), a top 8 phase will have one.

`client.GetBracketInfoForAsync(theBigHouse.Brackets.First())`

One Bracket. One request.
