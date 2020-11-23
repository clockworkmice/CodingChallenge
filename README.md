# CodingChallenge
Paymentsense coding challenge

## Setup
* Please download latest dotnet core (3+) -> https://dotnet.microsoft.com/download/dotnet-core

## Notes
* There is an exponential backoff retry policy on the 3rd party API calls to have a more resilient approach
* I have used AutoMapper to map between DTOs and view models
* API responses are cached in memory for 5 minutes to reduce load of the external API
* All paths are unit tested and has e2e tests on the real endpoint
* I have completed task 1-3 for the back end related tasks, I haven't completed the UI parts because, although I am happy to jump into front end pieces when needed, creating an Angular UI on a blank canvas isn't something I specialise in. My expertise are heavily towards the backend, event driven microservices and APIs, architecture and data systems. I hope this is okay for the role I have been interviewing for to date