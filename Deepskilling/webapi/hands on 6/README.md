# Hands On 6 - Kafka Integration with C#

This folder contains two Kafka chat demos built with C#.

## Overview

Kafka is a distributed streaming platform used to publish and consume messages at high throughput. In a Kafka system:

- Topics are named streams of records.
- Partitions split a topic so messages can be processed in parallel.
- Brokers are the Kafka servers that store and serve topic data.
- Producers write messages to topics.
- Consumers read messages from topics using a consumer group.
- Zookeeper is used by classic Kafka installations to coordinate brokers and metadata.

## .NET Kafka Support

This solution uses the `Confluent.Kafka` package, which is the standard .NET client for Apache Kafka.

## Installation Notes

1. Start Zookeeper with `zookeeper-server-start.bat ../../config/zookeeper.properties`.
2. Start Kafka with `kafka-server-start.bat ../../config/server.properties`.
3. Create the demo topic with `kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic chat-message`.

## Demo 1 - Command Prompt Chat

Project: `KafkaChat.ConsoleApp`

- Run `consume` mode in one terminal window to listen for chat messages.
- Run `produce` mode in another terminal window to send messages.
- Messages are serialized as JSON and published to the `chat-message` topic.

## Demo 2 - Windows Client Chat

Project: `KafkaChat.WinFormsApp`

- Each client window connects to the same Kafka topic.
- Multiple client instances can be opened to show the same chat stream.
- The client sends messages and also listens for messages in the background.

## How To Run

1. Open a terminal in this folder.
2. Start Kafka and Zookeeper first.
3. Run the console app with `dotnet run --project .\\KafkaChat.ConsoleApp\\KafkaChat.ConsoleApp.csproj -- consume`.
4. Open another terminal and run the producer with `dotnet run --project .\\KafkaChat.ConsoleApp\\KafkaChat.ConsoleApp.csproj -- produce`.
5. Run the Windows client with `dotnet run --project .\\KafkaChat.WinFormsApp\\KafkaChat.WinFormsApp.csproj`.

## Source Layout

- `KafkaChat.ConsoleApp` contains the command prompt chat demo.
- `KafkaChat.WinFormsApp` contains the Windows client chat demo.
- `HandsOn6.slnx` is the solution file for both projects.