# NewLF
#This application is divided into 5 parts.
#1- API
#2-Domain
#3-Infra
#4- Shared
#5-Tests


#1- API ==> Making use of the CQRS standard, using mediator,and a pre-validation using FluentApi,trying to keep as clean as possible
  As an authentication criterion, I work with JWT and create middleware, for request filters
#2-Domain ==> Working with context segregation, entity mapping using EF, and connection with sequential (Sql server) and non-sequential (MongoDB) databases
#3-Infra ==> Basically where all communication between Entities, Documents, Repositories and their respective databases
#4- Shared ==> In this part I establish the Extensions, work with triggering jobs events.
#5-Tests ==>Still a work in progress
