# QueueSystem

•	Install the tool: dotnet tool install --global dotnet-ef (or update: dotnet tool update --global dotnet-ef)

•	From the solution folder:

•	dotnet ef migrations add Initial --project QueueSystem.Implement --startup-project QueueSystem.API

•	dotnet ef database update --project QueueSystem.Implement --startup-project QueueSystem.API